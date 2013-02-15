using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portfolio.Models;
using Portfolio.Services;
using Portfolio.ViewModels;

namespace Portfolio.Controllers
{
    public class BlogController : Controller
    {
        /// <summary>
        /// Blog home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BlogViewModel model = new BlogViewModel();
            model.Articles = ArticlesService.Instance.GetArticles(this);
            model.Categories = ArticlesService.Instance.GetCategories(this);

            return View(model);
        }

        /// <summary>
        /// Blog page
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public ActionResult Article(string title)
        {
            // Look for the article's file named with that title
            foreach (Article article in ArticlesService.Instance.GetArticles(this))
            {
                if (article.Url.ToLower().Contains(Request.RawUrl.ToLower()))
                {
                    ArticleViewModel model = new ArticleViewModel();
                    model.Article = article;
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
    }
}
