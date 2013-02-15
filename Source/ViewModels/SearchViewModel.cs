using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public List<Article> Results { get; set; }

        public string SearchTitle { get; set; }
    }
}