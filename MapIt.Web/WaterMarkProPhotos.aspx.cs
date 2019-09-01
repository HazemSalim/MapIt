using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;

namespace MapIt.Web
{
    public partial class WaterMarkProPhotos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DirectoryInfo dir = new DirectoryInfo(MapPath(AppSettings.PropertyPhotos));
                FileInfo[] files = dir.GetFiles();
                ArrayList listItems = new ArrayList();
                foreach (FileInfo info in files)
                {
                    SaveAsWaterMark(info.Name);
                    listItems.Add(info);
                }
            }
        }

        void SaveAsWaterMark(string imageName)
        {
            try
            {
                System.Drawing.Bitmap image = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(Server.MapPath(AppSettings.PropertyPhotos + imageName));
                System.Drawing.Bitmap waterMarkImage = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(Server.MapPath(AppSettings.WaterMark));

                Graphics graphics = System.Drawing.Graphics.FromImage(image);
                waterMarkImage.SetResolution(graphics.DpiX, graphics.DpiY);

                int width = image.Width / 6;
                int height = (width * waterMarkImage.Height) / waterMarkImage.Width;

                int x = 10;
                int y = image.Height - ((image.Height * 30) / 100);

                graphics.DrawImage(waterMarkImage, x, y, width, height);
                image.Save(Server.MapPath(AppSettings.PropertyWMPhotos + imageName));
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos300 + imageName, 300);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos540 + imageName, 540);
                PresentHelper.ResizeImage(AppSettings.PropertyWMPhotos + imageName, AppSettings.PropertyWMPhotos1080 + imageName, 1080);

                graphics.Dispose();
                waterMarkImage.Dispose();
                image.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
    }
}