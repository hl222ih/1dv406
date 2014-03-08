using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Project.PageModel
{
    //Detta är en klass vars objekt motsvarar ett visuellt bildobjekt på sidan.
    //Ett PageItem-objekt innehåller information från i princip samtliga tabeller 
    //i databasen, en aning beroende på vilken typ av PageItem-objekt det är.
    //Kärnan i ett PageItem-objekt är dess Meaning.
    abstract public class PageItem
    {
        private Color pageItemBackGroundColor;

        public int PageItemId { get; set; } //motsvarar DB-tabellen Meaning:s MeaningId.
        public string MeaningWord { get; set; }
        public string MeaningComment { get; set; }
        public Color PageItemBackGroundColor
        {
            get
            {
                if (pageItemBackGroundColor != Color.Empty)
                {
                    return pageItemBackGroundColor;
                }
                else
                {
                    return Color.Transparent;
                }
            }
            set //eg. bara intressant för blissymboler.
            {
                pageItemBackGroundColor = value;
            } 
        }
        public int ImageId { get; set; }
        public int Position { get; set; }
        public string ImageComment { get; set; }
        abstract public PageItemType GetPageItemType();
    }
}