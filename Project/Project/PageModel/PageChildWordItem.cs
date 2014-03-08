using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageChildWordItem : PageItem
    {
        public override PageItemType GetPageItemType()
        {
            return PageItemType.ChildWordItem;
        }

    }
}