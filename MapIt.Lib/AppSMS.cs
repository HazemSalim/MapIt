using System.Net;
using System;
using System.Web;
using System.Text;
using System.IO;
using System.Collections;
using MapIt.Helpers;
using MessageBird;

namespace MapIt.Lib
{
    public class AppSMS
    {
        public static void Send(string messageBody, string phonenumber)
        {
            Client client = Client.CreateDefault(AppSettings.SMSAccessKey, null);

            MessageBird.Objects.Message message =
            client.SendMessage(AppSettings.SMSOriginator, messageBody, new[] { ParseHelper.GetInt64(phonenumber).Value });
        }
    }
}