using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Portfolio.Services;
using Portfolio.Utils.Log;
using Portfolio.Utils.Web;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class BlogController : BaseController
    {
        private static int pageSize = 3;

        /// <summary>
        /// Blog home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            BlogViewModel model = new BlogViewModel();
            model.Articles = ArticlesService.Instance.GetArticles(this).Select(a => new ArticleViewModel(a)).ToList();

            model.CurrentPage = 1;
            if (page.HasValue)
            {
                model.CurrentPage = page.Value > 0 ? page.Value : 1;
            }

            model.PageCount = model.Articles.Count / pageSize;
            model.Articles = model.Articles.Skip((model.CurrentPage - 1) * pageSize).Take(pageSize).ToList();

            return View(model);
        }

        /// <summary>
        /// Blog page
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ActionResult Single(string title)
        {
            // Look for the article's file named with that title
            foreach (Article article in ArticlesService.Instance.GetArticles(this))
            {
                if (article.Url.ToLower().Contains(Request.RawUrl.ToLower()))
                {
                    SingleViewModel model = new SingleViewModel();
                    model.Content = new ArticleViewModel(article);
                    model.PreviousArticle = ArticlesService.Instance.Previous(this, article);
                    model.NextArticle = ArticlesService.Instance.Next(this, article);

                    return View(model);
                }
            }

            return RedirectToActionPermanent("Error404", "Error");
        }

        /// <summary>
        /// Search for articles
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult Search(string request)
        {
            SearchViewModel search = new SearchViewModel();
            search.Search = request;

            if (request.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }

            bool isCategory = false;

            if (ArticlesService.Instance.GetCategories(this).Contains(request.ToLower()))
            {
                isCategory = true;
            }

            search.IsCategory = isCategory;
           
            if (isCategory)
            {
                search.Results = ArticlesService.Instance.GetArticlesFromCategories(this, request).Select(a => new ArticleViewModel(a, false)).ToList();
            }
            else
            {
                search.Results = ArticlesService.Instance.GetArticlesFromKeywords(this, request).Select(a => new ArticleViewModel(a, false)).ToList();
            }

            return View(search);
        }

        public ActionResult ReloadArticles()
        {
            ArticlesService.Instance.RegenerateArticles(this);

            return RedirectToAction("Index");
        }

        public string Sitemap()
        {
            return "";
        }

        public ActionResult Robots()
        {
            string robots = "";
            robots += "User-agent: *\n";
            robots += "Sitemap: " + Url.Action("Sitemap", "Blog", null, "http");

            return new ContentResult() {
               Content = robots,
               ContentType = "text/text"
            };
        }

        public ActionResult Feed()
        {
            SyndicationFeed feed = new System.ServiceModel.Syndication.SyndicationFeed();
            feed.Description = new TextSyndicationContent("Articles from Damien Mayance");
            feed.Title = new TextSyndicationContent("Damien Mayance");
            feed.Copyright = new TextSyndicationContent("Damien Mayance");
            feed.Links.Add(new SyndicationLink()
            {
                Uri = new Uri(Request.Url.Host, UriKind.RelativeOrAbsolute)
            });

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Article article in ArticlesService.Instance.GetArticles(this))
            {
                try
                {
                    SyndicationItem item = new SyndicationItem();

                    item.Content = new TextSyndicationContent(article.HtmlContent);
                    item.AddPermalink(new Uri(article.Url, UriKind.RelativeOrAbsolute));
                    item.PublishDate = article.PublishedDate;
                    item.Title = new TextSyndicationContent(article.Title);

                    items.Add(item);
                }
                catch (Exception ex)
                {
                    Logger.LogException(LogLevel.Error, "Blog.Feed", ex);
                }
            }

            feed.Items = items;

            return new RssActionResult()
            {
                Feed = feed
            };
        }

        public ActionResult RedirectDownload()
        {
            return RedirectToActionPermanent("Index");
        }
    }
}
