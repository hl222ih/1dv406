using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Labb2_2.Model.DAL
{
    public class ContactDAL : DALbase
    {
        //radera en kontakt från databasen
        public void DeleteContact(int contactId)
        {
            //skapa en connection till databasen
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
                    throw new ApplicationException("An error occurred while removing the contact from the database.");
                }
            }
        }

        //hämta en kontakt från databasen
        public Contact GetContactById(int contactId)
        {
            //skapa anslutningsobjekt
            using (var conn = CreateConnection())
            {
                try
                {
                    //skapa SqlCommand-objekt för att köra lagrad procedur Person.uspGetContact
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Value = contactId;

                    //öppna anslutningen till databasen
                    conn.Open();

                    //skapa SqlDataReader-objekt som reader refererar till
                    using (var reader = cmd.ExecuteReader())
                    {
                        //om reader innehåller en post att läsa...
                        if (reader.Read())
                        {
                            //ta reda på de olika kolumnernas position
                            var contactIdIndex = reader.GetOrdinal("ContactId");
                            var firstNameIndex = reader.GetOrdinal("FirstName");
                            var lastNameIndex = reader.GetOrdinal("LastName");
                            var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                            //läs in värdena från posten och returnera som en instans av Contact-klassen.
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

        //hämta en lista med alla kontakter, från databasen
        public IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(20000); //<-- det antal poster som finns i databasen, ungefär

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        //läs in positionerna före while-satsen så att de inte behöver läsas in gång på gång
                        var contactIdIndex = reader.GetOrdinal("ContactId");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            //skapar och lägger till Contact-objekt i listan contacts.
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

        //hämta ett antal (maximumRows) kontakter från databasen med start från kontaktnummer startRowIndex
        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            totalRowCount = 0;

            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(maximumRows); //maximumRows är antalet som hämtas varje gång, alltså poster per sida.

                    var cmd = new SqlCommand("Person.uspGetContactsPageWise", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                    //inparametern för den lagrade proceduren tar sidindex, metoden tar radindex/postindex. 
                    //räknar om post-index till sidindex (och behövdes visst lägga till en 1 för att det skulle bli rätt).
                    cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
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

        //lägga till en kontakt i databasen
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

                    //inte säker på att InputOutput gör nån nytta, Output hade nog räckt.
                    cmd.Parameters.Add("@ContactId", SqlDbType.Int, 4).Direction = ParameterDirection.InputOutput;
                    //och denna är ganska meningslös...
                    cmd.Parameters["@ContactId"].Value = contact.ContactID;
                    //...men det funkar nu iallafall så jag ändrar inte.

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    //kontaktID:t som av databasen tilldelats posten lagras i Contact-objektet.
                    contact.ContactID = (int)cmd.Parameters["@ContactId"].Value;
                }
                catch
                {
                    throw new ApplicationException("An error occurred while inserting the contact in the database.");
                }
            }
        }

        //uppdatera en kontakt i databasen (som har samma ContactID som den inskickade kontakten)
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