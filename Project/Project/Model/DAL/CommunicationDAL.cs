using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Project.Model.DAL
{
    public class CommunicationDAL : DALBase
    {

        public IEnumerable<WordType> SelectAllWordTypes()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var wordTypes = new List<WordType>();

                    var cmd = new SqlCommand("appSchema.usp_SelectAllWordTypes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var wTypeIdIndex = reader.GetOrdinal("WTypeId");
                        var colorIdIndex = reader.GetOrdinal("ColorId");
                        var wTypeIndex = reader.GetOrdinal("WType");

                        while (reader.Read())
                        {
                            wordTypes.Add(new WordType
                            {
                                WTypeId = reader.GetByte(wTypeIdIndex),
                                ColorId = reader.GetByte(colorIdIndex),
                                WType = reader.GetString(wTypeIndex)
                            });
                        }
                    }

                    return wordTypes;

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
    }
}