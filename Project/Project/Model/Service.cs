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
        //public System.Web.UI.WebControls.Image CurrentImage { get; set; }
        private BlissKomDAL communicationDAL;
        private IEnumerable<PageWordType> pageWordTypes;
        private PageCategory currentPageCategory;

        private PageCategory CurrentPageCategory
        {
            get { return currentPageCategory ?? (currentPageCategory = GetPageCategory(1, 1)); }
            set { currentPageCategory = value; }
        }

        private BlissKomDAL BlissKomDAL
        {
            get { return communicationDAL ?? (communicationDAL = new BlissKomDAL()); }
        }

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


        //public Color GetColorById(int wordTypeId)
        //{
        //    return ColorTranslator.FromHtml(GetColorHexCodeById(wordTypeId));
        //}

        //public string GetColorHexCodeById(int wTypeId)
        //{
        //    return PageWordTypes.First(pwt => pwt.ColorId == colorId).ColorRGBCode;
        //}

        public string GetColorRGBCodeOfPageWordType(int wTypeId)
        {
            return GetPageWordType(wTypeId).ColorRGBCode;
        }

        public Color GetColorOfPageWordType(int wTypeId)
        {
            return GetPageWordType(wTypeId).Color;
        }

        private PageWordType GetPageWordType(int wTypeId)
        {
            return PageWordTypes.First(pwt => pwt.WTypeId == wTypeId);
        }

        public Service()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        private IEnumerable<PageWordType> GetPageWordTypes()
        {
            return BlissKomDAL.SelectAllPageWordTypes();
        }
        //public void AddMeaning(string word, WordType wordType, string comment = "")

        public string GetCurrentCssTemplateName()
        {
            return CurrentPageCategory.GetCurrentCssTemplateName();
        }

        public int GetCurrentCategoryId()
        {
            return CurrentPageCategory.CatId;
        }

        public int GetCurrentPageNumber()
        {
            return CurrentPageCategory.CurrentPageNumber;
        }

        public PageItem GetNextLeftPageItem(PageItemType pit, int position, int meaningId)
        {
            var piu = CurrentPageCategory.CurrentPage.GetPageItemsUnit(meaningId);
            return piu.GetNextLeftPageItem(pit, position);
        }

        public PageItem GetNextRightPageItem(PageItemType pit, int position, int meaningId)
        {
            var piu = CurrentPageCategory.CurrentPage.GetPageItemsUnit(meaningId);
            return piu.GetNextRightPageItem(pit, position);
        }

        public IEnumerable<PageItemsUnit> GetCurrentPageItemsUnits()
        {
            return CurrentPageCategory.GetCurrentPageItemsUnits();
        }

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
            return BlissKomDAL.GetPageItemFileNames(meaningId);
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
    }
}