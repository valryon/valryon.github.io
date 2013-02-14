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
        //
        // GET: /Home/

        public ActionResult Index()
        {
            BlogViewModel model = new BlogViewModel();
            model.Articles = ArticlesService.Instance.GetArticles(this);

            return View(model);
        }

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

            return View("Error404", "Error");
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
