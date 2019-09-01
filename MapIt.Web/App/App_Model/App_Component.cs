using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Component
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

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

        public App_Component(GetComponents_Result component)
        {
            Id = component.Id;
            TitleEN = component.TitleEN;
            TitleAR = component.TitleAR;
            Photo = component.Photo;
        }
    }
}