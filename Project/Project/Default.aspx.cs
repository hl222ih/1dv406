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

        private bool IsSettingsMode
        {
            get { return (Session["IsSettingsMode"] != null && Session["IsSettingsMode"].ToString() == "True"); }
            set { Session["IsSettingsMode"] = value.ToString(); }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
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

            imbHome.Click += new ImageClickEventHandler(imbHome_Click);
            imbHome2.Click += new ImageClickEventHandler(imbHome_Click);

            imbOK.Click += new ImageClickEventHandler(imbOK_Click);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ModelState.AddModelError("testKey", "testValue");
            if (!IsSettingsMode)
            {
                RenderImages();
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            SetBackgroundColorsToDdlPageWordType();
            ClientScriptManager csm = Page.ClientScript;

            if (!lstMeaning.Enabled)
            {
                csm.RegisterStartupScript(this.GetType(), "DisableControl", "disableControl('Content_lstMeaning')", true);
            }
            if (chkIsCategory.Checked)
            {
                csm.RegisterStartupScript(this.GetType(), "EnableControl", "enableControl('Content_ddlCategoryLink')", true);
            }

        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            if (lstItem.SelectedIndex > -1)
            {
                Session["lstItemValue"] = lstItem.SelectedItem.Text;
            }
        }

        protected void RenderImages()
        {



            imbCancel.OnClientClick = "toggleNavButtons(false, false, false, false, false); undim(); return false;";

            imbLeft.OnClientClick = "showLeftImage(); return false;";
            imbRight.OnClientClick = "showRightImage(); return false;";
            //osynliggör navigeringsknappar som inte används i aktuell vy.
            imbOK.Style["display"] = "none";
            imbCancel.Style["display"] = "none";
            imbLeft.Style["display"] = "none";
            imbRight.Style["display"] = "none";
            imbInfo.Style["display"] = "none";
            if (Service.GetCurrentCategoryId() == 1 && Service.GetCurrentPageNumber() == 1)
            {
                imbHome.Style["display"] = "none";
            }
            else
            {
                imbSettings.Style["display"] = "none";
            }

            var cssTemplateName = Service.GetCurrentCssTemplateName();
            var pageItemsUnits = Service.GetCurrentPageItemsUnits();
            
            var counter = 0;

            foreach (var piu in pageItemsUnits)
            {
                counter++;
                var pi = piu.GetPageParentItem();

                var lb = new LinkButton()
                {
                    ID = String.Format("imbUnit{0}", pi.Position),
                    CssClass = String.Format("item {0}", cssTemplateName),
                    BackColor = pi.BackGroundColor,
                    CausesValidation = false
                };
                lb.Attributes.Add("mId", pi.MeaningId.ToString());
                

                var lbl = new Label()
                {
                    Text = pi.MeaningWord
                };

                var img = new Image()
                {
                    ImageUrl = String.Format("~/Images/ComPics/{0}", pi.ImageFileName)
                };
                img.Attributes.Add("type", pi.PageItemType.ToString());
                img.Attributes.Add("pos", pi.Position.ToString());

                if (pi.PageItemType == PageItemType.ParentWordItem)
                {
                    var nextLeft = Service.GetNextLeftPageItem(pi.PageItemType, pi.Position, pi.MeaningId);
                    var hasNextLeft = (nextLeft != null);
                    var nextRight = Service.GetNextRightPageItem(pi.PageItemType, pi.Position, pi.MeaningId);
                    var hasNextRight = (nextRight != null);
                    var info = String.Format("{0}\n{1}", pi.MeaningComment, pi.ImageComment);
                    var hasInfo = (info != "\n");
                    lb.OnClientClick = String.Format("dim({0}); toggleNavButtons({1}, {2}, {3}, {4}, {5}); return false;", pi.Position, "true", "true", hasNextLeft ? "true" : "false", hasNextRight ? "true" : "false", hasInfo ? "true" : "false");
                }
                else if (pi.PageItemType == PageItemType.ParentCategoryItem)
                {
                    lb.Click += new EventHandler(lbParentCategoryItem_Click);
                    lb.Attributes.Add("catId", ((PageParentCategoryItem)pi).LinkToCategoryId.ToString());
                }

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
                    imgChild.Attributes.Add("type", pci.PageItemType.ToString());
                    imgChild.Attributes.Add("pos", pci.Position.ToString());

                    lb.Controls.Add(imgChild);
                }
            }
        }

        protected void lbParentWordItem_Click(object sender, EventArgs e)
        {
            var lb = ((LinkButton)sender);
        }

        protected void lbParentCategoryItem_Click(object sender, EventArgs e)
        {
            var lb = ((LinkButton)sender);
            var test = lb.Attributes["catId"];
            Service.UpdatePageCategory(Convert.ToInt32(lb.Attributes["catId"]));
            //trigga omladdning av sidan för att den nya sidan ska visas.
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        protected void imbOK_Click(object sender, ImageClickEventArgs e)
        {
            Service.UpdatePageCategory(1, 1);
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        protected void imbHome_Click(object sender, ImageClickEventArgs e)
        {
            Service.UpdatePageCategory(1, 1);
            IsSettingsMode = false;
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            return Service.PageWordTypes;
        }

        public IEnumerable<Meaning> GetMeaningData()
        {
            return Service.GetMeanings().OrderBy(m => m.Word);
        }

        public Dictionary<int, string> GetImageFileNameDataOfPage()
        {
            lstItem.Items.Clear();
            if (lstMeaning.SelectedIndex > -1)
            {
                return Service.GetPageItemFileNames(Convert.ToInt16(lstMeaning.SelectedItem.Value));
            }
            return null;
        }

        public Dictionary<int, string> GetImageFileNameData()
        {
            lstFileName.Items.Clear();
            if (lstItem.SelectedIndex > -1)
            {
                return Service.GetAllFileNames();
            }
            return null;
        }

        protected void ddlPageWordType_DataBound(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        //sätter bakgrundsfärg på ddlPageWordType:s listitems i enlighet med respektive ordtyp.
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

        protected void lstMeaning_SelectedIndexChanged(object sender, EventArgs e)
        {
            var meaning = Service.GetMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
            btnUpdateItem.Text = "Uppdatera";
            txtWord.Text = meaning.Word;
            txtWordComment.Text = meaning.Comment;
            ddlPageWordType.ClearSelection();
            ddlPageWordType.Items.FindByValue(meaning.WTypeId.ToString()).Selected = true;
            var catInfo = Service.GetCatInfoOfMeaning(meaning.MeaningId);
            if (catInfo.Value != null)
            {
                chkIsCategory.Checked = true;
            }
            else
            {
                chkIsCategory.Checked = false;
            }
        }

        protected void btnUpdateMeaning_Click(object sender, EventArgs e)
        {
            var meaning = new Meaning()
            {
                Word = txtWord.Text,
                Comment = txtWordComment.Text,
                WTypeId = Convert.ToByte(ddlPageWordType.SelectedItem.Value)
            };
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
                Service.SaveOrUpdateMeaning(meaning);
            }
            //bekräfta och gör postback
        }

        protected void btnAddNewMeaning_Click(object sender, EventArgs e)
        {
            lstMeaning.ClearSelection();
            lstMeaning.Enabled = false;
            ddlPageWordType.SelectedIndex = 0;
            txtWord.Text = String.Empty;
            txtWordComment.Text = String.Empty;
            lstItem.Items.Clear();
            btnUpdateMeaning.Text = "Spara";
        }

        protected void btnDeleteMeaning_Click(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex >= 0)
            {
                //varna användaren först
                Service.DeleteMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                //bekräfta och gör postback
            }
        }

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

        protected void lstItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Session["lstItemValue"] == null || Session["lstItemValue"].ToString() != lstItem.SelectedItem.Text)
            {
                imgImage.ImageUrl = String.Format("~/Images/ComPics/{0}", lstItem.SelectedItem.Text);

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
                }
            }
        }

        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            //Item item = new Item();

            //if (lstItem.SelectedIndex > 0)
            //{
            //    item = Service.GetItem(Convert.ToInt16(lstItem.SelectedItem.Value));
            //}

            var item = new Item()
            {
                MeaningId = 0,
                PosId = 0,
                ImageId = 0,
                CatId = 0,
                CatRefId = 0
            };

            // större än 0 eftersom den första listitem är tom.
            if (lstItem.SelectedIndex > 0)
            {
                item.ItemId = Convert.ToInt16(lstItem.SelectedItem.Value);
            }
