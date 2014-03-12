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
            if (Session["refreshItemsUnits"] == null || (string)Session["refreshItemsUnits"] == "true")
            {
                RenderImages();
                ViewState["refreshItemsUnits"] = "false";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RenderImages()
        {
            var CssTemplateName = Service.GetCurrentCssTemplateName();
            var pageItemsUnits = Service.GetCurrentPageItemsUnits();
            var counter = 0;

            foreach (var piu in pageItemsUnits)
            {
                counter++;
                var pi = piu.GetPageParentItem();

                UpdatePanel upnl = new UpdatePanel();

                ImageButton imb = new ImageButton()
                {
                    ID = String.Format("imbUnit{0}", pi.Position),
                    ImageUrl = String.Format("~/Images/Blissymbols/{0}", pi.ImageFileName),
                    BackColor = pi.BackGroundColor,
                    CssClass = String.Format("item {0}", CssTemplateName)
                };

                if (pi.PageItemType == PageItemType.ParentWordItem)
                {
                    //speciella egenskaper för pwi
                    imb.Click += new System.Web.UI.ImageClickEventHandler(imbParentWordItem_Click);
                    imb.OnClientClick = "dim()";
                }
                else if (pi.PageItemType == PageItemType.ParentCategoryItem)
                {
                    //speciella egenskaper för pci
                    imb.Click += new System.Web.UI.ImageClickEventHandler(imbParentCategoryItem_Click);
                    imb.OnClientClick = "dim()";
                }

                upnl.ContentTemplateContainer.Controls.Add(imb);                
                phItems.Controls.Add(upnl);
            }
        }

        protected void imbParentWordItem_Click(object sender, ImageClickEventArgs e)
        {
            //((ImageButton)sender).BackColor = Service.GetColorById(2);
        }
        protected void imbParentCategoryItem_Click(object sender, ImageClickEventArgs e)
        {
            var imb = ((ImageButton)sender);
            if (imb.CssClass.Substring(0,5) == "item ")
            {
                imb.CssClass = imb.CssClass.Replace("item ", "itemFull ");
                imb.OnClientClick = "undim()";
            }
            else
            {
                imb.CssClass = imb.CssClass.Replace("itemFull ", "item ");
                imb.OnClientClick = "dim()";
            }
            
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
