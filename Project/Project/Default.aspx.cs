using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Project.Model;

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
            //divControl.Style.Add("background-color", "red");
            //if (divControl.Style["background-color"] == "red")
            //    divControl.Style.Add("background-color", "blue");
            //else
            //    divControl.Style.Add("background-color", "red");
            //if (ImageButton1.Style["background-color"] == "red")
            //    ImageButton1.Style.Add("background-color", "blue");
            //else
            //    ImageButton1.Style.Add("background-color", "red");

        }

        public IEnumerable<WordType> GetWordTypeData()
        {
            return Service.GetWordTypes();
        }

        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem li in DropDownList1.Items)
            {
                li.Attributes.CssStyle.Add("background-color", ((WordType)DropDownList1.DataSource).ColorId;
            }
        }

       
    }
}