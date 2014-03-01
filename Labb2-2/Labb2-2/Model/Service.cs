using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb2_2.Model.DAL;
using System.Text.RegularExpressions;

namespace Labb2_2.Model
{
    public class Service
    {

        //instans av ContactDAL-klassen
        private ContactDAL contactDAL;

        //skapar contactDal om den inte finns
        private ContactDAL ContactDAL
        {
            get { return contactDAL ?? (contactDAL = new ContactDAL()); }
        }

        //default-konstruktor (skapas eg. automatiskt)
        public Service() : base()
        {
        }

        public void DeleteContact(int contactId)
        {
            ContactDAL.DeleteContact(contactId);
        }

        public void DeleteContact(Contact contact)
        {
            ContactDAL.DeleteContact(contact.ContactID);
        }

        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return ContactDAL.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public void SaveContact(Contact contact)
        {
            ValidateContact(contact);

            if (contact.ContactID == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }

        //Validerar Contact-objektet, skulle behöva förbättras på vissa punkter
        //Lagrar olika felmeddelanden i en Exception som kastas om valideringen inte går igenom.
        private void ValidateContact(Contact contact)
        {

            var ex = new ArgumentException();
            var reEmail = new Regex(@"^[a-z][a-z0-9_\-~]*(\.[a-z0-9_\-~]+)?@([a-z0-9_\-]+\.){1,2}[a-z]{2,6}$");
            var reName = new Regex(@"^[a-zA-ZåäöÅÄÖ\- ]+$");

            if (contact.EmailAddress == null)
            {
                ex.Data.Add("EmailAddressEmpty", "E-mail address is required");
            }
            else if (contact.EmailAddress.Length > 50)
            {
                ex.Data.Add("EmailAddressLength", "E-mail address is too long");
            }
            else if (!reEmail.IsMatch(contact.EmailAddress))
            {
                ex.Data.Add("FirstNameCharacters", "E-mail address is not valid.");
            }
            if (contact.FirstName.Length > 50)
            {
                ex.Data.Add("FirstNameLength", "The first name is too long");
            }
            else if (!reName.IsMatch(contact.FirstName))
            {
                ex.Data.Add("FirstNameCharacters", "The first name contains invalid characters.");
            }
            if (contact.LastName.Length > 50)
            {
                ex.Data.Add("LastNameLength", "The last name is too long");
            }
            else if (!reName.IsMatch(contact.LastName))
            {
                ex.Data.Add("LastNameCharacters", "The last name contains invalid characters.");
            }            

            if (ex.Data.Count > 0)
            {
                throw ex;
            }
        }
    }
}