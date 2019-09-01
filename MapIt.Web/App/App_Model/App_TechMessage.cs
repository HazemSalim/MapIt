using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_TechMessage
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Phone { get; set; }

        public string Sender { get; set; }
        public string SenderPhoto { get; set; }
        public string TextMessage { get; set; }

        public bool IsRead { get; set; }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_TechMessage(TechMessage message)
        {
            Id = message.Id;

            UserId = message.UserId;
            Phone = message.User.Phone;

            Sender = message.Sender;
            SenderPhoto = message.Sender.ToLower() == "support" ? AppSettings.WebsiteURL + AppSettings.Icon : AppSettings.WebsiteURL + message.User.UserPhoto;
            TextMessage = message.TextMessage;

            IsRead = message.IsRead;

            AddedOn = message.AddedOn;
        }
    }
}