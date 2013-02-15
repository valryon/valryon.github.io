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
        /// <summary>
        /// Blog home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BlogViewModel model = new BlogViewModel();
            model.Articles = ArticlesService.Instance.GetArticles(this);

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
                    model.Content = new ArticleViewModel()
                    {
                        Article = article
                    };
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
        [AcceptVerbs("POST", "GET")]
        public ActionResult Search(string request)
        {
            SearchViewModel search = new SearchViewModel();

            bool isCategory = false;
            
            if (ArticlesService.Instance.GetCategories(this).Contains(request.ToLower()))
            {
                isCategory = true;
            }

            if (isCategory)
            {
                search.SearchTitle = "Category: \"" + request + "\"";
                search.Results = ArticlesService.Instance.GetArticlesFromCategories(this, request);
            }
            else
            {
                search.SearchTitle = "\"" + request + "\"";
                search.Results = ArticlesService.Instance.GetArticlesFromKeywords(this, request);
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

        public string Robots()
        {
            return "";
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
    }
}
