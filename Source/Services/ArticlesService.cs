using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;
using Newtonsoft.Json.Linq;
using Portfolio.Models;
using Portfolio.Utils.Data;
using Portfolio.Utils.Log;
using HtmlAgilityPack;

namespace Portfolio.Services
{
    /// <summary>
    /// Articles manager : parse them to html
    /// </summary>
    public class ArticlesService
    {
        private static ArticlesService m_instance;
        private static string cacheKey = "articles";
        private static string cacheKeyCategory = "articles_category_";

        private static String sourceFileLocation = ConfigurationManager.AppSettings["PostsLocation"];
        private static String PostsExtension = ConfigurationManager.AppSettings["PostsExtension"];
        private static String PostsInfosExtension = ConfigurationManager.AppSettings["PostsInfosExtension"];

        private static TimeSpan articlesTtl = new TimeSpan(1, 0, 0, 0);

        private Markdown m_markdownParser;

        public ArticlesService()
        {
            m_markdownParser = new Markdown(new MarkdownOptions()
            {
                AutoHyperlink = true,
                AutoNewlines = true,
                EncodeProblemUrlCharacters = true,
                LinkEmails = true
            });

#if DEBUG
            articlesTtl = new TimeSpan(0, 0, 5);
#endif
        }

        /// <summary>
        /// Get all articles (with smart cache)
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<Article> GetArticles(Controller controller, bool onlyFavorites = false)
        {
            if (CacheManager.TestKey(cacheKey) == false)
            {
                CacheManager.Set<List<Article>>(cacheKey, () =>
                {
                    return generateArticles(controller).OrderByDescending(a => a.PublishedDate).ToList();
                }, new TimeSpan(1, 0, 0, 0), true);
            }

            List<Article> articles = CacheManager.Get<List<Article>>(cacheKey);

            if (onlyFavorites)
            {
                return articles.Where(a => a.IsFavorite).ToList();
            }

            return articles;
        }

        /// <summary>
        /// Get the previous article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Article Previous(Controller controller, Article article)
        {
            return getFromArticlePosition(controller, article, -1);
        }

        /// <summary>
        /// Get the next article
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public Article Next(Controller controller, Article article)
        {
            return getFromArticlePosition(controller, article, 1);
        }

        /// <summary>
        /// Get (article + position) article
        /// </summary>
        /// <param name="article"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private Article getFromArticlePosition(Controller controller, Article article, int position)
        {
            List<Article> articles = GetArticles(controller);

            int index = articles.IndexOf(article);

            if (index >= 0)
            {
                int requestedPosition = index + position;

                if (requestedPosition > 0 && requestedPosition < articles.Count)
                {
                    return articles[requestedPosition];
                }
            }

            return null;
        }

