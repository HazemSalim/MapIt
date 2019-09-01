using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_CommAd
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int? CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.CommAdPhotos.Trim() + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public string Link { get; set; }

        public App_CommAd()
        {

        }

        public App_CommAd(CommercialAd ad)
        {
            Id = ad.Id;
            Title = ad.Title;
            CountryId = ad.CountryId.HasValue ? ad.CountryId.Value : 0;
            CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty;
            CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty;
            Photo = ad.Photo;
            Link = ad.Link;
        }
    }
}