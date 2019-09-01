using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_City
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }

        public string Coordinates { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public App_City(City city)
        {
            Id = city.Id;
            TitleEN = city.TitleEN;
            TitleAR = city.TitleAR;
            Coordinates = city.Coordinates;

            CountryId = city.CountryId;
            CountryEN = city.Country.TitleEN;
            CountryAR = city.Country.TitleAR;
        }
    }
}