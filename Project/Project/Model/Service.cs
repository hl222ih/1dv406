using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model.DAL;
using Project.PageModel;
using System.Drawing;

namespace Project.Model
{
    public class Service
    {
        //dataåtkomstlagret, instans av BlissKomDAL
        private BlissKomDAL blissKomDAL;
        //lagring av ordtyper, så att (den statiska) informationen inte
        //behöver hämtas från databasen gång på gång.
        private IEnumerable<PageWordType> pageWordTypes; 
        //lagring av aktuell PageCategory.
        //innehåller alla PagePages som innehåller alla PageItemsUnits och deras respektive PageItems
        //som behövs för rendering av aktuell sida. 
        private PageCategory currentPageCategory;

        /// <summary>
        /// Egenskap. Hämtar eller sätter aktuell kategori. 
        /// Om hämtas och saknas, skapas en ny kategori beståend av startkategorin (första sidan), som returneras.
        /// </summary>
        private PageCategory CurrentPageCategory
        {
            get { return currentPageCategory ?? (currentPageCategory = GetPageCategory(1, 1)); }
            set { currentPageCategory = value; }
        }

        /// <summary>
        /// Egenskap. Hämtar och returnerar dataåtkomstobjektet. Om sådant saknas skapas ett nytt som returneras.
        /// </summary>
        private BlissKomDAL BlissKomDAL
        {
            get { return blissKomDAL ?? (blissKomDAL = new BlissKomDAL()); }
        }

        /// <summary>
        /// Egenskap. Hämtar och returnerar en samling med information om ordtyperna.
        /// </summary>
        public IEnumerable<PageWordType> PageWordTypes
        {
            get 
            {
                if (pageWordTypes == null)
                {
                    pageWordTypes = GetPageWordTypes().Select(pwt => new PageWordType
                    {
                        WType = pwt.WType,
                        WTypeId = pwt.WTypeId,
                        ColorId = pwt.ColorId,
                        ColorRGBCode = pwt.ColorRGBCode
                    });
                }

                return pageWordTypes;
            }
        }

        /// <summary>
        /// Hämtar och returnerar färgkoden för inskickat ordtyps-id.
        /// </summary>
        /// <param name="wTypeId">ordtypens id</param>
        /// <returns>färgkoden som textsträng</returns>
        public string GetColorRGBCodeOfPageWordType(int wTypeId)
        {
            return GetPageWordType(wTypeId).ColorRGBCode;
        }

        /// <summary>
        /// Hämtar och returnerar färgkoden för inskickat ordtyps-id.
        /// </summary>
        /// <param name="wTypeId">ordtypens id</param>
        /// <returns>färgkoden som Color-objekt.</returns>
        public Color GetColorOfPageWordType(int wTypeId)
        {
            return GetPageWordType(wTypeId).Color;
        }

        /// <summary>
        /// Hämtar och returnerar PageWordType-objektet för inskickat ordtyps-id.
        /// </summary>
        /// <param name="wTypeId">ordtypens id</param>
        /// <returns>PageWordType-objektet.</returns>
        private PageWordType GetPageWordType(int wTypeId)
        {
            return PageWordTypes.First(pwt => pwt.WTypeId == wTypeId);
        }

        /// <summary>
        /// Konstruktor. Skapar ett nytt service-objekt.
        /// </summary>
        public Service()
            : base()
        {
            //Skapas egentligen automatiskt och anropar base() automatiskt, men har med för tydlighets skull.
        }

        /// <summary>
        /// Hämtar och returnerar en samling med samtliga ordtyper.
        /// </summary>
        /// <returns>samlingen med alla ordtyper</returns>
        private IEnumerable<PageWordType> GetPageWordTypes()
        {
            return BlissKomDAL.SelectAllPageWordTypes();
        }

        /// <summary>
        /// Hämtar och returnerar namnet på den css-mall som ska användas för aktuell kategori/sida.
        /// </summary>
        /// <returns>Namnet på css-mallen.</returns>
        public string GetCurrentCssTemplateName()
        {
            return CurrentPageCategory.GetCurrentCssTemplateName();
        }

        /// <summary>
        /// Hämtar och returnerar aktuellt kategori-id.
        /// </summary>
        /// <returns>kategori-id:t</returns>
        public int GetCurrentCategoryId()
        {
            return CurrentPageCategory.CatId;
        }

        /// <summary>
        /// Hämtar och returnerar aktuellt sidnummer.
        /// </summary>
        /// <returns>Sidnumret</returns>
        public int GetCurrentPageNumber()
        {
            //Applikationen har f.n. bara stöd för 1 sida per kategori. Databasen stödjer flera.
            return CurrentPageCategory.CurrentPageNumber;
        }

        /// <summary>
        /// Hämtar och returnerar det PageItem (den bild m m) som finns till vänster om den som för närvarande är aktuell.
        /// </summary>
        /// <param name="pit">aktuell ordtyp</param>
        /// <param name="position">aktuell position</param>
        /// <param name="meaningId">aktuellt betydelse-id.</param>
        /// <returns>det PageItem som finns till vänster om det nuvarande.</returns>
        public PageItem GetNextLeftPageItem(PageItemType pit, int position, int meaningId)
        {
            var piu = CurrentPageCategory.CurrentPage.GetPageItemsUnit(meaningId);
            return piu.GetNextLeftPageItem(pit, position);
        }

