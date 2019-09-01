using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using MapIt.Helpers;
using MapIt.Lib;

namespace MapIt.Web.App
{
    public partial class AppSPUF : System.Web.UI.Page
    {
        string SaveImage(HttpPostedFile httpPostedFileObj)
        {
            try
            {
                string imagePath = string.Empty;

                string photoName = httpPostedFileObj.FileName;
                string photoPath = AppSettings.ServicePhotos + photoName;

                httpPostedFileObj.SaveAs(Server.MapPath(photoPath));
                imagePath = AppSettings.WebsiteURL + photoPath;
                SaveAsWaterMark(httpPostedFileObj, photoName);

                return imagePath;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }

        void SaveAsWaterMark(HttpPostedFile flfile, string imageName)
        {
            try
            {
                byte[] imageByte = null;
                using (var binaryReader = new BinaryReader(flfile.InputStream))
                {
                    imageByte = binaryReader.ReadBytes(flfile.ContentLength);
                }

                MemoryStream memStream = new MemoryStream(imageByte);
                System.Drawing.Bitmap image = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(memStream);
                System.Drawing.Bitmap waterMarkImage = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(Server.MapPath("~" + AppSettings.WaterMark));

                Graphics graphics = System.Drawing.Graphics.FromImage(image);
                waterMarkImage.SetResolution(graphics.DpiX, graphics.DpiY);

                int width = image.Width / 6;
                int height = (width * waterMarkImage.Height) / waterMarkImage.Width;

                int x = 10;
                int y = image.Height - ((image.Height * 30) / 100);

                graphics.DrawImage(waterMarkImage, x, y, width, height);
                image.Save(Server.MapPath("~" + AppSettings.ServiceWMPhotos + imageName));
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.ServiceWMPhotos300 + imageName, 300);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.ServiceWMPhotos540 + imageName, 540);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.ServiceWMPhotos1080 + imageName, 1080);

                graphics.Dispose();
                waterMarkImage.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection uploadedFiles = Request.Files;

            for (int i = 0; i < uploadedFiles.Count; i++)
            {
                try
                {
                    HttpPostedFile userPostedFile = uploadedFiles[i];

                    if (userPostedFile.ContentLength > 0)
                    {
                        string imagePath = SaveImage(userPostedFile);

                        lblResult.Text += "<u>File #" + (i + 1) + "</u><br>";
                        lblResult.Text += "File Content Type: " + userPostedFile.ContentType + "<br>";
                        lblResult.Text += "File Size: " + userPostedFile.ContentLength + "kb<br>";
                        lblResult.Text += "File Name: " + userPostedFile.FileName + "<br>";

                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            lblResult.Text += "Location where saved: " + imagePath + "<br />";
                        }
                        else
                        {
                            lblResult.Text += "Unable to save photo  <br /> ";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblResult.Text += "Error: <br />" + ex.Message;
                }
            }
        }
    }
}