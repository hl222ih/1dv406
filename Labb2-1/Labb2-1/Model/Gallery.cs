using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Labb2_1.Model
{
    public class Gallery
    {
        static readonly Regex ApprovedExtensions;
        static readonly string PhysicalUploadedImagesPath;
        static readonly Regex SanitizePath;

        static public Gallery()
        {
            ApprovedExtensions = new Regex(@"^.*\.(?:gif|jpg|png)$", RegexOptions.IgnoreCase);
            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Uploads");
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        }

        //returnera en IEnumerable collection innehållande textsträngar
        //med de filnamn i Upload-katalogen som har tillåtna bildfils-
        //ändelser.
        static IEnumerable<string> GetImageNames()
        {
            var di = new DirectoryInfo(PhysicalUploadedImagesPath);
            
            //I en lista över alla filer i Upload-katalogen,
            //välj ut de filer vars filnamn som har filändelser
            //som överensstämmer med ApprovedExtensions-regexet.
            var fileNames = di.GetFiles()
                .Select(fi => fi.Name)
                .Where(fiName => ApprovedExtensions.IsMatch(fiName));

            return fileNames;
        }

        //kontrollerar om angivet filnamn finns i Upload-katalogen.
        static bool ImageExists(string fileName)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagesPath, fileName));
        }

        //kontrollerar om angivet Image-objekt har tillåten MIME-typ.
        static bool IsValidImage(Image image)
        {
            return (image.RawFormat.Guid.Equals(ImageFormat.Png.Guid) ||
                image.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid) ||
                image.RawFormat.Guid.Equals(ImageFormat.Gif.Guid));

            //(Är det ett problem att en Jpeg-bild kan sparas med filändelsen .Gif och vice versa?)
            //(Isåfall tar denna metod inte hänsyn till den risken.)
        }

        static void SaveImage(Stream stream, string fileName)
        {
            //skapa ett Image-objekt av den inskickade strömmen
            var image = System.Drawing.Image.FromStream(stream);

            //testa om filnamnet har korrekt filändelse, annars kasta undantag
            if (!ApprovedExtensions.IsMatch(Path.GetExtension(fileName)))
            {
                throw new ArgumentException(String.Format("Incorrect file extension, {0}", Path.GetExtension(fileName)));
            }

            //ta bort otillåtna tecken från filnamnet
            fileName = SanitizePath.Replace(fileName, "");

            //testa om bilden är i korrekt Mime-format, annars kasta undantag
            if (!IsValidImage(image))
            {
                throw new ArgumentException("Image format cannot be recognized");
            }

            //kolla om filnamnet är upptaget, och döp isåfall om filen med ett index
            if (ImageExists(fileName))
            {
                var index = 1;
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                while (ImageExists(String.Format("{0} ({1})", fileNameWithoutExtension, index)))
                {
                    index++;
                }
                fileName = String.Format("{0} ({1}){2}", fileNameWithoutExtension, index, Path.GetExtension(fileName));
            }

            //spara bilden i en fil.
            image.Save(Path.Combine(PhysicalUploadedImagesPath, fileName));

            //skapa en miniatyrbild av bilden
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);

            //skapa ett filnamn för miniatyrbilden som motsvarar filnamnet för bilden.
            var thumbFileName = Path.Combine(Path.GetFileNameWithoutExtension(fileName), "-thumb", Path.GetExtension(fileName));

            //Sannolikheten är väl inte så stor att filen innehållande miniatyrbilden redan finns,
            //men om den finns, måste det vara ok att skriva över den.
            if (ImageExists(thumbFileName))
            {
                File.Delete(Path.Combine(PhysicalUploadedImagesPath, thumbFileName));
            }

            //spara miniatyrbildfilen.
            thumbnail.Save(Path.Combine(PhysicalUploadedImagesPath, thumbFileName)); //ok

        }

    }
}