using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MarkdownView.Controllers
{
    public class PageController : Controller
    {
        private string AppPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["PageRoot"];
            }
        }

        private string UriPath
        {
            get
            {
                return WebConfigurationManager.AppSettings["UriRoot"];
            }
        }

        [Route()]
        [HttpGet]
        public ActionResult Index()
        {
            var list =
                from folder in Directory.GetDirectories(this.AppPath)
                let files =
                    from filename in Directory.GetFiles(folder, "*.md")
                    select Path.GetFileNameWithoutExtension(filename)
                where files.Count() > 0
                select new Models.Index(
                    folder.Split(Path.DirectorySeparatorChar).ToList().Last(),
                    files);
            
            return View(list);
        }

        [Route("{folder}/{file}")]
        [HttpGet]
        public ActionResult Page(string folder, string file)
        {
            var content = System.IO.File.ReadAllText(
                Path.Combine(this.AppPath, folder, file + ".md"));

            content = content.Replace(
                "![image](~",
                string.Format("![image]({0}", this.UriPath));

            var transformer = new Strike.IE.Markdownify();

            string markdown = transformer.Transform(content);

            //<pre><code class="([a-z]*-[a-z]*)"

            //<pre class="$1 prettyprint"><code

            markdown = markdown.Replace("<pre", "<pre class=\"prettyprint\"");

            return View(new Models.Page(file, markdown));
        }
    }
}