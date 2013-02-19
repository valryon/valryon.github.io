using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portfolio.Utils.Log;
using System.Globalization;
using System;

namespace Portfolio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Redirect old site resources
            routes.MapRoute(
                "RedirectDownload",
                "telechargements/",
                new { controller = "Blog", action = "RedirectDownload" }
            );

            routes.MapRoute(
                "Error 404",
                "404",
                new { controller = "Error", action = "Error404" }
            );

            routes.MapRoute(
                "Error 500",
                "500",
                new { controller = "Error", action = "Error500" }
            );

            routes.MapRoute(
                "Sitemap",
                "sitemap.xml",
                new { controller = "Blog", action = "Sitemap" }
            );

            routes.MapRoute(
                "Robots",
                "robots.txt",
                new { controller = "Blog", action = "Robots" }
            );

            routes.MapRoute(
                "Reload",
                "reload",
                new { controller = "Blog", action = "ReloadArticles" }
            );

            routes.MapRoute(
                "Feed",
                "feed",
                new { controller = "Blog", action = "Feed" }
            );

            routes.MapRoute(
                "SearchRequest",
                "search/{request}",
                new { controller = "Blog", action = "Search"}
            );

            routes.MapRoute(
                "Search",
                "search",
                new { controller = "Blog", action = "Search" }
            );

            routes.MapRoute(
               "Home",
               "blog/{page}",
               new { controller = "Blog", action = "Index", page = 1 },
               new { page = @"\d+" }
            );

            routes.MapRoute(
                "Article",
                "{title}",
                new { controller = "Blog", action = "Single" }
            );

            routes.MapRoute(
                "Default", // Nom d'itinéraire
                "{controller}/{action}/{id}", // URL avec des paramètres
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional } // Paramètres par défaut
            );
        }


        protected void Application_Start()
        {
            // Initialize log
            Logger.Initialize("Portfolio.Log");
            Logger.Log(LogLevel.Info, "Starting website Portfolio...");

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // Get the requested URL so we can do some validation on it.
            // We exclude the query string, and add that later, so it's not included
            // in the validation
            string url = (Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.AbsolutePath);

            // If we're not a request for the root, and end with a slash, strip it off
            if (HttpContext.Current.Request.Url.AbsolutePath != "/" && HttpContext.Current.Request.Url.AbsolutePath.EndsWith("/"))
            {
                Response.RedirectPermanent(url.Substring(0, url.Length - 1) + HttpContext.Current.Request.Url.Query);
            }
        }
    }
}