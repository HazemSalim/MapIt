using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_PropertyComponent
    {
        public Int64 PropertyComponentId { get; set; }

        public int ComponentId { get; set; }
        public string ComponentEN { get; set; }
        public string ComponentAR { get; set; }

        public int Count { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.ComponentPhotos + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }
    }
}