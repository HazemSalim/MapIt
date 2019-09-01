using System;
using System.IO;
using System.Web;

namespace MapIt.Helpers
{
    public class LogHelper
    {
        public static void LogException(Exception ex)
        {
            try
            {
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/log/log_ex.txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("");
                sw.WriteLine("Exception Time :   " + DateTime.Now.ToString("d/M/yyyy hh:mm"));
                sw.WriteLine("");
                sw.WriteLine("Exception Message :   ");
                sw.WriteLine(ex.Message);
                sw.WriteLine("");
                sw.WriteLine("Exception InnerException :   ");
                sw.WriteLine(ex.InnerException);
                sw.WriteLine("");
                sw.WriteLine("Exception StackTrace :   ");
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine("");
                sw.WriteLine("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ");
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
            catch
            {
            }
        }

        public static void LogPayment(string ordNo, string error)
        {
            try
            {
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/log/log_pay.txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("");
                sw.WriteLine("Error Time :   " + DateTime.Now.ToString("d/M/yyyy hh:mm"));
                sw.WriteLine("");
                sw.WriteLine("Error Code :   ");
                sw.WriteLine(error);
                sw.WriteLine("");
                sw.WriteLine("Order No. :   ");
                sw.WriteLine(ordNo);
                sw.WriteLine("");
                sw.WriteLine("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ");
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
            catch
            {
            }
        }

        public static void LogPushNot(string message)
        {
            try
            {
                FileStream fs = new FileStream(Path.Combine(HttpRuntime.AppDomainAppPath, "log/log_push.txt"), FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("");
                sw.WriteLine("Push Time :   " + DateTime.Now.ToString("d/M/yyyy hh:mm"));
                sw.WriteLine("");
                sw.WriteLine("Push Message :   ");
                sw.WriteLine(message);
                sw.WriteLine("");
                sw.WriteLine("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ");
                sw.Close();
                sw.Dispose();
                fs.Close();
                fs.Dispose();
            }
            catch
            {
            }
        }
    }
}
