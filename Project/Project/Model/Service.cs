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

        /// <summary>
        /// Hämtar och returnerar en samling med PageItems som tillhör inskickat kategori-id och sidnummer.
        /// </summary>
        /// <param name="categoryId">kategori-id:t</param>
        /// <param name="pageNumber">sidnumret</param>
        /// <returns>samlingen med PageItem:s</returns>
        public IEnumerable<PageItem> GetPageItems(int categoryId, int pageNumber)
        {
            return BlissKomDAL.SelectPageItemsOfPage(categoryId, pageNumber);
        }

        /// <summary>
        /// Hämtar och returnerar en samling med alla Meaning-objekt.
        /// </summary>
        /// <returns>Samlilngen med Meaning:s</returns>
        public IEnumerable<Meaning> GetMeanings()
        {
            return BlissKomDAL.SelectAllMeanings();
        }

        /// <summary>
        /// Hämtar och returnerar det Meaning-objekt som har inskickat meaningId.
        /// </summary>
        /// <param name="meaningId">MeaningId</param>
        /// <returns>Meaning-objektet.</returns>
        public Meaning GetMeaning(Int16 meaningId)
        {
            return BlissKomDAL.SelectMeaning(meaningId);
        }

        /// <summary>
        /// Hämtar och returnerar en associativ vektor med de ItemId:n och FileName:s som
        /// tillhör betydelse-id:t MeaningId.
        /// </summary>
        /// <param name="meaningId">MeaningId.</param>
        /// <returns>Associativ vektor med ItemId (key) och FileName (value)</returns>
        public Dictionary<int, string> GetPageItemFileNames(Int16 meaningId)
        {
            return BlissKomDAL.SelectPageItemFileNames(meaningId);
        }

        /// <summary>
        /// Sparar nytt eller uppdaterar befintligt Meaning-objekt.
        /// Nytt om Meaning-objektets egenskap MeaningId är 0, annars uppdatera.
        /// </summary>
        /// <param name="meaning">Meaning-objektet</param>
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

        /// <summary>
        /// Raderar Meaning med inskickat MeaningId.
        /// </summary>
        /// <param name="meaningId">MeaningId</param>
        public void DeleteMeaning(Int16 meaningId)
        {
            BlissKomDAL.DeleteMeaning(meaningId);
        }

        /// <summary>
        /// Hämtar och returnerar en associativ vektor med alla Image-objekts ImageId respektive FileName
        /// </summary>
        /// <returns>Associativ vektor med ImageId (key) och FileName (value)</returns>
        public Dictionary<int, string> GetAllFileNames()
        {
            return BlissKomDAL.SelectAllFileNames();
        }

        /// <summary>
        /// Hämtar och returnerar en associativ vecktor med alla kategoriers CatId respektive CatName.
        /// </summary>
        /// <returns>Associativ vektor med CatId (key) och CatName (value)</returns>
        public Dictionary<int, string> GetAllCategories()
        {
            return BlissKomDAL.SelectAllCategories();
        }

        /// <summary>
        /// Hämta och returnera kategori-info som hör till inskickat MeaningId. 
        /// CatId (key - den kategori Item:s tillhör) och
        /// CatRefId (value - den kategori ParentItem länkar till), om någon, annars null.
        /// </summary>
        /// <param name="meaningId">MeaningId</param>
        /// <returns>CatId (key) och eventuellt CatRefId (value), annars null.</returns>
        public KeyValuePair<int, int?> GetCatInfoOfMeaning(Int16 meaningId)
        {
            return BlissKomDAL.SelectCatInfoOfMeaning(meaningId);
        }

        /// <summary>
        /// Hämta och returnera all positions-info som består av PosId och en strängrepresentation av respektive PosId.
        /// </summary>
        /// <returns>PosId (key), Strängrepresentation av PosId (value)</returns>
        public Dictionary<int, string> GetAllPositions()
        {
            return BlissKomDAL.SelectAllPositions();
        }

        /// <summary>
        /// Hämtar och returnerar PosId för Item med inskickat ItemId.
        /// </summary>
        /// <param name="itemId">ItemId</param>
        /// <returns>PosId</returns>
        public int GetPositionOfItem(Int16 itemId)
        {
            return BlissKomDAL.SelectPositionIdOfItem(itemId);
        }

        /// <summary>
        /// Spara nytt eller uppdatera befintligt Item.
        /// </summary>
        /// <param name="item">Item-objektet</param>
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

        /// <summary>
        /// Radera befintligt Item.
        /// </summary>
        /// <param name="itemId">ItemId för det Item-objekt som ska raderas.</param>
        public void DeleteItem(Int16 itemId)
        {
            BlissKomDAL.DeleteItem(itemId);
        }
    }
}