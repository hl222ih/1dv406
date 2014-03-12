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
        private PagePage currentPage;

        private PagePage CurrentPage
        {
            get { return currentPage ?? (currentPage = new PagePage()); }
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
                    //hårdkodade värden från databasens Color-tabell.
                    var colors = new Dictionary<int, string>() { 
                        { 1, "#fde885" }, { 2, "#f9c7af" }, { 3, "#dce8b9" },
                        { 4, "#d6ecf7" }, { 5, "#dad5d2" }, { 6, "#ffffff" } };

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

        public string GetCssTemplateName()
        {
            return CurrentPage.CssTemplateName;
        }

        public IEnumerable<PageItemsUnit> GetPageItemsUnits()
        {
            return CurrentPage.PageItemsUnits.AsEnumerable();
        }
    }
}