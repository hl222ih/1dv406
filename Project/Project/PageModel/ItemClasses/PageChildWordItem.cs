using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageChildWordItem : PageItem
    {
        //typiskt PageItemType.ChildLeftWordItem _eller PageItemType.ChildRightWordItem.
        //se kommentarer i enum för PageItemType.

        private PageItemType pageItemType;

        public override PageItemType PageItemType 
        { 
            get { return pageItemType; }
            set { pageItemType = value; }
        }
    }
}