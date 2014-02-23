using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Labb2_2.DAL;

namespace Labb2_2.BLL
{
    public class Service
    {

        private ContactDAL contactDAL;

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

        private void ValidateContact(Contact contact)
        {
            var ex = new ArgumentException();

            if (contact.EmailAddress == null)
            {
            }
            else
            {
                if (contact.EmailAddress.Length > 50)
                {
                    ex.Data.Add("EmailAddressLength", "The e-mail address is too long");
                }
            }
            if (contact.FirstName.Length > 50)
            {
                ex.Data.Add("FirstNameLength", "The first name is too long");
            }
            if (contact.LastName.Length > 50)
            {
                ex.Data.Add("LastNameLength", "The last name is too long");
            }
            

            if (ex.Data.Count > 0)
            {
                throw ex;
            }
        }
    }
}