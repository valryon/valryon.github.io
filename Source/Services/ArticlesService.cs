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

namespace Portfolio.Services
{
    /// <summary>
    /// Articles manager : parse them to html
    /// </summary>
    public class ArticlesService
    {
        private static ArticlesService m_instance;
        private static string cacheKey = "articles";

        private String sourceFileLocation = ConfigurationManager.AppSettings["PostsLocation"];

        private Markdown m_markdownParser;
        private List<Article> m_articles;

        public ArticlesService()
        {
            m_markdownParser = new Markdown(new MarkdownOptions()
            {
                AutoHyperlink = true,
                AutoNewlines = true,
                EncodeProblemUrlCharacters = true,
                LinkEmails = true
            });
        }

        /// <summary>
        /// Get all 
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public List<Article> GetArticles(Controller controller)
        {
            if (CacheManager.TestKey(cacheKey) == false)
            {
                CacheManager.Set<List<Article>>(cacheKey, () =>
                {
                    return generateArticles(controller).OrderByDescending(a => a.PublishedDate).ToList();
                }, new TimeSpan(1, 0, 0, 0), true);
            }

            return CacheManager.Get<List<Article>>(cacheKey);
        }

        /// <summary>
        /// Force post reloading simply by deleting and reloading the cache
        /// </summary>
        /// <param name="controller"></param>
        public void RegenerateArticles(Controller controller)
        {
            CacheManager.Remove(cacheKey);
            GetArticles(controller);
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
                foreach (string source in Directory.GetFiles(sourceFileLocation, "*.md"))
                {
                    Logger.Log(LogLevel.Info, "Processing file: " + source);

                    // For each of them :
                    Article article = new Article();
                    article.SourcePath = source;
                    article.SourceFilename = Path.GetFileName(source);
                    article.Url = controller.Url.Action("Article", "Blog", new { title = Path.GetFileNameWithoutExtension(source).ProcessPath().ToLower() }, "http");

                    // -- Search for a json meta file
                    try
                    {
                        string jsonMeta = File.ReadAllText(Path.GetFullPath(source).Replace(".md", ".meta"));

                        JObject json = JObject.Parse(jsonMeta);

                        string layout = json["layout"].ToString();
                        string title = json["title"].ToString();
                        string desc = json["description"].ToString();
                        List<string> categories = json["categories"].Select(j => j.ToString()).ToList();
                        DateTime dateTime = DateTime.Parse(json["publishedDate"].ToString(), CultureInfo.GetCultureInfo("fr-FR"));

                        article.Layout = layout;
                        article.Title = title;
                        article.Description = desc;
                        article.Categories = categories;
                        article.PublishedDate = dateTime;
                    }
                    catch (Exception metaException)
                    {
                        Logger.LogException(LogLevel.Error, "GenerateArticles.ReadMeta", metaException);
                    }

                    // -- Extract file content
                    string postContent = File.ReadAllText(source);


                    // -- Read the markdown
                    string html = m_markdownParser.Transform(postContent);

                    article.Content = html;

                    articles.Add(article);
                }
            }
            catch (Exception e)
            {
                // Log
                Logger.LogException(LogLevel.Error, "GenerateArticles", e);

                // TODO Send mail

                return null;
            }

            return articles;
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


        public bool AreArticlesGenerated
        {
            get
            {
                return m_articles != null;
            }
        }
    }
}