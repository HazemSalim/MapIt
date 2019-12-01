using System;
using System.Collections.Generic;
using System.Linq;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_Broker
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullNameEN { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Link { get; set; }

        public string DetailsEN { get; set; }
        public string DetailsAR { get; set; }

        public bool AllAreas { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.BrokerPhotos + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public string AddedOnDate
        {
            get
            {
                return AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public IEnumerable<App_Area> Areas { get; set; }

        public App_Broker(Data.Broker broker)
        {
            Id = broker.Id;
            FullName = broker.FullName;
            FullNameEN = broker.FullNameEN;

            CountryId = broker.City.CountryId;
            CountryEN = broker.City.Country.TitleEN;
            CountryAR = broker.City.Country.TitleAR;

            CityId = broker.CityId;
            CityEN = broker.City.TitleEN;
            CityAR = broker.City.TitleAR;

            Phone = broker.Phone;
            Email = !string.IsNullOrEmpty(broker.Email) ? broker.Email : string.Empty;
            Link = !string.IsNullOrEmpty(broker.Link) ? broker.Link : string.Empty;

            DetailsEN = !string.IsNullOrEmpty(broker.DetailsEN) ? broker.DetailsEN : string.Empty;
            DetailsAR = !string.IsNullOrEmpty(broker.DetailsAR) ? broker.DetailsAR : string.Empty;

            AllAreas = broker.AllAreas;

            Photo = broker.Photo;
            AddedOn = broker.AddedOn;

            Areas = broker.BrokerAreas.Select(ba => new App_Area
            {
                Id = ba.Area.Id,
                TitleEN = ba.Area.TitleEN,
                TitleAR = ba.Area.TitleAR,
                FullTitleEN = ba.Area.City.TitleEN + "-" + ba.Area.TitleEN,
                FullTitleAR = ba.Area.City.TitleAR + "-" + ba.Area.TitleAR,
                Coordinates = ba.Area.Coordinates,
                CityId = ba.Area.CityId,
                CityEN = ba.Area.City.TitleEN,
                CityAR = ba.Area.City.TitleAR,
                CountryId = ba.Area.City.CountryId,
                CountryEN = ba.Area.City.Country.TitleEN,
                CountryAR = ba.Area.City.Country.TitleAR
            });
        }
    }
}