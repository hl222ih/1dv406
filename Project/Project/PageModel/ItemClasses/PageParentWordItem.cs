using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class PageParentWordItem : PageItem
    {
        //se kommentarer i enum för PageItemType.

        public override PageItemType PageItemType 
        { 
            get 
            { 
                return PageItemType.ParentWordItem; 
            } 
            set 
            { 
                //inget set 
            } 
        }
    }
}