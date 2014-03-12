using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Model;
using System.Drawing;

namespace Project.PageModel
{
    //Den här klassens objekt motsvarar innehållet för en sida, inklusive alla
    //betydelser, bilder osv.
    public class PagePage
    {
        public IList<PageItemsUnit> PageItemsUnits { get; set; }
        public string CategoryName { get; set; }
        public int UnitsPerPage { get; set; }
        public string CssTemplateName { get; set; }
        public int PageNumber { get; set; }

        public PagePage()
        {
            //lite testvärden
            CategoryName = "Huvudmeny";
            UnitsPerPage = 10;
            CssTemplateName = "page-default";
            PageNumber = 1;
            PageItemsUnits = new List<PageItemsUnit>();
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "punkt.svg",
                        MeaningWord = "test",
                        MeaningId = 1,
                        Position = 1,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#fde885")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "utanfor.svg",
                        MeaningWord = "test",
                        MeaningId = 2,
                        Position = 2,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#f9c7af")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "toalett.svg",
                        MeaningWord = "test",
                        MeaningId = 3,
                        Position = 3,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#dce8b9")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "spegel.svg",
                        MeaningWord = "test",
                        MeaningId = 4,
                        Position = 4,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#d6ecf7")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "sjukhus.svg",
                        MeaningWord = "test",
                        MeaningId = 5,
                        Position = 5,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#dad5d2")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "God.svg",
                        MeaningWord = "test",
                        MeaningId = 1,
                        Position = 6,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#ffffff")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "hjarta.svg",
                        MeaningWord = "test",
                        MeaningId = 2,
                        Position = 7,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#fde885")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "hus.svg",
                        MeaningWord = "test",
                        MeaningId = 3,
                        Position = 8,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#f9c7af")
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageFileName = "sjo.svg",
                        MeaningWord = "test",
                        MeaningId = 4,
                        Position = 9,
                        PageImageType = PageImageType.Blissymbol,
                        BackGroundColor = ColorTranslator.FromHtml("#dce8b9")
                    }
                }
            });
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageFileName = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
        }
    }
}