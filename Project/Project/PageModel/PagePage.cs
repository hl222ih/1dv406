using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                        ImageId = 1,
                        MeaningWord = "test",
                        PageItemId = 1,
                        Position = 1
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 2,
                        MeaningWord = "test",
                        PageItemId = 2,
                        Position = 2
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 3,
                        MeaningWord = "test",
                        PageItemId = 3,
                        Position = 3
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 4,
                        MeaningWord = "test",
                        PageItemId = 4,
                        Position = 4
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 5,
                        MeaningWord = "test",
                        PageItemId = 5,
                        Position = 5
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 1,
                        MeaningWord = "test",
                        PageItemId = 1,
                        Position = 6
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 2,
                        MeaningWord = "test",
                        PageItemId = 2,
                        Position = 7
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 3,
                        MeaningWord = "test",
                        PageItemId = 3,
                        Position = 8
                    }
                }
            });
            PageItemsUnits.Add(new PageItemsUnit
            {
                PageItems = new List<PageItem>{
                    new PageParentCategoryItem { 
                        ImageComment = "",
                        MeaningComment = "",
                        ImageId = 4,
                        MeaningWord = "test",
                        PageItemId = 4,
                        Position = 9
                    }
                }
            });
            //PageItemsUnits.Add(new PageItemsUnit
            //{
            //    PageItems = new List<PageItem>{
            //        new PageParentCategoryItem { 
            //            ImageComment = "",
            //            MeaningComment = "",
            //            ImageId = 1,
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
            //            ImageId = 1,
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
            //            ImageId = 1,
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
            //            ImageId = 1,
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
            //            ImageId = 1,
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
            //            ImageId = 1,
            //            MeaningWord = "test",
            //            PageItemId = 1,
            //            Position = 1
            //        }
            //    }
            //});
        }
    }
}