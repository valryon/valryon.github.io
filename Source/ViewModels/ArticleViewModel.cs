using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class ArticleViewModel
    {
        public Article Article { get; set; }

        public bool ShowContent { get; set; }

        public ArticleViewModel()
        {
            ShowContent = true;
        }

        public ArticleViewModel(Article a, bool showContent = true)
            : this()
        {
            ShowContent = showContent;
            Article = a;
        }
    }
}