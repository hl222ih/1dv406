using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.PageModel
{
    public class WordTypeItem
    {
        public int WTypeId { get; set; }
        public int ColorId { get; set; }
        public string ColorHexCode { get; set; } //inkluderande #
        public string WType { get; set; }
    }
}