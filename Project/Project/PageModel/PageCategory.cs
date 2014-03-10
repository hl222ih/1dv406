using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageCategory
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public int[] UnitsPerPages { get; set; }
        public string[] CssTemplates { get; set; }
    }
}