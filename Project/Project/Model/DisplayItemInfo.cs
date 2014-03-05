using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Model
{
    public class DisplayItemInfo
    {
        public string Color { get; set; }
        public string FileName { get; set; }
        public ItemType ItemType { get; set; }

        public DisplayItemInfo(string color, string fileName, ItemType itemType)
        {
            Color = color;
            FileName = fileName;
            ItemType = itemType;
        }
    }
}