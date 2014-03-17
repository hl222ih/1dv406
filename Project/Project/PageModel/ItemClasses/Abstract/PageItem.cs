using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Project.PageModel
{
    //Detta är en klass vars objekt motsvarar ett visuellt bildobjekt på sidan.
    //Ett PageItem-objekt innehåller information från i princip samtliga tabeller 
    //i databasen, en aning beroende på vilken typ av PageItem-objekt det är.
    //Kärnan i ett PageItem-objekt är dess Meaning.
    abstract public class PageItem
    {
        private string backGroundRGBColor;

        public int PageItemId { get; set; }
        public int MeaningId { get; set; }
        public string MeaningWord { get; set; }
        public string MeaningComment { get; set; }
        public string ImageFileName { get; set; }
        public int Position { get; set; }
        public string ImageComment { get; set; }
        abstract public PageItemType PageItemType { get; set; }
        public PageImageType PageImageType { get; set; }
        public string CssTemplateName { get; set; }
        public Color BackGroundColor
        {
            get
            {
                if (backGroundRGBColor != String.Empty)
                {
                    return ColorTranslator.FromHtml(backGroundRGBColor);
                }
                else
                {
                    return Color.White; //vit färg som standard
                }
            }
            set //eg. bara intressant för blissymboler.
            {
                backGroundRGBColor = String.Format("#{0}{1}{2}", value.R, value.G, value.B);
            }
        }
        public string BackGroundRGBColor
        {
            get
            {
                if (backGroundRGBColor != String.Empty)
                {
                    return backGroundRGBColor;
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
                    backGroundRGBColor = value;
                }
                //Om fel färgkod skickas in sätts ingen färg
            }

        }
    }
}