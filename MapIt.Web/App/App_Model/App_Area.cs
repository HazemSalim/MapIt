using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_Area
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string FullTitleEN { get; set; }
        public string FullTitleAR { get; set; }

        public string Coordinates { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public App_Area()
        {

        }

        public App_Area(Area area)
        {
            Id = area.Id;
            TitleEN = area.TitleEN;
            TitleAR = area.TitleAR;
            FullTitleEN = area.City.TitleEN + "-" + area.TitleEN;
            FullTitleAR = area.City.TitleAR + "-" + area.TitleAR;
            Coordinates = area.Coordinates;
            CityId = area.CityId;
            CityEN = area.City.TitleEN;
            CityAR = area.City.TitleAR;
            CountryId = area.City.CountryId;
            CountryEN = area.City.Country.TitleEN;
            CountryAR = area.City.Country.TitleAR;
        }
    }
}