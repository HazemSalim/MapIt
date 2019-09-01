using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_UserBalanceLog
    {
        public long Id { get; set; }

        public string LogNo { get; set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public double Amount { get; set; }

        public string TransType { get; set; }

        public String TransOnDate
        {
            get
            {
                return this.TransOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime TransOn { get; set; }

        public App_UserBalanceLog(UserBalanceLog log)
        {
            Id = log.Id;

            LogNo = log.LogNo;

            UserId = log.UserId;
            Name = log.User.FullName;
            Phone = log.User.Phone;

            Amount = log.Amount;

            TransType = log.TransType;

            TransOn = log.TransOn;
        }
    }
}