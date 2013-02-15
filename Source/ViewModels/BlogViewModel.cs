using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class BlogViewModel : BaseViewModel
    {
        public List<ArticleViewModel> Articles { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int MinPage { get; set; }
    }
}