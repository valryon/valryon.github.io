using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string Layout { get; set; }

        public string Description { get; set; }

        public List<string> Categories { get; set; }

        public DateTime PublishedDate { get; set; }

        public string SourcePath { get; set; }

        public string SourceFilename { get; set; }

        public string Url { get; set; }
    }
}