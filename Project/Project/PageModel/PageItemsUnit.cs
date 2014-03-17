using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    //Denna klass objekt motsvarar en grupp med PageItem-objekt.
    //Endast en av dessa PageItem-objekt visas samtidigt, men
    //de hör ihop på så sätt att de delar Meaning och att det
    //går att navigera mellan dem i en viss ordning.
    public class PageItemsUnit
    {
        public IList<PageItem> PageItems { get; set; }

        public PageItem GetPageParentItem()
        {
            return PageItems.First(pi =>
                pi.PageItemType == PageItemType.ParentCategoryItem ||
                pi.PageItemType == PageItemType.ParentWordItem);
        }

        public IEnumerable<PageItem> GetPageChildItems()
        {
            return PageItems.Where(pi =>
                pi.PageItemType == PageItemType.ChildLeftWordItem ||
                pi.PageItemType == PageItemType.ChildRightWordItem);
        }

        public int MeaningId
        {
            get { return PageItems[0].MeaningId; }
        }

        public PageItem GetNextLeftPageItem(PageItemType pit, int position)
        {
            if (pit == PageItemType.ParentWordItem)
            {
                return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildLeftWordItem && pi.Position == 1);
            }
            else if (pit == PageItemType.ChildLeftWordItem)
            {
                return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildLeftWordItem && pi.Position == position + 1);
            }
            else if (pit == PageItemType.ChildRightWordItem)
            {
                if (position == 1)
                {
                    return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ParentWordItem);
                }
                else
                {
                    return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildRightWordItem && pi.Position == position - 1);
                }
            }

            return null;
        }

        public PageItem GetNextRightPageItem(PageItemType pit, int position)
        {
            if (pit == PageItemType.ParentWordItem)
            {
                return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildRightWordItem && pi.Position == 1);
            }
            else if (pit == PageItemType.ChildRightWordItem)
            {
                return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildRightWordItem && pi.Position == position + 1);
            }
            else if (pit == PageItemType.ChildLeftWordItem)
            {
                if (position == 1)
                {
                    return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ParentWordItem);
                }
                else
                {
                    return PageItems.FirstOrDefault(pi => pi.PageItemType == PageItemType.ChildLeftWordItem && pi.Position == position - 1);
                }
            }

            return null;
        }

        public PageItem GetPageItem(int pageItemId)
        {
            return PageItems.First(pi => pi.PageItemId == pageItemId);
        }
    }
}