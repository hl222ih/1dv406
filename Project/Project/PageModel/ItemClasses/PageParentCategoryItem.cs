using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageParentCategoryItem : PageItem
    {
        //se kommentarer i enum för PageItemType.

        public int LinkToCategoryId { get; set; }

        public override PageItemType PageItemType 
        {
            get 
            { 
                return PageItemType.ParentCategoryItem; 
            } 
            set 
            { 
                //inget set 
            } 
        }
    }
}