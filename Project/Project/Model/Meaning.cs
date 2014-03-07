using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Model
{
    public class Meaning
    {
        public int MeaningId { get; set; }
        public int WTypeId { get; set; }
        //max 30
        public string Word { get; set; }
        //max 100
        public string Comment { get; set; }
    }
}