        /// <summary>
        /// Hämtar och returnerar det PageItem (den bild m m) som finns till höger om den som för närvarande är aktuell.
        /// </summary>
        /// <param name="pit">aktuell ordtyp</param>
        /// <param name="position">aktuell position</param>
        /// <param name="meaningId">aktuellt betydelse-id.</param>
        /// <returns>det PageItem som finns till höger om det nuvarande.</returns>
        public PageItem GetNextRightPageItem(PageItemType pit, int position, int meaningId)
        {
            var piu = CurrentPageCategory.CurrentPage.GetPageItemsUnit(meaningId);
            return piu.GetNextRightPageItem(pit, position);
        }

        /// <summary>
        /// Hämtar och returnerar de PageItemsUnits som tillhör aktuell sida.
        /// </summary>
        /// <returns>Samlingen med PageItemsUnits</returns>
        public IEnumerable<PageItemsUnit> GetCurrentPageItemsUnits()
        {
            return CurrentPageCategory.GetCurrentPageItemsUnits();
        }

        /// <summary>
        /// Privat metod.
        /// Hämtar och returnerar PageCategory som har inskickat kategori-id och sidnummer.
        /// </summary>
        /// <param name="categoryId">kategori-id:t</param>
        /// <param name="pageNumber">sidnumret (default = 1)</param>
        /// <returns>begärd PageCategory.</returns>
        private PageCategory GetPageCategory(int categoryId, int pageNumber = 1)
        {
            var pageCategory = new PageCategory
            {
                CatId = categoryId,
                CurrentPageNumber = pageNumber,
            };

            var pageItems = BlissKomDAL.SelectPageItemsOfPage(categoryId, pageNumber);

            pageCategory.CurrentPage.PageItemsUnits = pageItems
                .GroupBy(pi => pi.MeaningId)
                .Select(group => new PageItemsUnit
                {
                    PageItems = group.ToList()
                }).ToList();

            pageCategory.CurrentPage.CssTemplateName = String.Format("page-{0}", BlissKomDAL.SelectPageInfo(categoryId, pageNumber));
            pageCategory.CurrentPage.PageNumber = pageNumber;
            
            return pageCategory;
        }


        /// <summary>
        /// Uppdaterar aktuell PageCategory med den PageCategory som har inskickat kategori-id och sidnummer.
        /// </summary>
        /// <param name="categoryId">kategori-id</param>
        /// <param name="pageNumber">sidnummer (default = 1)</param>
        public void UpdatePageCategory(int categoryId, int pageNumber = 1)
        {
            currentPageCategory = GetPageCategory(categoryId, pageNumber);
        }

        public IEnumerable<PageItem> GetPageItems(int categoryId, int pageNumbers)
        {
            return BlissKomDAL.SelectPageItemsOfPage(categoryId, pageNumbers);
        }

        public IEnumerable<Meaning> GetMeanings()
        {
            return BlissKomDAL.SelectAllMeanings();
        }

        public Meaning GetMeaning(Int16 meaningId)
        {
            return BlissKomDAL.SelectMeaning(meaningId);
        }

        public Dictionary<int, string> GetPageItemFileNames(Int16 meaningId)
        {
            return BlissKomDAL.SelectPageItemFileNames(meaningId);
        }

        public void SaveOrUpdateMeaning(Meaning meaning)
        {
            if (meaning.MeaningId > 0)
            {
                BlissKomDAL.UpdateMeaning(meaning);
            }
            else
            {
                BlissKomDAL.InsertMeaning(meaning);
            }
        }

        public void DeleteMeaning(Int16 meaningId)
        {
            BlissKomDAL.DeleteMeaning(meaningId);
        }

        public Dictionary<int, string> GetAllFileNames()
        {
            return BlissKomDAL.SelectAllFileNames();
        }

        public Dictionary<int, string> GetAllCategories()
        {
            return BlissKomDAL.SelectAllCategories();
        }

        public KeyValuePair<int, int?> GetCatInfoOfMeaning(Int16 meaningId)
        {
            return BlissKomDAL.SelectCatInfoOfMeaning(meaningId);
        }

        public Dictionary<int, string> GetAllPositions()
        {
            return BlissKomDAL.SelectAllPositions();
        }

        public int GetPositionOfItem(Int16 itemId)
        {
            return BlissKomDAL.SelectPositionIdOfItem(itemId);
        }

        public void SaveOrUpdateItem(Item item)
        {
            if (item.ItemId > 0)
            {
                BlissKomDAL.UpdateItem(item);
            }
            else
            {
                BlissKomDAL.InsertItem(item);
            }
        }

        public void DeleteItem(Int16 itemId)
        {
            BlissKomDAL.DeleteItem(itemId);
        }
    }
}