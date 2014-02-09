using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb2_1.Model;
using System.IO;

namespace Labb2_1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Gallery.SaveImage(fuChooseFile.PostedFile.InputStream, fuChooseFile.FileName);
            }
        }
    }
}