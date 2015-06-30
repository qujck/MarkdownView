using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkdownView.Models
{
    public class Index
    {
        public Index(string name, IEnumerable<string> pages)
        {
            this.Name = name;
            this.Pages = pages;
        }

        public string Name { get; private set; }

        public IEnumerable<string> Pages { get; private set; }
    }
}