using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace MapIt.Helpers
{
    public class PresentHelper
    {
        public static bool ResizeImage(string imagePath, string newImagePath, int width)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(imagePath));

                // Auto Height
                int height = (width * image.Height) / image.Width;

                var imageEncoders = ImageCodecInfo.GetImageEncoders();
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 20L);
                var canvasWidth = width;
                var canvasHeight = height;
                var newImageWidth = width;
                var newImageHeight = height;
                var xPosition = 0;
                var yPosition = 0;

                var thumbnail = new Bitmap(canvasWidth, canvasHeight);
                var graphic = Graphics.FromImage(thumbnail);

                graphic.InterpolationMode = InterpolationMode.Low;
                graphic.SmoothingMode = SmoothingMode.HighSpeed;
                graphic.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                graphic.CompositingQuality = CompositingQuality.HighSpeed;
                graphic.DrawImage(image, xPosition, yPosition, newImageWidth, newImageHeight);

                thumbnail.Save(HttpContext.Current.Server.MapPath(newImagePath), imageEncoders[1], encoderParameters);
                image.Dispose();
                graphic.Dispose();
                thumbnail.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return false;
            }
        }

        public static string GetVideoId(string input)
        {
            try
            {
                string youtubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
                var regex = new Regex(youtubeLinkRegex, RegexOptions.Compiled);
                foreach (Match match in regex.Matches(input))
                {
                    foreach (var groupdata in match.Groups.Cast<Group>().Where(groupdata => !groupdata.ToString().StartsWith("http://") && !groupdata.ToString().StartsWith("https://") && !groupdata.ToString().StartsWith("youtu") && !groupdata.ToString().StartsWith("www.")))
                    {
                        return groupdata.ToString();
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string StringLimit(object obj, int limit, bool includeDots = true)
        {
            try
            {
                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    return string.Empty;
                string str = obj.ToString();
                if (str.Length <= limit)
                {
                    return str;
                }
                string s = str.Substring(0, limit);
                if (s.LastIndexOf(" ") != -1)
                {
                    s = s.Substring(0, s.LastIndexOf(" "));

                }

                if (includeDots)
                    s += " ...";

                return s;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        public static string GetDurationEn(DateTime dt)
        {
            if (DateTime.Now > dt)
            {
                var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta < 60)
                {
                    return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                }
                if (delta < 120)
                {
                    return "a minute ago";
                }
                if (delta < 2700) // 45 * 60
                {
                    return ts.Minutes + " minutes ago";
                }
                if (delta < 5400) // 90 * 60
                {
                    return "an hour ago";
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return ts.Hours + " hours ago";
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return "yesterday";
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return ts.Days + " days ago";
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "one month ago" : months + " months ago";
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
            else
            {
                var ts = new TimeSpan(dt.Ticks - DateTime.Now.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta < 60)
                {
                    return ts.Seconds == 1 ? "after one second" : "after " + ts.Seconds + " seconds";
                }
                if (delta < 120)
                {
                    return "after a minute";
                }
                if (delta < 2700) // 45 * 60
                {
                    return "after " + ts.Minutes + " minutes";
                }
                if (delta < 5400) // 90 * 60
                {
                    return "after an hour";
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return "after " + ts.Hours + " hours";
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return "tomorrow";
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return "after " + ts.Days + " days";
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "after one month" : "after " + months + " months";
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "after one year" : "after " + years + " years";
            }
        }

        public static string GetDurationAr(DateTime dt)
        {
            if (DateTime.Now > dt)
            {
                var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta < 60)
                {
                    return ts.Seconds == 1 ? "منذ ثانية" : "منذ " + ts.Seconds + " ثانية";
                }
                if (delta < 120)
                {
                    return "منذ دقيقة";
                }
                if (delta < 2700) // 45 * 60
                {
                    return "منذ " + ts.Minutes + " دقيقة";
                }
                if (delta < 5400) // 90 * 60
                {
                    return "منذ ساعة";
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return "منذ " + ts.Hours + " ساعة";
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return "امس";
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return "منذ " + ts.Days + " يوم";
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "منذ شهر" : "منذ " + months + " شهر";
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "منذ سنة" : "منذ " + years + " سنة";
            }
            else
            {
                var ts = new TimeSpan(dt.Ticks - DateTime.Now.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta < 60)
                {
                    return ts.Seconds == 1 ? "بعد ثانية" : "بعد " + ts.Seconds + (ts.Seconds > 10 ? " ثانية" : " ثواني");
                }
                if (delta < 120)
                {
                    return "بعد دقيقة";
                }
                if (delta < 2700) // 45 * 60
                {
                    return "بعد " + ts.Minutes + (ts.Minutes > 10 ? " دقيقة" : " دقائق");
                }
                if (delta < 5400) // 90 * 60
                {
                    return "بعد ساعة";
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return "بعد " + ts.Hours + (ts.Hours > 10 ? " ساعة" : " ساعات");
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return "غدا";
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return "بعد " + ts.Days + (ts.Days > 10 ? " يوم" : " ايام");
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? "بعد شهر" : "بعد " + months + (months > 10 ? " شهر" : " شهور");
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "بعد سنة" : "بعد " + years + (years > 10 ? " سنة" : " سنوات");
            }
        }

        public static void ConvertGridViewToDataTable(GridView grdv, ref DataTable table)
        {
            // create columns
            for (int i = 0; i < grdv.HeaderRow.Cells.Count; i++)
                table.Columns.Add(grdv.HeaderRow.Cells[i].Text);

            // fill rows
            foreach (GridViewRow row in grdv.Rows)
            {
                DataRow dr;
                dr = table.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace("&nbsp;", " ");
                }
                table.Rows.Add(dr);
            }
        }

        public static string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }

        //public static void ShowScriptMessage(string message, string location = "")
        //{
        //    try
        //    {
        //        string cleanMessage = message.Replace("'", "\'");
        //        Page pageObj = HttpContext.Current.CurrentHandler as Page;
        //        if (pageObj != null)
        //        {
        //            if (String.IsNullOrEmpty(location))
        //            {
        //                System.Web.UI.ScriptManager.RegisterClientScriptBlock(pageObj, pageObj.GetType(), "message", "alert('" + cleanMessage + "');", true);
        //            }
        //            else
        //            {
        //                System.Web.UI.ScriptManager.RegisterClientScriptBlock(pageObj, pageObj.GetType(), "message", "alert('" + cleanMessage + "'); window.location='" + location + "';", true);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.LogException(ex);
        //    }
        //}

        public static void ShowScriptMessage(string message, string location = "", int time = 2000)
        {
            try
            {
                string cleanMessage = message.Replace("'", "\'");
                Page pageObj = HttpContext.Current.CurrentHandler as Page;
                if (pageObj != null)
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(pageObj, pageObj.GetType()
                        , "message", "tempAlert('" + cleanMessage + "','" + location + "', " + time + ");", true);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public static void RunScript(string scriptText)
        {
            try
            {
                Page pageObj = HttpContext.Current.CurrentHandler as Page;
                if (pageObj != null)
                    System.Web.UI.ScriptManager.RegisterClientScriptBlock(pageObj, pageObj.GetType(), "script", scriptText, true);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        public static TokenObj GenerateToken()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://kuwaitportal.paci.gov.kw/arcgis/sharing/generateToken");
                var postData = "username=AqarMapUser";
                postData += "&password=@qaR#7g210";
                postData += "&Referer=requestip";
                postData += "&expiration=999999";
                postData += "&f=pjson";
                var data = Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                TokenObj token = JsonConvert.DeserializeObject<TokenObj>(responseString);
                return token;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }
        public static PACIData GetPACIData(string paciNo, string token)
        {
            try
            {
                WebRequest request = WebRequest.Create("https://kuwaitportal.paci.gov.kw/arcgisportal/rest/services/Hosted/PACIGeocoder/FeatureServer/0/query?where=civilid=" + paciNo + "&returnGeometry=true&f=pjson&token=" + token + "&outFields=*&returnFieldName=false");
                request.Method = "GET";
                WebResponse myWebResponse = request.GetResponse();
                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                Console.WriteLine("\nResponse stream received");
                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                Console.WriteLine("HTML...\r\n");
                string res = "";
                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Console.Write(str);
                    count = readStream.Read(read, 0, 256);
                    res = res + "" + str;
                }

                RootObject root = JsonConvert.DeserializeObject<RootObject>(res);
                PACIData obj = root.features.ToList().Count() > 0 ? root.features.ToList().FirstOrDefault().attributes : null;
                readStream.Close();
                myWebResponse.Close();

                return obj;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return null;
            }
        }
        public class RootObject
        {
            public List<Feature> features { get; set; }
        }
        public class Feature
        {
            public PACIData attributes { get; set; }
        }
    }
    public class PACIData
    {
        public Int64 objectid { get; set; }
        public string blockarabic { get; set; }
        public string housearabic { get; set; }
        public string parcelarabic { get; set; }
        public Int64 civilid { get; set; }
        public string streetenglish { get; set; }
        public string streetarabic { get; set; }
        public string parcelenglish { get; set; }
        public string houseenglish { get; set; }
        public string neighborhoodarabic { get; set; }
        public string neighborhoodenglish { get; set; }
        public string governoratearabic { get; set; }
        public string governorateenglish { get; set; }
        public string location { get; set; }
        public string blockenglish { get; set; }
        public string unit_no { get; set; }
        public string floor_no { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string buildingnamearabic { get; set; }
        public string buildingnameenglish { get; set; }
        public string buildingtypearabic { get; set; }
        public string buildingktypeenglish { get; set; }
        public Int64 neighborhoodid { get; set; }
        public Int64 governorateid { get; set; }
        public float ktm_x { get; set; }
        public float ktm_y { get; set; }
        public float utm_x { get; set; }
        public float utm_y { get; set; }
        public float wsphere_x { get; set; }
        public float wsphere_y { get; set; }
    }
    public class TokenObj
    {
        public string Token { get; set; }
        public string Expires { get; set; }
        public string Ssl { get; set; }
    }
}
