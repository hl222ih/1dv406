using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Model
{
    public class Item
    {
        public int ItemId { get; set; }
        public int MeaningId { get; set; }
        public int ImageId { get; set; }
        public int? CatRefId { get; set; }
        public int CatId { get; set; }
        public int PosId { get; set; }
    }
}