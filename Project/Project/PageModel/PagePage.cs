using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model;
using System.Drawing;

namespace Project.PageModel
{
    //Den här klassens objekt motsvarar innehållet för en sida, inklusive alla
    //betydelser, bilder osv.
    public class PagePage
    {
        public IList<PageItemsUnit> PageItemsUnits { get; set; }
        //public string CategoryName { get; set; }
        public int UnitsPerPage { get; set; }
        public string CssTemplateName { get; set; }
        public int PageNumber { get; set; }

        public PagePage()
        {
            //lite testvärden
            //CategoryName = "Huvudmeny";
            UnitsPerPage = 10;
            CssTemplateName = "page-default";
            PageNumber = 1;
            PageItemsUnits = new List<PageItemsUnit>();
        }

    }
}