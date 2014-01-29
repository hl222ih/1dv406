using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using labb1._1.Model;

namespace labb1._1
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Skriver ut antalet versaler i inmatad text, 
        /// eller nollställer formuläret.
        /// </summary>
        protected void btnCountOrReset_Click(object sender, EventArgs e)
        {
            if (!lblCountInfo.Visible)
            {
                var numberOfCapitals = TextAnalyzer.GetNumberOfCapitals(txtInput.Text);
                lblCountInfo.Visible = true;
                lblCountInfo.Text = String.Format("Texten innehåller {0} versaler.", numberOfCapitals.ToString());
                txtInput.Enabled = false;
                btnCountOrReset.Text = "Rensa";
            }
            else
            {
                lblCountInfo.Visible = false;
                txtInput.Enabled = true;
                txtInput.Text = String.Empty;
                txtInput.Focus();
            }
        }
    }
}