using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_ServiceCategory
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string Photo { get; set; }
        public int ServicesCount { get; set; }
    }
}