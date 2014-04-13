using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Project.Model;
using Project.PageModel;
using System.Web.DynamicData;
using System.Web.Services;
using System.Web.Script.Services;
using System.ComponentModel.DataAnnotations;

namespace Project
{
    public partial class _Default : Page
    {
        /// <summary>
        /// Egenskap som, om det finns, returnerar innehållet i en Sessionsvariabel
        /// innehåller hela Service-objektet, annars skapar det ett nytt Service-objekt
        /// som returneras.
        /// </summary>
        private Service Service
        {
            get
            {
                if (Session["Service"] == null)
                {
                    Session["Service"] = new Service();
                }
                return Session["Service"] as Service;
            }
        }
        
        /// <summary>
        /// Sessionsvariabel för om applikationen är i inställningsläge (eller i användningsläge)
        /// </summary>
        private bool IsSettingsMode
        {
            get { return (Session["IsSettingsMode"] != null && Session["IsSettingsMode"].ToString() == "True"); }
            set { Session["IsSettingsMode"] = value.ToString(); }
        }

        /// <summary>
        /// Senaste valda värdet hos lstItems-kontrollen. Obs. returnerar null om inget tidigare värde är valt.
        /// </summary>
        private string LstItemValue
        {
            get { return (Session["lstItemsValue"] != null ? Session["lstItemsValue"].ToString() : null); }
            set { Session["lstItemsValue"] = value; }
        }

        /// <summary>
        /// Tillfällig (under sidladdningsfasen) lagring av ett eventuellt "success"-meddelande.
        /// </summary>
        private string successMessage;

        /// <summary>
        /// Körs vid varje sidladdning. Justerar CSS så att inställningslagret
        /// eller användningslagret visas.
        /// Registrerar onclickhändelser för navigeringsknappar som inte är
        /// kopplade till specifika "ItemsUnits".
        /// 
        /// Här kan man ställa in så att klientvalidering stängs av.
        /// (sätt disableClientScript till true)
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            //Om applikationen är i inställningsläge, visa inställningar i förgrunden,
            //annars, lägg inställningar i bakgrunden
            if (IsSettingsMode)
            {
                pnlForm.Attributes.CssStyle["z-index"] = "10 !important";
                imbHome2.Attributes.CssStyle["z-index"] = "11 !important";
            }
            else
            {
                pnlForm.Attributes.CssStyle["z-index"] = "-10 !important";
                imbHome2.Attributes.CssStyle["z-index"] = "-10 !important";
            }

            //registrera händelsehanterare för hemknapp från underkategorier
            imbHome.Click += new ImageClickEventHandler(imbHome_Click);
            //registrera händelsehanterare för hemknapp från inställningar (återgång till användningsläge)
            imbHome2.Click += new ImageClickEventHandler(imbHome_Click);
            //registrera händelsehanterare för OK-knapp (återgång till startsidan i användningsläge)
            imbOK.Click += new ImageClickEventHandler(imbOK_Click);


            //I debug-syfte: Inaktivera client side validation genom att
            //sätta värdet på disableClientScript till true.
            var disableClientScript = true;

            if (disableClientScript)
            {
                rfvWord.EnableClientScript = false;
                rfvPosition.EnableClientScript = false;
                rfvCategory.EnableClientScript = false;
                rfvFileName.EnableClientScript = false;
                rfvMeaning.EnableClientScript = false;
            }

        }

