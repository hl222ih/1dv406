using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageCategory
    {
        private PagePage currentPage;

        public int CatId { get; set; }
        public string CatName { get; set; }
        public int CurrentPageNumber { get; set; }

        public PagePage CurrentPage { get { return currentPage ?? (currentPage = new PagePage()); } }

        public string GetCurrentCssTemplateName()
        {
            return CurrentPage.CssTemplateName;
        }

        public IEnumerable<PageItemsUnit> GetCurrentPageItemsUnits()
        {
            return CurrentPage.PageItemsUnits.AsEnumerable();
        }

        public PageCategory()
        {
            CurrentPageNumber = 1;
        }

    }
}