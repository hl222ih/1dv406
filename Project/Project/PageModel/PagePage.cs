using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    //Den här klassens objekt motsvarar innehållet för en sida, inklusive alla
    //betydelser, bilder osv.
    public class PagePage
    {
        public IEnumerable<PageItemsUnit> PageItemUnits { get; set; }
        public string CategoryName { get; set; }
        public int UnitsPerPage { get; set; }
        public string CssTemplate { get; set; }
        public int PageNumber { get; set; }
    }
}