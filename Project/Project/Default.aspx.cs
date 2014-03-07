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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBackgroundColorsToDropDownListItems();
        }

        public IEnumerable<WordTypeItem> GetWordTypeItemData()
        {
            return Service.WordTypeItems;
        }

        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            SetBackgroundColorsToDropDownListItems();
        }

        private void SetBackgroundColorsToDropDownListItems()
        {
            DropDownList1.BackColor = Service.GetColorById(int.Parse(DropDownList1.SelectedValue));

            foreach (ListItem li in DropDownList1.Items)
            {
                li.Attributes.CssStyle.Add("background-color", Service.GetColorHexCodeById(int.Parse(li.Value)));
            }
        }
    }
}