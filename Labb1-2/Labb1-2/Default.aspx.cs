using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Labb1_2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculateDiscount_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                txtAmount.Text = txtAmount.Text.Trim();
                var subtotal = decimal.Parse(txtAmount.Text);

                var reciept = new Reciept(subtotal);

                lblTotalAmount.Text = reciept.Subtotal.ToString("F");
                lblDiscountRate.Text = reciept.DiscountRate.ToString("0");
                lblDiscount.Text = reciept.MoneyOff.ToString("F");
                lblTotalAmountToPay.Text = reciept.Total.ToString("F");

                pnlReciept.Visible = true;
            }
        }
    }
}