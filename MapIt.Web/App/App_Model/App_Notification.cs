using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_Notification
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public long PropertyId { get; set; }
        public long ServiceId { get; set; }
        public int OfferId { get; set; }

        public int TypeId { get; set; }
        public string Type { get; set; }

        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

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

        public App_Notification(Notification notif)
        {
            Id = notif.Id;

            UserId = notif.UserId.HasValue ? notif.UserId.Value : 0;
            Name = notif.UserId.HasValue ? notif.User.FullName : string.Empty;
            Phone = notif.UserId.HasValue ? notif.User.Phone : string.Empty;

            PropertyId = notif.PropertyId.HasValue ? notif.PropertyId.Value : 0;
            ServiceId = notif.ServiceId.HasValue ? notif.ServiceId.Value : 0;
            OfferId = notif.OfferId.HasValue ? notif.OfferId.Value : 0;

            TypeId = notif.TypeId;
            Type = notif.NotifType.Title;

            TitleEN = notif.TitleEN;
            TitleAR = notif.TitleAR;

            IsRead = notif.IsRead;

            AddedOn = notif.AddedOn;
        }
    }
}