using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageParentCategoryItem : PageItem
    {
        public Uri LinkToPage { get; set; }
        public override PageItemType PageItemType { get { return PageItemType.ParentCategoryItem; } }
    }
}