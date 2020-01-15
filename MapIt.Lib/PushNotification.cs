using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using JdSoft.Apple.Apns.Notifications;
using MapIt.Helpers;

namespace MapIt.Lib
{
    public class PushNotification
    {
        #region Properties

        private string _GoogleAppID;
        public string GoogleAppID
        {
            get { return _GoogleAppID; }
            set { _GoogleAppID = value; }
        }

        private string _P12FilePath;
        public string P12FilePath
        {
            get { return _P12FilePath; }
            set { _P12FilePath = value; }
        }

        //This is the password that you protected your p12File 
        //If you did not use a password, set it as null or an empty string
        private string _P12FilePassword;
        public string P12FilePassword
        {
            get { return _P12FilePassword; }
            set { _P12FilePassword = value; }
        }

        //True if you are using sandbox certificate, or false if using production
        private bool _Sandbox;
        public bool Sandbox
        {
            get { return _Sandbox; }
            set { _Sandbox = value; }
        }

        private bool _PrintResult;
        public bool PrintResult
        {
            get { return _PrintResult; }
            set { _PrintResult = value; }
        }

        private HttpContext _CurrentContext;
        public HttpContext CurrentContext
        {
            get { return _CurrentContext; }
            set { _CurrentContext = value; }
        }

        #endregion

        public bool SendNotificationForAndroid(string token, string message, Dictionary<string, string> parameters = null)
        {
            try
            {
                string value = HttpUtility.UrlEncode(message);
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + token;

                if ((parameters != null && parameters.Count > 0))
                {
                    foreach (string key in parameters.Keys)
                    {
                        if (!string.IsNullOrEmpty(key) && !key.Trim().Equals(""))
                        {
                            postData += "&data." + key.Trim() + "=" + parameters[key];
                        }
                    }
                }

                Console.WriteLine(postData);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                string sResponseFromServer = tReader.ReadToEnd();

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                bool result = (!string.IsNullOrEmpty(sResponseFromServer) ? true : false);

                if (result == true)
                {
                    LogHelper.LogPushNot("Send notification successfully to token: " + token);
                }

                //if (PrintResult == true && CurrentContext != null)
                //{
                //    if (result == true)
                //    {
                //        LogHelper.LogPushNot("Send notification successfully to token: " + token + "<br />");
                //        CurrentContext.Response.Write("Send notification successfully to token: " + token + "<br />");
                //    }
                //    else
                //    {
                //        LogHelper.LogPushNot("Send notification failed to token: " + token + "<br />");
                //        CurrentContext.Response.Write("Send notification failed to token: " + token + "<br />");
                //    }
                //}

                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                LogHelper.LogPushNot("Send notification failed to android token: " + token);

                return false;
            }
        }

        public bool SendPushNotificationForIphone(string token, string Message, Dictionary<string, string> parameters = null, int pushCounter = 1)
        {
            try
            {
                NotificationService service = new NotificationService(Sandbox, P12FilePath, P12FilePassword, 1)
                {
                    SendRetries = 1,
                    //5 retries before generating notificationfailed event
                    
                    ReconnectDelay = 1
                };

                //.2 seconds

                service.Error += service_Error;
                service.NotificationTooLong += service_NotificationTooLong;

                service.BadDeviceToken += service_BadDeviceToken;
                service.NotificationFailed += service_NotificationFailed;
                service.NotificationSuccess += service_NotificationSuccess;
                service.Connecting += service_Connecting;
                service.Connected += service_Connected;
                service.Disconnected += service_Disconnected;

                Notification alertNotification = new Notification();

                alertNotification.DeviceToken = token;
                alertNotification.Payload.Alert.Body = Message;
                alertNotification.Payload.Sound = "push.mp3";
                alertNotification.Payload.Badge = 0; // pushCounter;

                if ((parameters != null && parameters.Count > 0))
                {
                    foreach (string key in parameters.Keys)
                    {
                        if (!string.IsNullOrEmpty(key) && !key.Trim().Equals(""))
                        {
                            alertNotification.Payload.AddCustom(key.Trim(), parameters[key]);
                        }
                    }
                }

                //Message ID
                alertNotification.Payload.ContentAvailable = 33;

                service.QueueNotification(alertNotification);

                //First, close the service.  
                //This ensures any queued notifications get sent befor the connections are closed
                service.Close();

                //Clean up
                service.Dispose();
                LogHelper.LogPushNot("Send notification success to iphone token: " + token);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                LogHelper.LogPushNot("Send notification failed to iphone token: " + token);

                return false;
            }
        }

        private void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            Console.WriteLine("Bad Device Token: {0}", ex.Message);
        }

        private void service_Disconnected(object sender)
        {
            Console.WriteLine("Disconnected...");
        }

        private void service_Connected(object sender)
        {
            Console.WriteLine("Connected...");
        }

        private void service_Connecting(object sender)
        {
            Console.WriteLine("Connecting...");
        }

        private void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            Console.WriteLine(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

        public void service_NotificationSuccess(object sender, Notification notification)
        {
            if (PrintResult == true && CurrentContext != null)
            {
                LogHelper.LogPushNot("Send notification successfully to token: " + notification.DeviceToken + "<br />");
                CurrentContext.Response.Write("Send notification successfully to token: " + notification.DeviceToken + "<br />");
            }
        }

        public void service_NotificationFailed(object sender, Notification notification)
        {
            if (PrintResult == true && CurrentContext != null)
            {
                LogHelper.LogPushNot("Send notification failed to token: " + notification.DeviceToken + "<br />");
                CurrentContext.Response.Write("Send notification failed to token: " + notification.DeviceToken + "<br />");
            }
        }

        private void service_Error(object sender, Exception ex)
        {
        }

        public string SendPushNotification(Token token, string Message, Dictionary<string, string> parameters)
        {
            string tokenIds = "";
            if (token != null)
            {
                switch ((token.Type))
                {
                    //iphone
                    case 1:
                        if (SendPushNotificationForIphone(token.DToken, Message, parameters, (token.PushCount > 0 ? token.PushCount : 1)))
                        {
                            tokenIds += (string.IsNullOrEmpty(tokenIds) ? "" : ",") + token.Id.ToString();
                        }

                        break;
                    //android
                    case 2:
                        SendNotificationForAndroid(token.DToken, Message, parameters);
                        break;
                }

            }
            return tokenIds;
        }

    }

    public class Token
    {

        private string _Id;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _DToken;
        public string DToken
        {
            get { return _DToken; }
            set { _DToken = value; }
        }

        private int _Type;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private int _PushCount;
        public int PushCount
        {
            get { return _PushCount; }
            set { _PushCount = value; }
        }

    }

    public delegate string AsyncSendPushNotification(Token token, string Message, Dictionary<string, string> parameters);
}
