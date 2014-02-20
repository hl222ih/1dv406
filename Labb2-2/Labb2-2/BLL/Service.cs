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

        public Service() : base()
        {
        }

        public void DeleteContact(int contactId)
        {
        }

        public void DeleteContact(Contact contact)
        {
        }

        public Contact GetContact(int contactId)
        {
        }

        public IEnumerable<Contact> GetContacts()
        {
        }

        public IEnumerable<Contact> GetContactsPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
        }

        public void SaveContact(Contact contact)
        {
        }
    }
}