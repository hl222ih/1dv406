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
        private CommunicationDAL communicationDAL;
        private IEnumerable<PageWordType> pageWordTypes;
        private PageCategory currentPageCategory;

        private PageCategory CurrentPageCategory
        {
            get { return currentPageCategory ?? (currentPageCategory = GetPageCategory(1, 1)); }
            set { currentPageCategory = value; }
        }

        private CommunicationDAL CommunicationDAL
        {
            get { return communicationDAL ?? (communicationDAL = new CommunicationDAL()); }
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
            return CommunicationDAL.SelectAllPageWordTypes();
        }
        //public void AddMeaning(string word, WordType wordType, string comment = "")

        public string GetCurrentCssTemplateName()
        {
            return CurrentPageCategory.GetCurrentCssTemplateName();
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

            var pageItems = CommunicationDAL.SelectPageItemsOfPage(categoryId, pageNumber);
    
            pageCategory.CurrentPage.PageItemsUnits = pageItems
                .GroupBy(pi => pi.MeaningId)
                .Select(group => new PageItemsUnit
                {
                    PageItems = group.ToList()
                }).ToList();

            //test
            //pageCategory.CurrentPage.UnitsPerPage = 2;
            pageCategory.CurrentPage.CssTemplateName = "page-five-eight";

            //slut test

            return pageCategory;
        }
        public void UpdatePageCategory(int categoryId, int pageNumber = 1)
        {
            currentPageCategory = GetPageCategory(categoryId, pageNumber);
        }

        public IEnumerable<PageItem> GetPageItems(int categoryId, int pageNumbers)
        {
            return CommunicationDAL.SelectPageItemsOfPage(categoryId, pageNumbers);
        }
    }
}