//            Service.SaveOrUpdateItem(item);

            //bekräfta och gör postback

        }

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            lstItem.ClearSelection();
            lstItem.Enabled = false;

            ddlPosition.SelectedIndex = 0;
            chkIsCategory.Checked = false;
            lstFileName.ClearSelection();

            btnUpdateItem.Text = "Spara";

        }

        protected void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (lstItem.SelectedIndex >= 1)
            {
                //varna användaren först
                Service.DeleteItem(Convert.ToInt16(lstItem.SelectedItem.Value));
                //bekräfta och gör postback
            }

        }

        protected void btnResetItem_Click(object sender, EventArgs e)
        {

        }

        protected void lstFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgImage.ImageUrl = String.Format("~/Images/ComPics/{0}", lstFileName.SelectedItem.Text);
        }

        protected void lstFileName_DataBound(object sender, EventArgs e)
        {
            if (lstItem.SelectedIndex > -1)
            {
                lstFileName.ClearSelection();
                lstFileName.Items.FindByText(lstItem.SelectedItem.Text).Selected = true;
            }
        }

        public Dictionary<int, string> GetCategoryData()
        {
            if (lstItem.SelectedIndex > -1)
            {
                return Service.GetAllCategories();
            }
            return null;
        }

        public Dictionary<int, string> IsCategoryGetCategoryData()
        {
            if (chkIsCategory.Checked)
            {
                return GetCategoryData();
            }
            return null;
        }

        protected void ddlCategory_DataBound(object sender, EventArgs e)
        {
            if (lstMeaning.SelectedIndex > -1)
            {
                if (lstItem.SelectedIndex > -1)
                {
                    var catInfo = Service.GetCatInfoOfMeaning(Convert.ToInt16(lstMeaning.SelectedItem.Value));
                    //Value innehåller CatRefId
                    //if (catInfo.Value != null)
                    //{
                    //    chkIsCategory.Checked = true;
                    //}
                    if (catInfo.Key != 0)
                    {
                        //chkIsCategory.Checked = false;
                        //Key innehåller CatId
                        if (ddlCategory.Items.Count > 0)
                        {
                            ddlCategory.Items.FindByValue(catInfo.Key.ToString()).Selected = true;
                        }
                    }
                }
            }
        }

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

        public Dictionary<int, string> GetPositionData()
        {
            if (lstItem.SelectedIndex > -1)
            {
                Dictionary<int, string> positions = Service.GetAllPositions();
                return positions;
            }
            return null;
        }

        protected void ddlPosition_DataBound(object sender, EventArgs e)
        {
            if (lstItem.SelectedIndex > -1)
            {
                var posInfo = Service.GetPositionOfItem(Convert.ToInt16(lstItem.SelectedItem.Value));
                ddlPosition.ClearSelection();
                ddlPosition.Items.FindByValue(posInfo.ToString()).Selected = true;
            }
        }

        protected void imbSettings_Click(object sender, ImageClickEventArgs e)
        {
            IsSettingsMode = true;
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }
    }
}
