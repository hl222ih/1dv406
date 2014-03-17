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

        protected void Page_Init(object sender, EventArgs e)
        {
            //rendera bara ItemsUnits när det behövs
            var test = Session["refreshItemsUnits"] != null ? Session["refreshItemsUnits"].ToString() : "null";
            if (Session["refreshItemsUnits"] == null || (string)Session["refreshItemsUnits"] == "true")
            {
                RenderImages();
                //Session["refreshItemsUnits"] = "false";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RenderImages()
        {

            imbHome.Click += new ImageClickEventHandler(imbHome_Click);
            
            imbOK.Click += new ImageClickEventHandler(imbOK_Click);

            imbCancel.Click += new ImageClickEventHandler(imbCancel_Click);
            
            imbCancel.OnClientClick = "toggleNavButtons(false, false, false, false, false); undim(); return false;";

            // imbLeft.Click += new ImageClickEventHandler(imbLeft_Click);
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
                    
                    //lb.Click += new EventHandler(lbParentWordItem_Click);
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
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        protected void imbCancel_Click(object sender, ImageClickEventArgs e)
        {
            //if (lb.CssClass.Substring(0, 5) == "item ")
            
        }

        protected void imbLeft_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            return Service.PageWordTypes;
        }

        public IEnumerable<Meaning> GetMeaningData()
        {
            return Service.GetMeanings();
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
            txtWord.Text = meaning.Word;
            txtWordComment.Text = meaning.Comment;
            ddlPageWordType.ClearSelection();
            ddlPageWordType.Items.FindByValue(meaning.WTypeId.ToString()).Selected = true;
            Dictionary<int, string> pageItemFileNames = Service.GetPageItemFileNames(Convert.ToInt32(lstMeaning.SelectedItem.Value));
            foreach (var pifn in pageItemFileNames)
            {
                var a = pifn.Key;
                var b = pifn.Value;
                lstItem.Items.Add(new ListItem(pifn.Value, pifn.Key.ToString()));
            }

        }

        protected void lstItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PageItem pageItem = Service.GetPageItem(Convert.ToInt32(lstItem.SelectedItem.Value));
        }
    }
}
