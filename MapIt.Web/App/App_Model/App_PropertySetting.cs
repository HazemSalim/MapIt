using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_PropertySetting
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string CArea { get; set; }
        public string Block { get; set; }
        public string Area { get; set; }
        public string BuildingAge { get; set; }
        public string MonthlyIncome { get; set; }
        public string SellingPrice { get; set; }
        public string RentPrice { get; set; }
        public string Details { get; set; }
        public string Component { get; set; }
        public string Feature { get; set; }
    }
}