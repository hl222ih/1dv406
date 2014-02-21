using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Labb2_1.Model;
using System.IO;
using System.Web.UI.HtmlControls;

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
                if (Request.QueryString["upload"] == "success")
                {
                    pnlConfirmBox.Visible = true;
                }
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (!fuChooseFile.HasFile)
                {
                    ThrowException("Vald fil existerar inte");
                    return;
                }

                if (fuChooseFile.PostedFile.ContentLength > 4194304)
                {
                    ThrowException("Filens storlek får inte överstiga 4MB");
                    return;
                }

                try
                {
                    var newFileName = Gallery.SaveImage(fuChooseFile.PostedFile.InputStream, fuChooseFile.FileName);

                    //eftersom inte programmet har krashat ännu så lyckades uppladdningen:
                    Response.Redirect(String.Format("Default.aspx?image={0}&upload=success",
                                    Server.UrlEncode(fuChooseFile.FileName)));

                }
                catch (BadImageFormatException ex)
                {
                    ThrowException(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    ThrowException(ex.Message);
                }
                catch (Exception ex)
                {
                    ThrowException(String.Format("Ett fel inträffade, ({0})", ex.Message));
                }
            }
        }

        protected void ThrowException(string errorMessage)
        {
            var validator = new CustomValidator
            {
                IsValid = false,
                ErrorMessage = errorMessage
            };
            Page.Validators.Add(validator);
        }

        public IEnumerable<ImageInfo> Repeater_GetData()
        {
            return Gallery.GetImageInfos();
        }

        protected void rptImageBox_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (Request.QueryString["image"] != null)
                {
                    if (Request.QueryString["image"] == ((ImageInfo)e.Item.DataItem).FileName)
                    {
                        HyperLink link = (HyperLink)e.Item.FindControl("lbThumbImage");
                        link.CssClass = "selectedThumbLink";
                    }
                }
            }
        }
    }
}