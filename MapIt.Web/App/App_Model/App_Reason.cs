using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Reason
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        public App_Reason(Reason obj)
        {
            Id = obj.Id;
            TitleEN = obj.TitleEN;
            TitleAR = obj.TitleAR;
        }
    }
}