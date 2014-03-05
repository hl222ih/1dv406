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
                            //var test = reader.GetInt32(wTypeIdIndex);
                        

                            wordTypes.Add(new WordType
                            {
                                WTypeId = (int)reader.GetByte(wTypeIdIndex),
                                ColorId = (int)reader.GetByte(colorIdIndex),
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
    }
}