        /// <summary>
        /// Get the articles containing the keywrods
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticlesFromKeywords(Controller controller, string keywords)
        {
            List<Article> articles = GetArticles(controller);
            List<Article> results = new List<Article>();

            // Split keywords
            string[] keywordList = keywords.Split('+');
            foreach (string k in keywordList)
            {
                string kClean = k.RemoveHtmlTags().ToLower().Trim();

                foreach (Article a in articles)
                {
                    bool match = false;
                    match |= a.Title.ToLower().Contains(kClean);
                    match |= a.Description.RemoveHtmlTags().ToLower().Contains(kClean);
                    match |= a.HtmlContent.RemoveHtmlTags().ToLower().Contains(kClean);

                    if (match)
                    {
                        results.Add(a);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Get the articles containing this category
        /// </summary>
        /// <returns></returns>
        public List<Article> GetArticlesFromCategories(Controller controller, string cat)
        {
            string catCacheKey = cacheKeyCategory + cat;

            string searchCat = cat.ToLower().Trim();

            // Articles for this cat are in cache?
            if (CacheManager.TestKey(catCacheKey) == false)
            {

                // If no, we put them in
                Func<List<Article>> getArticlesForCat = () =>
                {
                    List<Article> articles = GetArticles(controller);

                    return articles.Where(a => a.Categories.Contains(cat)).ToList();
                };

                CacheManager.Set<List<Article>>(catCacheKey, getArticlesForCat, new TimeSpan(1, 0, 0, 0), true);
            }

            return CacheManager.Get<List<Article>>(catCacheKey);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<string> GetCategories(Controller controller)
        {
            List<string> cats = new List<string>();

            foreach (Article a in GetArticles(controller))
            {
                cats.AddRange(a.Categories);
            }

            return cats.Distinct().ToList(); // :3
        }

        /// <summary>
        /// Force post reloading simply by deleting and reloading the cache
        /// </summary>
        /// <param name="controller"></param>
        public void RegenerateArticles(Controller controller)
        {
            CacheManager.Remove(cacheKey);
            CacheManager.ReloadAll();
        }

        /// <summary>
        /// Read markdown files and transform them to HTML
        /// </summary>
        private List<Article> generateArticles(Controller controller)
        {
            List<Article> articles = new List<Article>();

            // Find the files
            Logger.Log(LogLevel.Debug, "Source file location: " + sourceFileLocation);
            try
            {
                foreach (string source in Directory.GetFiles(sourceFileLocation, "*." + PostsExtension))
                {
                    Logger.Log(LogLevel.Info, "Processing file: " + source);

                    // For each of them :
                    try
                    {
                        Article article = new Article();
                        article.SourcePath = source;
                        article.SourceFilename = Path.GetFileName(source);
                        article.Url = controller.Url.Action("Single", "Blog", new { title = Path.GetFileNameWithoutExtension(source).ProcessPath().ToLower() }, "http");

                        // -- Search for a json meta file
                        try
                        {
                            string jsonMeta = File.ReadAllText(Path.GetFullPath(source).Replace("." + PostsExtension, "." + PostsInfosExtension));

                            JObject json = JObject.Parse(jsonMeta);

                            string layout = json["layout"].ToString();
                            string title = json["title"].ToString();
                            string desc = json["description"].ToString();
                            List<string> categories = json["categories"].Select(j => j.ToString().ToLower().Trim()).ToList();
                            DateTime dateTime = DateTime.Parse(json["publishedDate"].ToString(), CultureInfo.GetCultureInfo("fr-FR"));
                            bool isFavorite =Boolean.Parse(json["favorite"].ToString());
                            string lang = json["lang"].ToString();

                            article.Layout = layout;
                            article.Title = title;
                            article.Description = desc;
                            article.Categories = categories;
                            article.PublishedDate = dateTime;
                            article.IsFavorite = isFavorite;
                            article.Language = lang;

                            // -- Extract file content
                            string postContent = File.ReadAllText(source);


                            // -- Read the markdown
                            string html = m_markdownParser.Transform(postContent);
                            html = manipulateHtml(html);
                            article.HtmlContent = html;

                            articles.Add(article);
                        }
                        catch (Exception metaException)
                        {
                            Logger.LogException(LogLevel.Error, "GenerateArticles.ReadMeta", metaException);
                        }
                    }
                    catch (Exception e)
                    {
                        // Log
                        Logger.LogException(LogLevel.Error, "GenerateArticles.Foreach", e);
                    }
                }
            }
            catch (Exception e)
            {
                // Log
                Logger.LogException(LogLevel.Error, "GenerateArticles", e);
                return null;
            }

            return articles;
        }

        /// <summary>
        /// Apply some auto rules to the html output
        /// </summary>
        /// <param name="htmlInput"></param>
        /// <returns></returns>
        private string manipulateHtml(string htmlInput)
        {
            string htmlOuput = string.Empty ;

            // Manipulate directly the DOM
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlInput);

            // Add class and tricks

            // -- on images
            foreach(var imgElement in doc.DocumentNode.Descendants("img"))
            {
                string imgClass = "";
                if(imgElement.Attributes["class"] != null)
                {
                    imgClass = imgElement.Attributes["class"].Value;
                }
                imgClass += " img-rounded displayed";
                imgElement.SetAttributeValue("class", imgClass);
                
                // center
                //var pContainer = imgElement.Ancestors("p").FirstOrDefault();
            }

            // Return as HTML string
            htmlOuput = doc.DocumentNode.InnerHtml;

            return htmlOuput;
        }



        public static ArticlesService Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ArticlesService();
                }

                return m_instance;
            }
        }
    }
}