using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb2_2.BLL; //men borde ju inte få referera till BLL egentligen :/
using System.Data;
using System.Data.SqlClient;

namespace Labb2_2.DAL
{
    public class ContactDAL : DALbase
    {

        public void DeleteContact(int contactId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                }
            }
        }

        public Contact GetContactById(int contactId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var contactIdIndex = reader.GetOrdinal("ContactId");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                            return new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("An error occurred while retrieving the contact from the database.");
                }
            }
        }

        public IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });
                        }

                    }
                    contacts.TrimExcess();

                    return contacts;
                }

                catch
                {
                    throw new ApplicationException("An error occurred while retrieving contacts from the database.");
                }
            }
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            totalRowCount = 0;

            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(maximumRows);

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex;
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters["@RecordCount"].Value = totalRowCount;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;


                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var contactIdIndex = reader.GetOrdinal("ContactId");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                            contacts.Add(new Contact
                            {
                                ContactID = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            });

                        }

                        contacts.TrimExcess();

                        return contacts;

                    }
                }
                catch
                {
                    throw new ApplicationException("An error occurred while retrieving contacts from the database.");
                }
            }
        }

        public void InsertContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    //hämtar ContactId som av databasen tilldelas contact
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Direction = ParameterDirection.InputOutput;

                    cmd.Parameters["@ContactId"].Value = contact.ContactID;


                    conn.Open();
                    cmd.ExecuteNonQuery();

                    contact.ContactID = (int)cmd.Parameters["@ContactId"].Value;
                }
                catch
                {
                    throw new ApplicationException("An error occurred while inserting the contact in the database.");
                }
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Value = contact.ContactID;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occurred while updating the contact in the database.");
                }
            }
        }
    }
}