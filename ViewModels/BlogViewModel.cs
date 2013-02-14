using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Portfolio.Models;

namespace Portfolio.ViewModels
{
    public class BlogViewModel : BaseViewModel
    {
        public List<Article> Articles { get; set; }
    }
}