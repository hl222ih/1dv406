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
                return colorRGBCode;
            }
            set
            {
                var re = new Regex(@"^\#[a-f0-9]{6}$", RegexOptions.IgnoreCase);
                if (re.IsMatch(value))
                {
                    colorRGBCode = value;
                }
                else
                {
                    colorRGBCode = "#FFFFFF"; //vit färg som standard
                }
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