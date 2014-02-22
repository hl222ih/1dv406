using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb2_2.BLL;

namespace Labb2_2
{
    public partial class Default : System.Web.UI.Page
    {

        private Service service;

        private Service Service
        {
            get { return service ?? (service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int myOut;
                var test = Service.GetContactsPageWise(10, 10, out myOut);
                var test2 = 1;
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError(ex.Message, ex);
                //ex.Data.Add(ex.Message);
                ex.Data.Add("sdfsdf", 2);
            }

        }
    }
}