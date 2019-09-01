using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_WatchList
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public int TypeId { get; set; }
        public string TypeEN { get; set; }
        public string TypeAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public int AreaId { get; set; }
        public string AreaEN { get; set; }
        public string AreaAR { get; set; }

        public int PurposeId { get; set; }
        public string PurposeEN { get; set; }
        public string PurposeAR { get; set; }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_WatchList(WatchList list)
        {
            Id = list.Id;

            UserId = list.UserId;
            Name = list.User.FullName;
            Phone = list.User.Phone;

            TypeId = list.TypeId;
            TypeEN = list.PropertyType.TitleEN;
            TypeAR = list.PropertyType.TitleAR;

            CountryId = list.CountryId.HasValue ? list.CountryId.Value : 0;
            CountryEN = list.CountryId.HasValue ? list.Country.TitleEN : string.Empty;
            CountryAR = list.CountryId.HasValue ? list.Country.TitleAR : string.Empty;

            CityId = list.CityId.HasValue ? list.CityId.Value : 0;
            CityEN = list.CityId.HasValue ? list.City.TitleEN : string.Empty;
            CityAR = list.CityId.HasValue ? list.City.TitleAR : string.Empty;

            AreaId = list.AreaId.HasValue ? list.AreaId.Value : 0;
            AreaEN = list.AreaId.HasValue ? list.Area.TitleEN : string.Empty;
            AreaAR = list.AreaId.HasValue ? list.Area.TitleAR : string.Empty;

            PurposeId = list.PurposeId.HasValue ? list.PurposeId.Value : 0;
            PurposeEN = list.PurposeId.HasValue ? list.Purpos.TitleEN : string.Empty;
            PurposeAR = list.PurposeId.HasValue ? list.Purpos.TitleAR : string.Empty;

            AddedOn = list.AddedOn;
        }
    }
}