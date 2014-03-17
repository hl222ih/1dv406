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
            //Hårdkodad specialhämtning, har inte hunnit skapa en generisk metod för att hämta
            //rätt element baserat på sida för vald kategori. Kategorin med kategoriId är
            //den kategorin som innehåller fler element än vad som får plats på en sida
            //med nuvarande test-data.
            if (CatId != 4)
            {
                return CurrentPage.PageItemsUnits.AsEnumerable();
            }
            else if (CurrentPageNumber == 1)
            {
                return CurrentPage.PageItemsUnits.ToList().GetRange(0, 11).AsEnumerable();
            }
            else if (CurrentPageNumber == 2)
            {
                return CurrentPage.PageItemsUnits.ToList().GetRange(12, 12).AsEnumerable();
            }
            return null;
        
        }

        public PageCategory()
        {
            CurrentPageNumber = 1;
        }

        public Dictionary<int, string> GetPageItemFileNames(int meaningId)
        {
            return CurrentPage.PageItemsUnits
                .First(piu => piu.MeaningId == meaningId)
                .PageItems
                .ToDictionary(pi => pi.PageItemId, pi => pi.ImageFileName);
        }

    }
}