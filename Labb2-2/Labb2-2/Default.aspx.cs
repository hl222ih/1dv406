using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb2_2.Model;

namespace Labb2_2
{
    public partial class Default : System.Web.UI.Page
    {

        private Service service;
        

        private Service Service
        {
            //returnera Service-objekt. Om sådant inte finns, skapa det först.
            get { return service ?? (service = new Service()); }
        }

        //Lagra successmessage (att en post raderads, skapats, ändrats) så att den kan visas vid nästa postback.
        public string SuccessMessage
        {
            get
            {
                return Session["SuccessMessage"] as string;
            }
            private set
            {
                Session["SuccessMessage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SuccessMessage))
            {
                ShowSuccessMessage();                
            }

        }

        //fixar så att meddelande från Exceptions som uppstår visas i error-rutan.
        //oavsett om felmeddelanden lagrats i ex.Data eller i ex.Message
        private void HandleException(Exception ex)
        {
            if (ex.Data.Count > 0)
            {
                foreach (DictionaryEntry de in ex.Data)
                {
                    ModelState.AddModelError(de.Key.ToString(), de.Value.ToString());
                    // funkar inte eftersom det är ModelState-errors som visas här.
                    //AddValidatorControl(String.Format("{0:-15}: {1}", de.Key.ToString(), de.Value));
                }
            }
            else
            {
                ModelState.AddModelError("", ex.Message);
                //// funkar inte eftersom det är ModelState-errors som visas här.
                //AddValidatorControl(ex.Message);
            }
        }

        //används inte.
        private void AddValidatorControl(string message)
        {
            Validators.Add(new CustomValidator
            {
                IsValid = false,
                ErrorMessage = message
            });
            SuccessMessage = String.Empty;
        }

        //visar meddelande om lyckad operation. det exakta meddelandet har tidigare sparats i SuccessMessage-sessionsvariabeln.
        private void ShowSuccessMessage()
        {
            pnlConfirmBox.Visible = true;
            pnlConfirmBox.Controls.Add(new LiteralControl(SuccessMessage));
            SuccessMessage = String.Empty;               
        }

        public void lvContact_InsertItem(Contact contact)
        {
            Validate("InsertGroup"); //causesvalidation=true på "lägg till"-kontrollen verkar bara validera mot modelstate
            if (IsValid)
            {
                //Om ModelState inte validerar måste felmeddelanden visas.
                //ShowModelStateErrors är satt till false i utgångsläget för att inte visa både meddelanden
                //från validation-kontrollerna och från modelstatevalideringen samtidigt.
                //Om validation-kontrollerna validerar till true måste modelstateerrors visas.
                vsErrors1.ShowModelStateErrors = true;

                if (ModelState.IsValid)
                {
                    try
                    {
                        Service.SaveContact(contact);
                        SuccessMessage = "Kontakten har lagts till.";
                        ShowSuccessMessage();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
                else
                {
                }
            }
            else
            {
                //sätt showmodelstateerrors till false igen om validation-kontrollerna inte valideras till true.
                vsErrors1.ShowModelStateErrors = false;
            }
            
        }


        public void lvContact_UpdateItem(Contact contact)
        {
            Validate("EditGroup"); //causesvalidation=true på "lägg till"-kontrollen verkar bara validera mot modelstate
            if (IsValid)
            {
                //Om ModelState inte validerar måste felmeddelanden visas.
                //ShowModelStateErrors är satt till false i utgångsläget för att inte visa både meddelanden
                //från validation-kontrollerna och från modelstatevalideringen samtidigt.
                //Om validation-kontrollerna validerar till true måste modelstateerrors visas.
                vsErrors2.ShowModelStateErrors = true;

                if (ModelState.IsValid)
                {
                    try
                    {
                        var existingContact = Service.GetContact(contact.ContactID);

                        if (contact == null)
                        {
                            ModelState.AddModelError("", String.Format("Kontakt med kontakt-id {0} kunde inte hittas.", contact.ContactID));
                            //AddValidatorControl(String.Format("Kontakt med kontakt-id {0} kunde inte hittas.", contact.ContactID));
                            return;
                        }

                        Service.SaveContact(contact);
                        SuccessMessage = "Kontakten har uppdaterats.";
                        ShowSuccessMessage();
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                }
            }
            else
            {
                //sätt showmodelstateerrors till false igen om validation-kontrollerna inte valideras till true.
                vsErrors2.ShowModelStateErrors = false;
            }
        }


        public void lvContact_DeleteItem(int contactId)
        {
            try
            {
                Service.DeleteContact(contactId);
                SuccessMessage = "Kontakten har tagits bort.";
                ShowSuccessMessage();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        //hämta en sida full med kontakter.
        //maximumRows är antal poster som ska hämtas (som mest)
        //startRowIndex är från och med vilken post som ska hämtas
        //totalRowCount förstår jag inte vad det är för något, men tytligen något asp.net vet.
        public IEnumerable<Contact> lvContact_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                return Service.GetContactsPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                //måste tydligen ha ett värde.
                totalRowCount = 0;
                return null;
            }
        }
    }
}