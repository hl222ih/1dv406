﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Project.PageModel;

namespace Project.Model.DAL
{
    public class CommunicationDAL : DALBase
    {

        public IEnumerable<PageWordType> SelectAllPageWordTypes()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var pageWordTypes = new List<PageWordType>();

                    var cmd = new SqlCommand("appSchema.usp_SelectAllPageWordTypes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var wTypeIdIndex = reader.GetOrdinal("WTypeId");
                        var colorIdIndex = reader.GetOrdinal("ColorId");
                        var wTypeIndex = reader.GetOrdinal("WType");
                        var colorRGBCodeIndex = reader.GetOrdinal("ColorRGBCode");

                        while (reader.Read())
                        {
                            pageWordTypes.Add(new PageWordType
                            {
                                WTypeId = (int)reader.GetByte(wTypeIdIndex),
                                ColorId = (int)reader.GetByte(colorIdIndex),
                                WType = reader.GetString(wTypeIndex),
                                ColorRGBCode = reader.GetString(colorRGBCodeIndex)
                            });
                        }
                    }

                    return pageWordTypes;

                }
                catch
                {
                    throw new ApplicationException("Misslyckades med att hämta information från databasen.");
                }
            }
        }

        public IEnumerable<Meaning> SelectAllMeanings()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var meanings = new List<Meaning>();

                    var cmd = new SqlCommand("appSchema.usp_SelectAllMeanings", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var meaningIdIndex = reader.GetOrdinal("MeaningId");
                        var wTypeIdIndex = reader.GetOrdinal("WTypeId");
                        var wordIndex = reader.GetOrdinal("Word");
                        var commentIndex = reader.GetOrdinal("Comment");

                        while (reader.Read())
                        {
                            meanings.Add(new Meaning
                            {
                                MeaningId = reader.GetInt16(meaningIdIndex),
                                WTypeId = reader.GetByte(wTypeIdIndex),
                                Word = reader.GetString(wordIndex),
                                Comment = reader.GetString(commentIndex)
                            });
                        }
                    }

                    return meanings;

                }
                catch
                {
                    throw new ApplicationException("Misslyckades med att hämta information från databasen.");
                }
            }
        }

        public IEnumerable<Item> SelectAllItems()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var items = new List<Item>();

                    var cmd = new SqlCommand("appSchema.usp_SelectAllItems", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {

                        var itemIdIndex = reader.GetOrdinal("ItemId");
                        var meaningIdIndex = reader.GetOrdinal("MeaningId");
                        var imageIdIndex = reader.GetOrdinal("ImageId");
                        var catRefIdIndex = reader.GetOrdinal("CatRefId");
                        var catIdIndex = reader.GetOrdinal("CatId");
                        var posIdIndex = reader.GetOrdinal("PosId");

                        while (reader.Read())
                        {
                            items.Add(new Item
                            {
                                ItemId = reader.GetInt16(itemIdIndex),
                                MeaningId = reader.GetInt16(meaningIdIndex),
                                ImageId = reader.GetInt16(imageIdIndex),
                                CatRefId = reader.GetInt16(catRefIdIndex),
                                CatId = reader.GetInt16(catIdIndex),
                                PosId = reader.GetByte(posIdIndex)
                            });
                        }
                    }

                    return items;

                }
                catch
                {
                    throw new ApplicationException("Misslyckades med att hämta information från databasen.");
                }
            }
        }


        public PageCategory SelectPartialCategory(int categoryId, int pageNumber)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    PageCategory pageCategory = new PageCategory
                    {
                        CatId = categoryId,
                        CurrentPageNumber = pageNumber
                    };
                    var pageItems = new List<PageItem>();

                    var cmd = new SqlCommand("appSchema.usp_SelectFullPage", conn);
                    cmd.Parameters.Add("@CatId", SqlDbType.SmallInt, 2);
                    cmd.Parameters.Add("@CatPageNum", SqlDbType.TinyInt, 1);
                    cmd.Parameters["@CatId"].Value = (Int16)categoryId;
                    cmd.Parameters["@CatPageNum"].Value = (byte)pageNumber;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {

                        var backGroundColorIndex = reader.GetOrdinal("BackGroundColor");
                        var pageWTypeIndex = reader.GetOrdinal("PageWType");
                        var meaningWordIndex = reader.GetOrdinal("MeaningWord");
                        var meaningCommentIndex = reader.GetOrdinal("MeaningComment");
                        var meaningIdIndex = reader.GetOrdinal("MeaningId");
                        var pageItemTypeIndex = reader.GetOrdinal("PageItemType");
                        var positionIndex = reader.GetOrdinal("Position");
                        var pageImageTypeIndex = reader.GetOrdinal("PageImageType");
                        var imageCommentIndex = reader.GetOrdinal("ImageComment");
                        var imageFileNameIndex = reader.GetOrdinal("ImageFileName");
                        var catNameIndex = reader.GetOrdinal("CatName");
                        var catRefIdIndex = reader.GetOrdinal("CatRefId");
                        var cssTemplateNameIndex = reader.GetOrdinal("CssTemplateName");

                        while (reader.Read())
                        {
                            var pageItemType = reader.GetString(pageItemTypeIndex);
                            var catRefId = reader.IsDBNull(catRefIdIndex) ? 
                                null : (int?)reader.GetInt16(catRefIdIndex);
                            PageItem pageItem;

                            if (pageItemType.Equals("Parent"))
                            {
                                if (catRefId == null)
                                {
                                    pageItem = new PageParentWordItem()
                                    {
                                    };
                                }
                                else
                                {
                                    pageItem = new PageParentCategoryItem()
                                    {
                                        LinkToCategoryId = (int)catRefId
                                    };
                                }
                            }
                            else if (pageItemType.Equals("LeftChild"))
                            {
                                pageItem = new PageChildWordItem()
                                {
                                    PageItemType = PageItemType.ChildLeftWordItem
                                };
                            }
                            else if (pageItemType.Equals("RightChild"))
                            {
                                pageItem = new PageChildWordItem()
                                {
                                    PageItemType = PageItemType.ChildRightWordItem
                                };
                            }
                            else
                            {
                                //måste tillhöra en typ, skippa annars aktuellt pageitem
                                continue;
                            }

                            pageItem.BackGroundRGBColor = reader.GetString(backGroundColorIndex);
                            pageItem.MeaningWord = reader.GetString(meaningWordIndex);
                            pageItem.MeaningComment = reader.GetString(meaningCommentIndex);
                            pageItem.MeaningId = (int)reader.GetInt16(meaningIdIndex);
                            pageItem.Position = (int)reader.GetInt16(positionIndex);
                            switch (reader.GetString(pageImageTypeIndex))
                            {
                                case "Blissymbol":
                                    pageItem.PageImageType = PageImageType.Blissymbol;
                                    break;
                                case "SignLanguage":
                                    pageItem.PageImageType = PageImageType.SignLanguage;
                                    break;
                                case "Photo":
                                    pageItem.PageImageType = PageImageType.Photo;
                                    break;
                            }
                            pageItem.ImageComment = reader.GetString(imageCommentIndex);
                            pageItem.ImageFileName = reader.GetString(imageFileNameIndex);
                            pageItem.CssTemplateName = reader.GetString(cssTemplateNameIndex);
                            
                            pageItems.Add(pageItem);
                        }


                    }

                    pageCategory.CurrentPage.PageItemsUnits = pageItems
                        .GroupBy(pi => pi.MeaningId)
                        .Select(group => new PageItemsUnit
                            {
                                PageItems = group.ToList()
                            }).ToList();

                    return pageCategory;
                }
                catch
                {
                    throw new ApplicationException("Misslyckades med att hämta information från databasen.");
                }
            }
        }
    }
}