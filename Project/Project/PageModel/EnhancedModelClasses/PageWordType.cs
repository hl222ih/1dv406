using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Project.PageModel
{
    public class PageWordType
    {
        private string colorRGBCode;

        public int WTypeId { get; set; }
        public int ColorId { get; set; }
        public string ColorRGBCode
        {
            get
            {
                if (colorRGBCode != String.Empty)
                {
                    return colorRGBCode;
                }
                else
                {
                    return "#ffffff"; //vit färg som standard
                }
            }
            set
            {
                var re = new Regex(@"^\#[a-f0-9]{6}$", RegexOptions.IgnoreCase);
                if (re.IsMatch(value))
                {
                    colorRGBCode = value;
                }
                //om fel färgkod skickas in sätts inget värde
            }
        }
        public string WType { get; set; }
        public Color Color
        {
            get
            {
                return ColorTranslator.FromHtml(ColorRGBCode);
            }
        }
    }
}