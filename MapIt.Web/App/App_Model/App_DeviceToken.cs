using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_DeviceToken
    {
        public int Id { get; set; }

        public long UserId { get; set; }
        public string Phone { get; set; }

        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public int DeviceType { get; set; }
        public int PushCounter { get; set; }
        public bool IsLogged { get; set; }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_DeviceToken(DevicesToken token)
        {
            Id = token.Id;

            UserId = token.UserId.HasValue ? token.UserId.Value : 0;
            Phone = token.UserId.HasValue ? token.User.Phone : string.Empty;

            DeviceId = !string.IsNullOrEmpty(token.DeviceId) ? token.DeviceId : string.Empty;
            DeviceToken = token.DeviceToken;
            DeviceType = token.DeviceType;
            PushCounter = token.PushCounter;
            IsLogged = token.IsLogged;
            AddedOn = token.AddedOn;
        }
    }
}