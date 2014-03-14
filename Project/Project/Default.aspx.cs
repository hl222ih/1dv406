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
        //protected HtmlGenericControl divControl;

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

            var ok = new ImageButton();
            var cancel = new ImageButton();
            var nextLeft = new ImageButton();
            var nextRight = new ImageButton();
            var home = new ImageButton();

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
                    BackColor = pi.BackGroundColor
                    //CausesValidation = false
                };

                var lbl = new Label()
                {
                    Text = pi.MeaningWord
                };

                var img = new Image()
                {
                    ImageUrl = String.Format("~/Images/ComPics/{0}", pi.ImageFileName),
                };

                if (pi.PageItemType == PageItemType.ParentWordItem)
                {
                    //speciella egenskaper för pwi
                    lb.Click += new EventHandler(lbParentWordItem_Click);
                    lb.OnClientClick = "dim()";
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




            }
        }

        protected void lbParentWordItem_Click(object sender, EventArgs e)
        {
            var lb = ((LinkButton)sender);
            if (lb.CssClass.Substring(0, 5) == "item ")
            {
                lb.CssClass = lb.CssClass.Replace("item ", "itemFull ");
                lb.OnClientClick = "undim()";
            }
            else
            {
                lb.CssClass = lb.CssClass.Replace("itemFull ", "item ");
                lb.OnClientClick = "dim()";
            }
        }
        protected void lbParentCategoryItem_Click(object sender, EventArgs e)
        {
            var lb = ((LinkButton)sender);
            var test = lb.Attributes["catId"];
            Service.UpdatePageCategory(Convert.ToInt32(lb.Attributes["catId"]));
            //trigga omladdning av sidan för att den nya sidan ska visas.
            Response.Redirect(Request.Url.AbsoluteUri, false);

        }
        
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            return Service.PageWordTypes;
        }

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton1.BackColor = Service.GetColorById(4);
        //}

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

    }
}
