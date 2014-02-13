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
        private Gallery gallery;

        private Gallery Gallery
        {
            get { return gallery ?? (gallery = new Gallery()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["image"] != null)
            {
                imgFull.ImageUrl = String.Format("{0}{1}", "~/Uploads/", Request.QueryString["image"]);
                imgFull.Visible = true;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                Gallery.SaveImage(fuChooseFile.PostedFile.InputStream, fuChooseFile.FileName);
                //vill egentligen hantera undantag här, men förstår inte
                //hur jag inifrån code-behind kan komma åt och visa validationsummary-rutan :(

                //eftersom inte programmet har krashat ännu så lyckades uppladdningen:
                pnlConfirmBox.Visible = true;
            }
        }

        public IEnumerable<ImageInfo> Repeater_GetData()
        {
            return Gallery.GetImageInfos();
        }
    }
}