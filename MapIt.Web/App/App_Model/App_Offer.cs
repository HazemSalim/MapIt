using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Helpers;
using MapIt.Data;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Offer
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public string Phone { get; set; }
        public string Link { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.OfferPhotos + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public int ViewersCount { get; set; }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_Offer(MapIt.Data.Offer offer)
        {
            Id = offer.Id;
            TitleEN = offer.TitleEN;
            TitleAR = offer.TitleAR;
            DescriptionEN = !string.IsNullOrEmpty(offer.DescriptionEN) ? offer.DescriptionEN : string.Empty;
            DescriptionAR = !string.IsNullOrEmpty(offer.DescriptionAR) ? offer.DescriptionAR : string.Empty;

            CountryId = offer.CountryId;
            CountryEN = offer.Country.TitleEN;
            CountryAR = offer.Country.TitleAR;

            Phone = offer.Phone;
            Link = !string.IsNullOrEmpty(offer.Link) ? offer.Link : string.Empty;
            Photo = offer.Photo;
            ViewersCount = offer.ViewersCount;
            AddedOn = offer.AddedOn;
        }
    }
}