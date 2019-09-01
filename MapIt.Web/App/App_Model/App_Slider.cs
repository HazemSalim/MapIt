using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Slider
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        public int? CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public string Link { get; set; }
        public int Ordering { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.SliderPhotos.Trim() + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public App_Slider(Slider slider)
        {
            Id = slider.Id;
            TitleEN = slider.TitleEN;
            TitleAR = slider.TitleAR;
            CountryId = slider.CountryId.HasValue ? slider.CountryId.Value : 0;
            CountryEN = slider.CountryId.HasValue ? slider.Country.TitleEN : string.Empty;
            CountryAR = slider.CountryId.HasValue ? slider.Country.TitleAR : string.Empty;
            Link = slider.Link;
            Ordering = slider.Ordering;
            Photo = slider.Photo;
        }
    }
}