using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_PaymentMethod
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public double CostFeePercent { get; set; }

        public App_PaymentMethod(PaymentMethod paymentMethod)
        {
            Id = paymentMethod.Id;
            TitleEN = paymentMethod.TitleEN;
            TitleAR = paymentMethod.TitleAR;
            CostFeePercent = paymentMethod.CostFeePercent;
        }
    }
}
