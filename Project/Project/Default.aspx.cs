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
            if (!IsPostBack)
            {
            
                RenderImages();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RenderImages()
        {
            var CssTemplateName = Service.GetCssTemplateName();
            var pageItemsUnits = Service.GetPageItemsUnits();
            var counter = 0;

            foreach (var piu in pageItemsUnits)
            {
                counter++;
                var pi = piu.GetPageParentItem();
                ImageButton imb = new ImageButton()
                {//    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="item" BackColor="#fde885" ImageUrl="~/Images/Blissymbols/God.svg" Height="200" Width="300" />
                    ID = String.Format("imbUnit{0}", pi.Position),
                    ImageUrl = "~/Images/Blissymbols/hus.svg", //testvärde
                    BackColor = pi.BackGroundColor,
                    Height = 100, //testvärde
                    Width = 150 //testvärde
                };

                if (pi.PageItemType == PageItemType.ParentWordItem)
                {
                    //speciella egenskaper för pwi
                    imb.Click += new ImageClickEventHandler(imbWordItem_Click);
                }
                else if (pi.PageItemType == PageItemType.ParentCategoryItem)
                {
                    //speciella egenskaper för pci
                    imb.Click += new ImageClickEventHandler(imbParentItem_Click);
                }



                phItems.Controls.Add(imb);
            }
        }

        protected void imbWordItem_Click(object sender, ImageClickEventArgs e)
        {
        }
        protected void imbParentItem_Click(object sender, ImageClickEventArgs e)
        {
        }
        
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //sätter bakgrundsfärg på ddlPageWordType:s listitems i enlighet med respektive ordtyp.
            if (ddlPageWordType.Items.Count > 0)
            {
                foreach (ListItem li in ddlPageWordType.Items)
                {
                    li.Attributes.CssStyle.Add("background-color", Service.GetColorHexCodeById(int.Parse(li.Value)));
                }
            }
        }

        //protected void ddlPageWordType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SetBackgroundColorsToDdlPageWordType();
        //}

        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            return Service.PageWordTypes;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton1.BackColor = Service.GetColorById(4);
        }

        //protected void ddlPageWordType_DataBound(object sender, EventArgs e)
        //{
        //    SetBackgroundColorsToDdlPageWordType();
        //}

        //private void SetBackgroundColorsToDdlPageWordType()
        //{
        //   // ddlPageWordType.BackColor = Service.GetColorById(int.Parse(ddlPageWordType.SelectedValue));

        //    foreach (ListItem li in ddlPageWordType.Items)
        //    {
        //        li.Attributes.CssStyle.Add("background-color", Service.GetColorHexCodeById(int.Parse(li.Value)));
        //    }
        //}

    }
}