        /// <summary>
        /// Körs vid varje sidladdning. Rendera bara bilder för användningsläget om applikationen
        /// inte är i inställningsläget.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Optimering. Rendera bara bilder osv. om applikationen inte är i inställningsläge
            if (!IsSettingsMode)
            {
                RenderImages();
            }            
        }

        /// <summary>
        /// Körs vid varje sidladdning. 
        /// Registrerar startupp-script och
        /// synliggör meddelandeboxar om det finns meddelanden att visa.
        /// </summary>
        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            SetBackgroundColorsToDdlPageWordType();
            ClientScriptManager csm = Page.ClientScript;

            //användarvänlighet, lstMeaning görs tydligare otillgänglig på klientsidan 
            //(tillgänglig som default).
            if (!lstMeaning.Enabled)
            {
                csm.RegisterStartupScript(this.GetType(), "DisableControl", "BlissKom.disableControl('Content_lstMeaning')", true);
            }
            //användarvänlighet, ddlCategoryLink tydligare otillgänglig på klientsidan (default).
            //här görs den tillgänglig igen om chkIsCategory är ikryssad.
            if (chkIsCategory.Checked)
            {
                csm.RegisterStartupScript(this.GetType(), "EnableControl", "BlissKom.enableControl('Content_ddlCategoryLink')", true);
            }

            //visar pnlErrorBox om ModelStateErrors existerar.
            if (!ModelState.IsValid)
            {
                pnlErrorBox.Style["display"] = "block";
            }

            if (successMessage != null)
            {
                lblSuccess.Text = successMessage;
                pnlSuccessBox.Style["display"] = "block";
                csm.RegisterStartupScript(this.GetType(), "DimSimple", "BlissKom.dimSimple(true)", true);
            }

            if (lstMeaning.SelectedIndex > -1)
            {
                btnUpdateMeaning.Text = "Uppdatera";
            }

            if (lstItem.SelectedIndex > -1)
            {
                btnUpdateItem.Text = "Uppdatera";
            }
        }

        /// <summary>
        /// Körs innan varje sida avslutas. Sparar värdet för lstItem så att den kan testa
        /// om en "äkta" SelectedIndexChanged sker.
        /// </summary>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (lstItem.SelectedIndex > -1)
            {
                LstItemValue = lstItem.SelectedItem.Text;
            }
        }

        /// <summary>
        /// Hämtar all info som hör till tidigare vald sida (genom klick-eventet för viss kategorilänk)
        /// Renderar alla bilder (ParentItems) som hör till den aktuella sidan och registrerar klick-event för dessa.
        /// Körs bara då en ny sida visas. Varje enhet med bilder (PageItemUnit) placeras i en egen UpdatePanel så
        /// att endast den delen hämtas asynkront vid klick vilket snabbar upp lite och undviker flicker.
        /// </summary>
        protected void RenderImages()
        {
            //lägger till händelsehanterar för cancel-knappen. Gömmer den förstorade bildrutan och tar bort
            //dimningseffekten och återställer bilden till ursprungsläget.
            imbCancel.OnClientClick = "BlissKom.toggleNavButtons(false, false, false, false, false); BlissKom.undim(); return false;";

            //lägger till händelsehanterare (javascript) för om användaren klickar på vänster-
            //respektive högerknappen som visas vid förstorad bild (om det finns fler bilder
            //som hör till samma ItemsUnit.
            imbLeft.OnClientClick = "BlissKom.showLeftImage(); return false;";
            imbRight.OnClientClick = "BlissKom.showRightImage(); return false;";

            //osynliggör navigeringsknappar som inte används i aktuell vy.
            imbOK.Style["display"] = "none";
            imbCancel.Style["display"] = "none";
            imbLeft.Style["display"] = "none";
            imbRight.Style["display"] = "none";
            imbInfo.Style["display"] = "none";

            //osynliggör hem-knappen om startsidan är den som visas. 
            //(Visa isåfall inställningsknappen istället.)
            //annars, osynliggör inställningsknappen (och visa hem-knappen).
            if (Service.GetCurrentCategoryId() == 1 && Service.GetCurrentPageNumber() == 1)
            {
                imbHome.Style["display"] = "none";
            }
            else
            {
                imbSettings.Style["display"] = "none";
            }

            //hämta den mall som gäller för aktuell sida (hur bilderna ska visas - position/storlek osv.
            //Site.css innehåller olika stilmallar för de olika css-mall-namnen.
            var cssTemplateName = Service.GetCurrentCssTemplateName();
            //hämta de ItemsUnits, dvs de bilder och underbilder med info som hör till aktuell sida.
            var pageItemsUnits = Service.GetCurrentPageItemsUnits();
            
            var counter = 0;

            //kopplar text, bild och händelsehanterare till varje PageItemUnit av aktuell sida.
            foreach (var piu in pageItemsUnits)
            {
                counter++;
                var pi = piu.GetPageParentItem();

                //om pageitemunit saknar en pageparentitem, hoppa till nästa pageitemunit
                if (pi == null)
                {
                    continue;
                }

                var lb = new LinkButton()
                {
                    ID = String.Format("imbUnit{0}", pi.Position),
                    CssClass = String.Format("item {0}", cssTemplateName),
                    BackColor = pi.BackGroundColor,
                    CausesValidation = false
                };
                lb.Attributes.Add("mId", pi.MeaningId.ToString());
                
                //skapa ett Label-objekt innehållande ordet för aktuellt PageItem
                var lbl = new Label()
                {
                    Text = pi.MeaningWord
                };

                //skapa ett Image-objekt innehållande bildlänken för aktuellt PageItem.
                var img = new Image()
                {
                    ImageUrl = String.Format("~/Images/ComPics/{0}", pi.ImageFileName)
                };
                img.Attributes.Add("data-type", pi.PageItemType.ToString());
                img.Attributes.Add("data-pos", pi.Position.ToString());

                //Ser till att navigeringspilar visas åt höger respektive vänster om det finns bilder där.
                //Inte helt optimal logik i databasen gör att det blir lite krystat här.
                //Kopplar händelsehanterare till javascript som körs för att dimma bakgrunden, visa rätt bild
                //beroende på om användaren klickar vänster/höger-navigeringspil och att rätt navigeringsknappar
                //visas på den förstorade bilden.
                if (pi.PageItemType == PageItemType.ParentWordItem)
                {
                    var nextLeft = Service.GetNextLeftPageItem(pi.PageItemType, pi.Position, pi.MeaningId);
                    var hasNextLeft = (nextLeft != null);
                    var nextRight = Service.GetNextRightPageItem(pi.PageItemType, pi.Position, pi.MeaningId);
                    var hasNextRight = (nextRight != null);
                    var info = String.Format("{0}\n{1}", pi.MeaningComment, pi.ImageComment);
                    var hasInfo = (info != "\n");
                    lb.OnClientClick = String.Format("BlissKom.dim({0}); BlissKom.toggleNavButtons({1}, {2}, {3}, {4}, {5}); return false;", pi.Position, "true", "true", hasNextLeft ? "true" : "false", hasNextRight ? "true" : "false", hasInfo ? "true" : "false");
                }
                //om bildlänken länkar till en ny kategori/sida behövs inte ovanstående, 
                //däremot måste den nya sidan med bilder visas istället genom en full postback.
                //Anger vilken kategori som ska visas som catId. (borde vara data-catId för att vara html5-godkänd, men vill inte ändra nu)
                else if (pi.PageItemType == PageItemType.ParentCategoryItem)
                {
                    lb.Click += new EventHandler(lbParentCategoryItem_Click);
                    lb.Attributes.Add("catId", ((PageParentCategoryItem)pi).LinkToCategoryId.ToString());
                }

                //UpdatePanels läggs runt alla PageItemUnits, men det blir bara postback
                //om det är en kategorilänkbild som klickas. då förhindrar det att det blir
                //en dubbel postback. Om det är en vanlig "ord"-bild så sker navigeringen på
                //klientsidan.
                UpdatePanel upnl = new UpdatePanel();
                upnl.ContentTemplateContainer.Controls.Add(lb);
                lb.Controls.Add(lbl);
                lb.Controls.Add(img);
                phItems.Controls.Add(upnl);

                var pcis = piu.GetPageChildItems();

                
                foreach (var pci in pcis)
                {
                    var imgChild = new Image()
                    {
                        ImageUrl = String.Format("~/Images/ComPics/{0}", pci.ImageFileName)                 
                    };

                    imgChild.Style["display"] = "none";
                    imgChild.Attributes.Add("data-type", pci.PageItemType.ToString());
                    imgChild.Attributes.Add("data-pos", pci.Position.ToString());

                    lb.Controls.Add(imgChild);
                }
            }
        }

        /// <summary>
        /// Körs efter asynkron postback då användaren klickat på en kategorilänkbild.
        /// </summary>
        protected void lbParentCategoryItem_Click(object sender, EventArgs e)
        {
            var lb = ((LinkButton)sender);
            var test = lb.Attributes["catId"];
            Service.UpdatePageCategory(Convert.ToInt32(lb.Attributes["catId"]));
            //trigga omladdning av sidan för att den nya sidan ska visas.
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        /// <summary>
        /// Körs då användaren klickar på OK-knappen i en förstorad bild. Orsakar Postback som laddar startsidan med bilder.
        /// </summary>
        protected void imbOK_Click(object sender, ImageClickEventArgs e)
        {
            Service.UpdatePageCategory(1, 1);
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        /// <summary>
        /// Körs då användaren klickar på Home-knappen 
        /// (som visas när någon annan bild-sida än startsidan visas, eller på inställningssidan).
        /// Ser också till att inställningen är att navigationsläget är aktivt, om knappen klickats
        /// från inställningsläget.
        /// </summary>
        protected void imbHome_Click(object sender, ImageClickEventArgs e)
        {
            Service.UpdatePageCategory(1, 1);
            IsSettingsMode = false;
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        /// <summary>
        /// Hämtar ordtyper (ordklasser) och tillhörande färger för populerande av dropdownlista med dessa i inställningsläget.
        /// </summary>
        /// <returns>Samlingen med alla tillgängliga PageWordType:s.</returns>
        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            return Service.PageWordTypes;
        }

        /// <summary>
        /// Hämtar och sorterar betydelse-data för populerande av lista med betydelser i inställningsläget.
        /// </summary>
        /// <returns>Samlingen med alla tillgängliga Meaning:s</returns>
        public IEnumerable<Meaning> GetMeaningData()
        {
            return Service.GetMeanings().OrderBy(m => m.Word);
        }

        /// <summary>
        /// Tömmer lista med "Items" som representeras av dess respektive filnamn.
        /// Hämtar listan med de Items som hör till vald Meaning och resurnerar till för populerande av "Item"-lista.
        /// </summary>
        /// <returns>Samlingen med Items som hör till vald Meaning, om det finns några Items, annars null.</returns>
        public Dictionary<int, string> GetImageFileNameDataOfPageUnit()
        {
            lstItem.Items.Clear();
            if (lstMeaning.SelectedIndex > -1)
            {
                return Service.GetPageItemFileNames(Convert.ToInt16(lstMeaning.SelectedItem.Value));
            }
            return null;
        }

        /// <summary>
        /// Hämtar och sorterar alla tillgängliga bild-id:n med tillhörande filnamn.
        /// </summary>
        /// <returns>Samling med bild-id:n och tillhörande filnamn. Null om tom.</returns>
        public IEnumerable<KeyValuePair<int, string>> GetImageFileNameData()
        {
            if (Service.GetAllFileNames() != null)
            {
                return Service.GetAllFileNames().OrderBy(fn => fn.Value);
            }
            return null;
        }

        /// <summary>
        /// Körs när dropdown-listan för ordtyp bundits. Sätter bakgrundsfärger till de olika ordtyperna i dropdown-listan.
        /// </summary>
        protected void ddlPageWordType_DataBound(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        /// <summary>
        /// sätter bakgrundsfärg på ddlPageWordType:s listitems i enlighet med respektive ordtyp.
        /// </summary>
        private void SetBackgroundColorsToDdlPageWordType()
        {
            if (ddlPageWordType.Items.Count > 0)
            {
                foreach (ListItem li in ddlPageWordType.Items)
                {
                    li.Attributes.CssStyle.Add("background-color", Service.GetColorRGBCodeOfPageWordType(int.Parse(li.Value)));
                }
            }
        }

        /// <summary>
        /// Uppdaterar resterande listboxar och dropdown-boxar baserat på det valda värdet i lstMeaning.
        /// </summary>
        protected void lstMeaning_SelectedIndexChanged(object sender, EventArgs e)
        {
            //uppdatera bara beroende kontroller om lstMeaning-Listboxen orsakade postback
            //eller om användaren klickade på återställ-knappen
            if (GetControlCausingPostBack().Equals(lstMeaning) || sender.Equals(btnResetMeaning))
            {
                var meaning = Service.GetMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                txtWord.Text = meaning.Word;
                txtWordComment.Text = meaning.Comment;
                ddlPageWordType.ClearSelection();
                btnUpdateMeaning.Text = "Uppdatera";

                if (ddlPageWordType.Items.FindByValue(meaning.WTypeId.ToString()) != null)
                {
                    ddlPageWordType.Items.FindByValue(meaning.WTypeId.ToString()).Selected = true;
                }

                var catInfo = Service.GetCatInfoOfMeaning(meaning.MeaningId);

                ddlCategory.ClearSelection();
                if (ddlCategory.Items.FindByValue(catInfo.Key.ToString()) != null)
                {
                    ddlCategory.Items.FindByValue(catInfo.Key.ToString()).Selected = true;
                }

                chkIsCategory.Checked = (catInfo.Value != null);
                ddlCategoryLink.ClearSelection();
                ddlCategoryLink.Items.Clear();

                ddlPosition.ClearSelection();
                
            }
        }

        /// <summary>
        /// Uppdaterar beroende listboxar och dropdownlistor om en verklig ändring av index skett.
        /// </summary>
        protected void lstItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //index ändras vid varje omladdning av sidan.
            //effektivare om information bara hämtas när det valda lstItem verkligen ändrats.
            if (LstItemValue == null || LstItemValue != lstItem.SelectedItem.Text || sender.Equals(btnResetItem))
            {
                imgImage.ImageUrl = String.Format("~/Images/ComPics/{0}", lstItem.SelectedItem.Text);
                btnUpdateItem.Text = "Uppdatera";

                if (lstFileName.Items.Count > 1)
                {
                    lstFileName.ClearSelection();
                    lstFileName.Items.FindByText(lstItem.SelectedItem.Text).Selected = true;
                }

                if (ddlPosition.Items.Count > 0)
                {
                    var posInfo = Service.GetPositionOfItem(Convert.ToInt16(lstItem.SelectedItem.Value));
                    ddlPosition.ClearSelection();
                    ddlPosition.Items.FindByValue(posInfo.ToString()).Selected = true;

                    //endast ParentItems kan vara kategorilänkar, för närvarande PosId 1-30.
                    if (posInfo > 30)
                    {
                        chkIsCategory.Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// Kontrollerar vilken, om någon, kontroll som orsakade postback och returnerar den.
        /// </summary>
        /// <returns>
        /// kontrollen med det id som ges av __EVENTTARGET
        /// Om den inte finns, en "tom" kontroll.
        /// </returns>
        private Control GetControlCausingPostBack()
        {
            //Beroende på vilken kontroll som orsakar postback så kan olika saker behöva ske.
            return FindControl(Request.Params.Get("__EVENTTARGET")) ?? new Control();
        }

        /// <summary>
        /// Uppdaterar befintlig eller lägger till en ny betydelse.
        /// </summary>
        protected void btnUpdateMeaning_Click(object sender, EventArgs e)
        {
            //kontrollerar igen om berörda kontroller är validerade.
            if (IsValid)
            {
                var meaning = new Meaning()
                {
                    Word = txtWord.Text,
                    Comment = txtWordComment.Text,
                };

                if (ddlPageWordType.SelectedIndex > -1)
                {
                    meaning.WTypeId = Convert.ToByte(ddlPageWordType.SelectedItem.Value);
                }

                if (lstMeaning.SelectedIndex > -1)
                {
                    meaning.MeaningId = Convert.ToInt16(lstMeaning.SelectedItem.Value);
                }

                ICollection<ValidationResult> validationResults;
                if (!meaning.Validate(out validationResults))
                {
                    foreach (var v in validationResults)
                    {
                        ModelState.AddModelError("", v.ErrorMessage);
                    }
                }
                else
                {
                    try
                    {
                        Service.SaveOrUpdateMeaning(meaning);
                        if (meaning.MeaningId == 0)
                        {
                            successMessage = String.Format("Den nya betydelsen {0} har sparats.", meaning.Word);
                        }
                        else
                        {
                            successMessage = String.Format("Betydelsen {0} har uppdaterats.", meaning.Word);
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("", String.Format("Sparandet eller uppdateringen av {0} misslyckades.", meaning.Word));
                    }
                }
            }
            else
            {
                //Visa inte ModelErrors om det finns PageErrors...
                ModelState.Clear();
                //default för pnlErrorBox är display: none, 
                //så den måste visas om page innehåller Page-valideringsfel
                pnlErrorBox.Style["display"] = "block";
            }

        }

        /// <summary>
        /// Nollställer formuläret så att en ny betydelse kan fyllas i och läggas till.
        /// </summary>
        protected void btnAddNewMeaning_Click(object sender, EventArgs e)
        {
            lstMeaning.ClearSelection();
            ddlPageWordType.SelectedIndex = 0;
            txtWord.Text = String.Empty;
            txtWordComment.Text = String.Empty;
            lstItem.Items.Clear();
            ddlCategory.Items.Clear();
            ddlCategoryLink.Items.Clear();
            chkIsCategory.Checked = false;
            lstFileName.Items.Clear();
            ddlPosition.Items.Clear();
            btnUpdateMeaning.Text = "Spara";
        }

        /// <summary>
        /// Raderar en betydelse.
        /// </summary>
        protected void btnDeleteMeaning_Click(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex >= 0)
            {
                try
                {
                    Service.DeleteMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                    successMessage = "Betydelsen har raderats.";
                }
                catch
                {
                    ModelState.AddModelError("", "Raderingen av vald betydelse misslyckades.");
                }
            }
            else
            {
                ModelState.AddModelError("", "En betydelse att radera måste först markeras.");
            }
        }

        /// <summary>
        /// Återställer formuläret till utgångsläget för betydelsen som valts.
        /// Om ingen betydelse valts, återställs formuläret till utgångsläget som det var innan en betydelse valts.
        /// </summary>
        protected void btnResetMeaning_Click(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex >= 0)
            {
                lstMeaning_SelectedIndexChanged(sender, e);
            }
            else
            {
                txtWord.Text = String.Empty;
                txtWordComment.Text = String.Empty;
                ddlPageWordType.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Lägger till nytt "Item" eller uppdaterar befintligt "Item" om sådant är valt i lstItems.
        /// </summary>
        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            //kontrollerar att klientsidevalideringen lyckas även på servern.
            if (IsValid)
            {
                var item = new Item();

                //läs in värden (id:n) från listboxar och dropdown-listor. 
                if (lstItem.SelectedIndex > -1)
                {
                    item.ItemId = Convert.ToInt16(lstItem.SelectedItem.Value);
                }

                if (lstMeaning.SelectedIndex > -1)
                {
                    item.MeaningId = Convert.ToInt16(lstMeaning.SelectedItem.Value);
                }

                if (ddlPosition.SelectedIndex > -1)
                {
                    item.PosId = Convert.ToByte(ddlPosition.SelectedItem.Value);
                }

                if (lstFileName.SelectedIndex > -1)
                {
                    item.ImageId = Convert.ToInt16(lstFileName.SelectedItem.Value);
                }

                if (ddlCategory.SelectedIndex > -1)
                {
                    item.CatId = Convert.ToInt16(ddlCategory.SelectedItem.Value);
                }

                //om kryssrutan för kategorilänk är ikryssad så kontrolleras att giltiga värden är angivna.
                if (chkIsCategory.Checked)
                {
                    //Endast ParentItems kan vara kategorilänkar, för närvarande PosId 1-30.
                    //Det är ingen optimal lösning, men det är så databasen ser ut just nu.
                    if (item.PosId > 30)
                    {
                        ModelState.AddModelError("", "Endast 'ParentItem':s kan vara kategorilänkar.");
                    }
                    else
                    {
                        if (ddlCategoryLink.SelectedIndex > -1)
                        {
                            item.CatRefId = Convert.ToInt16(ddlCategoryLink.SelectedItem.Value);
                        }
                        else
                        {
                            ModelState.AddModelError("", "För kategorilänkar måste kategorin bilden länkar till väljas.");
                        }
                    }
                }

                //Triggar kontroll av ModelStateErrors manuellt, eftersom jag inte använder dubbelsidig bindning av kontrollerna.
                //Med hjälp av extension-metod som jag själv inte skapat.
                ICollection<ValidationResult> validationResults;
                if (!item.Validate(out validationResults))
                {
                    foreach (var v in validationResults)
                    {
                        ModelState.AddModelError("", v.ErrorMessage);
                    }
                }
                
                if (ModelState.Count == 0)
                {
                    try
                    {
                        Service.SaveOrUpdateItem(item);
                        if (item.ItemId == 0)
                        {
                            successMessage = "Bilden har lagts till markerad betydelse.";
                        }
                        else
                        {
                            successMessage = "Bilden har ändrats för markerad betydelse.";
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Tilläggande eller ändrandet av bilden misslyckades.");
                    }
                }
            }
            else
            {
                //Visa inte ModelErrors om det finns PageErrors...
                ModelState.Clear();
                //default för pnlErrorBox är display: none, 
                //så den måste visas om page innehåller Page-valideringsfel
                pnlErrorBox.Style["display"] = "block";
            }

        }

        /// <summary>
        /// Nollställer formuläret till så som det såg ut när betydelsen valdes.
        /// </summary>
        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            lstItem.ClearSelection();
            ddlPosition.SelectedIndex = 0;
            chkIsCategory.Checked = false;
            lstFileName.ClearSelection();
            imgImage.ImageUrl = "";
            btnUpdateItem.Text = "Spara";
        }

        /// <summary>
        /// Raderar ett "Item".
        /// </summary>
        protected void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                if (lstItem.SelectedIndex > -1)
                {
                    try
                    {
                        Service.DeleteItem(Convert.ToInt16(lstItem.SelectedItem.Value));
                        successMessage = "Den valda bilden har tagits bort från betydelsen";
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Misslyckades med att ta bort den valda bilden från betydelsen.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "En bildfil för vald betydelse måste väljas.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Betydelse måste anges.");
            }
        }

        /// <summary>
        /// Återställer formuläret till utgångsläget som gäller för valt "Item".
        /// Om inget "Item" är valt, återställs utgångsläget till vad som gäller då inget "Item" är valt.
        /// </summary>
        protected void btnResetItem_Click(object sender, EventArgs e)
        {
            if (lstItem.SelectedIndex >= 0)
            {
                lstItem_SelectedIndexChanged(sender, e);
            }
            else
            {
                btnAddNewItem_Click(sender, e);
            }
        }

        /// <summary>
        /// Ändra förhandsgranskning av bilden som valts i fillistan.
        /// </summary>
        protected void lstFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgImage.ImageUrl = String.Format("~/Images/ComPics/{0}", lstFileName.SelectedItem.Text);
        }

        /// <summary>
        /// Körs när FileName-listan databundits. Ser till att det filnamn som valts i "Item"-listan
        /// även markeras i FileName-listan. Om inget "betydelse" valts, ska FileName-listan vara tom.
        /// </summary>
        protected void lstFileName_DataBound(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                if (lstItem.SelectedIndex > -1)
                {
                    lstFileName.ClearSelection();
                    if (lstFileName.Items.FindByText(lstItem.SelectedItem.Text) != null)
                    {
                        lstFileName.Items.FindByText(lstItem.SelectedItem.Text).Selected = true;
                    }
                }
            }
            else
            {
                lstFileName.Items.Clear();
            }
        }
        
        /// <summary>
        /// För populering av dropdown-listan för kategorier, med CatId som bakomliggande värde.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetCategoryData()
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                return Service.GetAllCategories();
            }
            return null;
        }

        /// <summary>
        /// För (eventuell) populering av dropdown-listan för kategori-länkar.
        /// </summary>
        /// <returns>Om kryssrutan för kategorilänk är ikryssad, listan med kategorier, annars null.</returns>
        public Dictionary<int, string> IsCategoryGetCategoryData()
        {
            if (chkIsCategory.Checked)
            {
                return GetCategoryData();
            }
            return null;
        }

        /// <summary>
        /// Ställer in utgångsvärden beroende på vald betydelse. Endast om betydelse är vald.
        /// </summary>
        protected void ddlCategory_DataBound(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                var catInfo = Service.GetCatInfoOfMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                if (catInfo.Key != 0)
                {
                    if (ddlCategory.Items.Count > 0)
                    {
                        ddlCategory.Items.FindByValue(catInfo.Key.ToString()).Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Ställer in utgångsvärden beroende på vald betydelse. Endast om betydelse är vald.
        /// </summary>
        protected void ddlCategoryLink_DataBound(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                if (ddlCategoryLink.Items.Count > 0)
                {
                    var catInfo = Service.GetCatInfoOfMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                    if (catInfo.Value != null)
                    {
                        //Value innehåller CatRefId
                        ddlCategoryLink.Items.FindByValue(catInfo.Value.ToString()).Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Populerar listan för tillgängliga positioner, med PosId som bakomliggande värde.
        /// </summary>
        /// <returns>Om betydelse valts, Dictionary med tillgängliga positioner, annars null.</returns>
        public Dictionary<int, string> GetPositionData()
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                Dictionary<int, string> positions = Service.GetAllPositions();
                return positions;
            }
            return null;
        }

        /// <summary>
        /// Markerar positionen i dropdown-listan för positioner baserat på valt "Item".
        /// </summary>
        protected void ddlPosition_DataBound(object sender, EventArgs e)
        {
            if (lstItem.SelectedIndex > -1)
            {
                var posInfo = Service.GetPositionOfItem(Convert.ToInt16(lstItem.SelectedItem.Value));
                ddlPosition.ClearSelection();
                ddlPosition.Items.FindByValue(posInfo.ToString()).Selected = true;
            }
        }

        /// <summary>
        /// I navigationsläget kan man klicka på en ikon för inställningar. 
        /// Denna metod ändrar inställningen i programmet så att inställningläget visas istället.
        /// Triggar omladdning av sidan.
        /// </summary>
        protected void imbSettings_Click(object sender, ImageClickEventArgs e)
        {
            IsSettingsMode = true;
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        /// <summary>
        /// Körs om användaren klickar i rutan för lyckad operation.
        /// </summary>
        protected void btnOKSuccess_Click(object sender, EventArgs e)
        {
            lstFileName.ClearSelection();
            imgImage.ImageUrl = "";
        }
    }
}
