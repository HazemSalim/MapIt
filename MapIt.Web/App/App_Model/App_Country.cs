using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Country
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        public string CCode { get; set; }
        public string ISOCode { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL+AppSettings.CountryPhotos + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public App_Country(Country country)
        {
            Id = country.Id;
            TitleEN = country.TitleEN;
            TitleAR = country.TitleAR;
            CCode = country.CCode;
            ISOCode = country.ISOCode;
            Photo = country.Photo;
        }
    }
}