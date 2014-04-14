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
        public int UnitsPerPage { get; set; }
        public string CssTemplateName { get; set; }
        public int PageNumber { get; set; }

        //default-konstruktor...
        public PagePage()
        {
        }

        public PageItemsUnit GetPageItemsUnit(int meaningId)
        {
            return PageItemsUnits.First(piu => piu.MeaningId == meaningId);
        }

    }
}