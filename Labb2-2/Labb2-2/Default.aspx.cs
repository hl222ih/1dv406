using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb2_2.BLL;

namespace Labb2_2
{
    public partial class Default : System.Web.UI.Page
    {

        private Service service;

        private Service Service
        {
            get { return service ?? (service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    int myOut;
            //    var test = Service.GetContactsPageWise(10, 10, out myOut);
            //    var test2 = 1;
            //}
            //catch (ApplicationException ex)
            //{
            //}

        }

        private void HandleException(Exception ex)
        {
            if (ex.Data.Count > 0)
            {
                foreach (DictionaryEntry de in ex.Data)
                {
                    AddValidatorControl(String.Format("{0:-15}: {1}", de.Key.ToString(), de.Value));
                }
            }
            else
            {
                AddValidatorControl(ex.Message);
            }
        }

        private void AddValidatorControl(string message)
        {
            Validators.Add(new CustomValidator
            {
                IsValid = false,
                ErrorMessage = message
            });
        }

        public void lvContact_InsertItem(Contact contact)
        {
            try
            {
                Service.SaveContact(contact);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        public void lvContact_UpdateItem(int contactId)
        {
            try
            {
                var contact = Service.GetContact(contactId);

                if (contact == null)
                {
                    AddValidatorControl(String.Format("Kontakt med kontakt-id {0} kunde inte hittas.", contactId));
                    return;
                }
                Service.SaveContact(contact);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }


        public void lvContact_DeleteItem(int contactId)
        {
            try
            {
                Service.DeleteContact(contactId);

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void lvContact_DeleteItem(Contact contact)
        {
            try
            {
                Service.DeleteContact(contact);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void lvContact_GetData()
        {
            try
            {
                //Service.GetContacts();
                int myOut;
                Service.GetContactsPageWise(10, 10, out myOut);

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
    }
}