using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb1_3.Model;

namespace Labb1_3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RenderTable(bool CelciusToFahrenheit, int startValue, int endValue, int stepValue)
        {
            //skapar tableheader
            var thCell1 = new TableHeaderCell();
            var thCell2 = new TableHeaderCell();
            if (CelciusToFahrenheit)
            {
                thCell1.Controls.Add(new LiteralControl("°C"));
                thCell2.Controls.Add(new LiteralControl("°F"));
            }
            else
            {
                thCell1.Controls.Add(new LiteralControl("°F"));
                thCell2.Controls.Add(new LiteralControl("°C"));
            }
            var thRow = new TableHeaderRow();
            thRow.Controls.Add(thCell1);
            thRow.Controls.Add(thCell2);
            tblResults.Controls.Add(thRow);

            //skapar tablebody
            while (startValue <= endValue)
            {
                var row = new TableRow();
                var cell = new TableCell();
                cell.Controls.Add(
                    new LiteralControl(
                        startValue.ToString()));
                row.Controls.Add(cell);
                cell = new TableCell();

                if (CelciusToFahrenheit)
                {
                    cell.Controls.Add(
                        new LiteralControl(
                            TemperatureConverter.CelciusToFahrenheit(startValue).ToString()));
                }
                else
                {
                    cell.Controls.Add(
                        new LiteralControl(
                            TemperatureConverter.FahrenheitToCelcius(startValue).ToString()));
                }

                row.Controls.Add(cell);
                tblResults.Rows.Add(row);

                startValue += stepValue;

            }
        }

        protected void btnConvert_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                RenderTable(rdbC2F.Checked,
                    Convert.ToInt32(txtStart.Text),
                    Convert.ToInt32(txtEnd.Text),
                    Convert.ToInt32(txtStep.Text));
            }
        }
    }
}