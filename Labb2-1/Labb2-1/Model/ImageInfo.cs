using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb2_1.Model
{
    public struct ImageInfo
    {
        public string FileName { get; set; }
        public string ThumbName { get; set; }

        public ImageInfo(string fileName, string thumbName) : this()
        {
            FileName = fileName;
            ThumbName = thumbName;
        }
    }
}