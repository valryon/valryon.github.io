using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.ViewModels
{
    public abstract class BaseViewModel
    {
        public string Title { get; set; }
        public string Search { get; set; }
        public List<string> Categories { get; set; }

        public List<ArticleViewModel> FavoritesArticles { get; set; }

        public Dictionary<string, string> Metas { get; set; }

        public BaseViewModel()
        {
            Metas = new Dictionary<string, string>();
        }
    }
}