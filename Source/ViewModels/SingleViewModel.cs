using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class SingleViewModel : BaseViewModel
    {
        public ArticleViewModel Content { get; set; }

        public Article NextArticle { get; set; }

        public Article PreviousArticle { get; set; }

        public Dictionary<string,List<Article>> CategoriesArticles { get; set; }

        public SingleViewModel()
        {
            CategoriesArticles = new Dictionary<string, List<Article>>();
        }
    }
}