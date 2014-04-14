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
        /// <summary>
        /// Lista med PageItem-objekt.
        /// </summary>
        public IList<PageItem> PageItems { get; set; }

        /// <summary>
        /// Hämtar och returnerar den första (bör vara den enda) ParentItem i PageItems.
        /// </summary>
        /// <returns>ParentItem (ParentCategoryItem eller ParentWordItem) om sådan finns, annars null.</returns>
        public PageItem GetPageParentItem()
        {
            return PageItems.FirstOrDefault(pi =>
            pi.PageItemType == PageItemType.ParentCategoryItem ||
            pi.PageItemType == PageItemType.ParentWordItem);
        }

        /// <summary>
        /// Hämtar och returnerar en samling med ChildItems i PageItems.
        /// </summary>
        /// <returns>Samling med ChildItems (ChildLeftWordItem:s och ChildRightWordItem:s).</returns>
        public IEnumerable<PageItem> GetPageChildItems()
        {
            return PageItems.Where(pi =>
                pi.PageItemType == PageItemType.ChildLeftWordItem ||
                pi.PageItemType == PageItemType.ChildRightWordItem);
        }

        /// <summary>
        /// Hämtar och returnerar den MeaningId som PageItems tillhör. (Alla PageItem:s bör tillhöra samma).
        /// </summary>
        public int MeaningId
        {
            get { return PageItems[0].MeaningId; }
        }

        /// <summary>
        /// Hämtar och returnerar nästa PageItem utgående från aktuell PageItem med inskickade värden.
        /// </summary>
        /// <param name="pit">aktuell PageItemType</param>
        /// <param name="position">aktuell position räknat från mitten = 0. Ett steg från mitten = 1. Två steg från mitten = 2 osv.</param>
        /// <returns>PageItem till vänster om aktuellt PageItem, om sådan finns, annars null</returns>
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

        /// <summary>
        /// Hämtar och returnerar nästa PageItem utgående från aktuell PageItem med inskickade värden.
        /// </summary>
        /// <param name="pit">aktuell PageItemType</param>
        /// <param name="position">aktuell position räknat från mitten = 0. Ett steg från mitten = 1. Två steg från mitten = 2 osv.</param>
        /// <returns>PageItem till höger om aktuellt PageItem, om sådan finns, annars null</returns>
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

        /// <summary>
        /// Hämtar och returnerar PageItem som har inskickat pageItemId (motsvarar itemId i databasen).
        /// </summary>
        /// <param name="pageItemId">PageItem:s PageItemId</param>
        /// <returns>PageItem-objektet.</returns>
        public PageItem GetPageItem(int pageItemId)
        {
            return PageItems.First(pi => pi.PageItemId == pageItemId);
        }
    }
}