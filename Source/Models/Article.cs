using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Models
{
    public class Article
    {
        public string Title { get; set; }
        public string HtmlContent { get; set; }

        public string Layout { get; set; }

        public string Description { get; set; }

        public List<string> Categories { get; set; }

        public DateTime PublishedDate { get; set; }

        public string SourcePath { get; set; }

        public string SourceFilename { get; set; }

        public string Url { get; set; }

        public bool IsFavorite { get; set; }

        public string Language { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Article)
            {
                Article a = obj as Article;

                return a.SourcePath == SourcePath;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}