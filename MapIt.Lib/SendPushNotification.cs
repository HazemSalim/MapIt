using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Repository;

namespace MapIt.Lib
{
    public class SendPushNotification
    {
        public static void Send(string id, string id2, string type, string details = "")
        {
            try
            {
                var devicesTokensRepository = new DevicesTokensRepository();
                var dtList = devicesTokensRepository.GetAll();

                if (dtList != null && dtList.Count() > 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Id", id);
                    dic.Add("Id2", id);
                    dic.Add("Type", type);

                    string msg = "";
                    if (!details.Trim().Equals(""))
                    {
                        msg = details;
                    }

                    PushNotification push = new PushNotification();
                    push.GoogleAppID = ConfigurationManager.AppSettings["GoogleAppID"];
                    push.P12FilePath = ConfigurationManager.AppSettings["P12FilePath"];
                    push.P12FilePassword = ConfigurationManager.AppSettings["P12FilePassword"];
                    push.Sandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["Sandbox"]);
                    push.PrintResult = Convert.ToBoolean(ConfigurationManager.AppSettings["PrintResult"]);
                    push.CurrentContext = HttpContext.Current;

                    foreach (var item in dtList)
                    {
                        // Increasing Push Counter
                        devicesTokensRepository = new DevicesTokensRepository();
                        devicesTokensRepository.IncreasePushCounter(item.DeviceToken);

                        Token t = new Token();
                        int tokenId = item.Id;
                        string token = item.DeviceToken;
                        int tokenType = item.DeviceType;
                        int pushCounter = item.PushCounter;
                        if (!token.Trim().Equals("") && tokenType > 0)
                        {
                            t.Id = tokenId.ToString();
                            t.Type = tokenType;
                            t.DToken = token;
                            t.PushCount = pushCounter + 1;
                        }

                        AsyncSendPushNotification del = new AsyncSendPushNotification(push.SendPushNotification);
                        IAsyncResult result = del.BeginInvoke(t, msg, dic, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        public static void Send(string id, string id2, string type, List<DevicesToken> dt, string details = "")
        {
            try
            {
                DevicesTokensRepository devicesTokensRepository;
                devicesTokensRepository = new DevicesTokensRepository();

                if (dt != null && dt.Count > 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Id", id);
                    dic.Add("Id2", id);
                    dic.Add("Type", type);

                    string msg = "";
                    if (!details.Trim().Equals(""))
                    {
                        msg = details;
                    }

                    PushNotification push = new PushNotification();
                    push.GoogleAppID = ConfigurationManager.AppSettings["GoogleAppID"];
                    push.P12FilePath = ConfigurationManager.AppSettings["P12FilePath"];
                    push.P12FilePassword = ConfigurationManager.AppSettings["P12FilePassword"];
                    push.Sandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["Sandbox"]);
                    push.PrintResult = Convert.ToBoolean(ConfigurationManager.AppSettings["PrintResult"]);
                    push.CurrentContext = HttpContext.Current;

                    foreach (var item in dt)
                    {
                        // Increasing Push Counter
                        devicesTokensRepository = new DevicesTokensRepository();
                        devicesTokensRepository.IncreasePushCounter(item.DeviceToken);

                        Token t = new Token();
                        int tokenId = item.Id;
                        string token = item.DeviceToken;
                        int tokenType = item.DeviceType;
                        int pushCounter = item.PushCounter;
                        if (!token.Trim().Equals("") && tokenType > 0)
                        {
                            t.Id = tokenId.ToString();
                            t.Type = tokenType;
                            t.DToken = token;
                            t.PushCount = pushCounter + 1;
                        }

                        AsyncSendPushNotification del = new AsyncSendPushNotification(push.SendPushNotification);
                        IAsyncResult result = del.BeginInvoke(t, msg, dic, null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
    }
}
