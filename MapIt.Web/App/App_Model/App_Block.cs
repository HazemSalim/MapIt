using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_Block
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string FullTitleEN { get; set; }
        public string FullTitleAR { get; set; }

        public string Coordinates { get; set; }

        public int AreaId { get; set; }
        public string AreaEN { get; set; }
        public string AreaAR { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public App_Block()
        {

        }

        public App_Block(Block block)
        {
            Id = block.Id;
            TitleEN = block.TitleEN;
            TitleAR = block.TitleAR;
            FullTitleEN = block.Area.City.TitleEN + "-" + block.Area.TitleEN + "-" + block.TitleEN;
            FullTitleAR = block.Area.City.TitleAR + "-" + block.Area.TitleAR + "-" + block.TitleAR;
            Coordinates = block.Coordinates;
            AreaId = block.AreaId;
            AreaEN = block.Area.TitleEN;
            AreaAR = block.Area.TitleAR;
            CityId = block.Area.CityId;
            CityEN = block.Area.City.TitleEN;
            CityAR = block.Area.City.TitleAR;
            CountryId = block.Area.City.CountryId;
            CountryEN = block.Area.City.Country.TitleEN;
            CountryAR = block.Area.City.Country.TitleAR;
        }
    }
}