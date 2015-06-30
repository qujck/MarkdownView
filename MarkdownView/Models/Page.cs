using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkdownView.Models
{
    public class Page
    {
        public Page(string name, string markdown)
        {
            this.Name = name;
            this.Markdown = markdown;
        }

        public string Name { get; private set; }

        public string Markdown { get; private set; }
    }
}