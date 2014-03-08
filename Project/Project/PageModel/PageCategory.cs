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
        public int UnitsPerPage { get; set; }
        public string CssTemplate { get; set; }
    }
}