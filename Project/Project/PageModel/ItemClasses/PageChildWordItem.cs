using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageChildWordItem : PageItem
    {
        private PageItemType pageItemType;

        public override PageItemType PageItemType 
        { 
            get { return pageItemType; }
            set { pageItemType = value; }
        }
    }
}