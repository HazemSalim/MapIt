using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Purpos
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string ShortTitleAR { get; set; }
        public string ShortTitleEN { get; set; }

        public App_Purpos(Purpos obj)
        {
            Id = obj.Id;
            TitleEN = obj.TitleEN;
            TitleAR = obj.TitleAR;
            ShortTitleEN = obj.ShortTitleEN;
            ShortTitleAR = obj.ShortTitleAR;
        }
    }
}