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

namespace Project
{
    public partial class _Default : Page
    {
        //protected HtmlGenericControl divControl;

        private Service service;

        private Service Service
        {
            get { return service ?? (service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        protected void ddlPageWordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        private IEnumerable<PageWordType> pageWordTypes;

        public IEnumerable<PageWordType> GetPageWordTypeData()
        {
            if (Session["PageWordTypes"] == null)
            {
                Session["PageWordTypes"] = Service.PageWordTypes;
            }
            return Session["PageWordTypes"] as IEnumerable<PageWordType>;
        }

        protected void ddlPageWordType_DataBound(object sender, EventArgs e)
        {
            SetBackgroundColorsToDdlPageWordType();
        }

        private void SetBackgroundColorsToDdlPageWordType()
        {
            ddlPageWordType.BackColor = Service.GetColorById(int.Parse(ddlPageWordType.SelectedValue));

            foreach (ListItem li in ddlPageWordType.Items)
            {
                li.Attributes.CssStyle.Add("background-color", Service.GetColorHexCodeById(int.Parse(li.Value)));
            }
        }
    }
}