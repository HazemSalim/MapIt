﻿using System;
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
    public partial class AppVUF : Page
    {
        string SaveVideo(HttpPostedFile httpPostedFileObj)
        {
            try
            {
                string filePath = string.Empty;

                string videoName = httpPostedFileObj.FileName;
                string videoPath = AppSettings.PropertyVideos + videoName;

                httpPostedFileObj.SaveAs(Server.MapPath(videoPath));
                filePath = AppSettings.WebsiteURL + videoPath;

                return filePath;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
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
                        string imagePath = SaveVideo(userPostedFile);

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
                            lblResult.Text += "Unable to save video  <br /> ";
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