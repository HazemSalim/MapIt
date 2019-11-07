using System;
using System.Collections.Generic;
using System.Linq;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.App.App_Model
{
    public class App_Service
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ServiceUrl { get; set; }

        public int CategoryId { get; set; }
        public string CategoryEN { get; set; }
        public string CategoryAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public long UserId { get; set; }
        public string Phone { get; set; }

        public int ExYears { get; set; }

        public string OtherPhones { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public bool AllAreas { get; set; }
        public bool IsCompany { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsReport { get; set; }
        public bool IsViewed { get; set; }
        public bool IsSentComment { get; set; }
        public bool AdminAdded { get; set; }

        public int Rate { get; set; }
        public int RatersCount { get; set; }
        public int ViewersCount { get; set; }

        public int UnreadMessageCount { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public string DurationEN { get; set; }
        public string DurationAR { get; set; }

        public IEnumerable<App_ServicePhoto> ServicePhotos { get; set; }
        public IEnumerable<App_Area> Areas { get; set; }
        public IEnumerable<App_CommAd> CommAds { get; set; }

        CommercialAdsRepository commercialAdsRepository = new CommercialAdsRepository();

        public App_Service(MapIt.Data.Service service)
        {
            Id = service.Id;
            Title = service.Title;
            Description = service.Description;
            ServiceUrl = AppSettings.WebsiteURL + "Srv/" + service.PageName;

            CategoryId = service.CategoryId;
            CategoryEN = service.ServicesCategory.TitleEN;
            CategoryAR = service.ServicesCategory.TitleAR;

            CountryId = service.City.Country.Id;
            CountryEN = !string.IsNullOrEmpty(service.City.Country.TitleEN) ? service.City.Country.TitleEN : string.Empty;
            CountryAR = !string.IsNullOrEmpty(service.City.Country.TitleAR) ? service.City.Country.TitleAR : string.Empty;

            CityId = service.CityId;
            CityEN = !string.IsNullOrEmpty(service.City.TitleEN) ? service.City.TitleEN : string.Empty;
            CityAR = !string.IsNullOrEmpty(service.City.TitleAR) ? service.City.TitleAR : string.Empty;

            UserId = service.UserId;
            Phone = service.User.Phone;

            ExYears = service.ExYears;

            OtherPhones = !string.IsNullOrEmpty(service.OtherPhones) ? service.OtherPhones :
                (!string.IsNullOrEmpty(service.User.OtherPhones) ? service.User.OtherPhones : service.User.Phone);

            Longitude = service.Longitude;
            Latitude = service.Latitude;

            AllAreas = service.AllAreas;
            IsCompany = service.IsCompany;
            IsFavorite = false;
            IsReport = false;
            IsViewed = false;
            IsSentComment = false;
            AdminAdded = service.AdminAdded;
            Rate = 3;
            RatersCount = service.ServiceRates.Count;
            ViewersCount = service.ViewersCount;

            UnreadMessageCount = 0;

            Photo = service.ServicePhoto;
            AddedOn = service.AddedOn;

            DurationEN = PresentHelper.GetDurationEn(service.AddedOn);
            DurationAR = PresentHelper.GetDurationAr(service.AddedOn);

            ServicePhotos = service.ServicePhotos.Select(sp => new App_ServicePhoto
            {
                ServicePhotoId = sp.Id,
                Path = AppSettings.WebsiteURL + AppSettings.ServiceWMPhotos,
                Photo = sp.Photo
            });

            Areas = service.ServiceAreas.Select(sa => new App_Area
            {
                Id = sa.Area.Id,
                TitleEN = sa.Area.TitleEN,
                TitleAR = sa.Area.TitleAR,
                FullTitleEN = sa.Area.City.TitleEN + "-" + sa.Area.TitleEN,
                FullTitleAR = sa.Area.City.TitleAR + "-" + sa.Area.TitleAR,
                Coordinates = sa.Area.Coordinates,
                CityId = sa.Area.CityId,
                CityEN = sa.Area.City.TitleEN,
                CityAR = sa.Area.City.TitleAR,
                CountryId = sa.Area.City.CountryId,
                CountryEN = sa.Area.City.Country.TitleEN,
                CountryAR = sa.Area.City.Country.TitleAR
            });

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    CountryId = ad.CountryId.HasValue ? ad.CountryId.Value : 0,
                    CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty,
                    CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty,
                    Photo = ad.Photo,
                    Link = ad.Link
                });
        }

        public App_Service(Data.Service service, long userId)
        {
            Id = service.Id;
            Title = service.Title;
            Description = service.Description;

            CategoryId = service.CategoryId;
            CategoryEN = service.ServicesCategory.TitleEN;
            CategoryAR = service.ServicesCategory.TitleAR;

            CountryId = service.City.Country.Id;
            CountryEN = !string.IsNullOrEmpty(service.City.Country.TitleEN) ? service.City.Country.TitleEN : string.Empty;
            CountryAR = !string.IsNullOrEmpty(service.City.Country.TitleAR) ? service.City.Country.TitleAR : string.Empty;

            CityId = service.CityId;
            CityEN = !string.IsNullOrEmpty(service.City.TitleEN) ? service.City.TitleEN : string.Empty;
            CityAR = !string.IsNullOrEmpty(service.City.TitleAR) ? service.City.TitleAR : string.Empty;

            UserId = service.UserId;
            Phone = service.User.Phone;

            ExYears = service.ExYears;

            OtherPhones = service.OtherPhones;

            Longitude = service.Longitude;
            Latitude = service.Latitude;

            AllAreas = service.AllAreas;
            IsCompany = service.IsCompany;
            IsFavorite = false;
            IsReport = false;
            IsViewed = false;
            IsSentComment = false;
            AdminAdded = service.AdminAdded;
            Rate = 3;
            RatersCount = service.ServiceRates.Count;
            ViewersCount = service.ViewersCount;

            UnreadMessageCount = service.ServiceComments.Count(sc => !sc.IsRead && sc.ReceiverId == userId);

            Photo = service.ServicePhoto;
            AddedOn = service.AddedOn;

            DurationEN = PresentHelper.GetDurationEn(service.AddedOn);
            DurationAR = PresentHelper.GetDurationAr(service.AddedOn);

            ServicePhotos = service.ServicePhotos.Select(sp => new App_ServicePhoto
            {
                ServicePhotoId = sp.Id,
                Path = AppSettings.WebsiteURL + AppSettings.ServiceWMPhotos,
                Photo = sp.Photo
            });

            Areas = service.ServiceAreas.Select(sa => new App_Area
            {
                Id = sa.Area.Id,
                TitleEN = sa.Area.TitleEN,
                TitleAR = sa.Area.TitleAR,
                FullTitleEN = sa.Area.City.TitleEN + "-" + sa.Area.TitleEN,
                FullTitleAR = sa.Area.City.TitleAR + "-" + sa.Area.TitleAR,
                Coordinates = sa.Area.Coordinates,
                CityId = sa.Area.CityId,
                CityEN = sa.Area.City.TitleEN,
                CityAR = sa.Area.City.TitleAR,
                CountryId = sa.Area.City.CountryId,
                CountryEN = sa.Area.City.Country.TitleEN,
                CountryAR = sa.Area.City.Country.TitleAR
            });

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    CountryId = ad.CountryId.HasValue ? ad.CountryId.Value : 0,
                    CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty,
                    CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty,
                    Photo = ad.Photo,
                    Link = ad.Link
                });
        }

        public App_Service(MapIt.Data.CommercialAd adObj)
        {
            Id = -1;
            Title = adObj.Id.ToString();
            Description = !String.IsNullOrEmpty(adObj.Photo) ? adObj.Link : string.Empty;

            CategoryId = 0;
            CategoryEN = string.Empty;
            CategoryAR = string.Empty;

            CountryId = adObj.CountryId.HasValue ? adObj.CountryId.Value : 0;
            CountryEN = adObj.CountryId.HasValue ? adObj.Country.TitleEN : string.Empty;
            CountryAR = adObj.CountryId.HasValue ? adObj.Country.TitleAR : string.Empty;

            CityId = 0;
            CityEN = string.Empty;
            CityAR = string.Empty;

            UserId = 0;
            Phone = string.Empty;

            ExYears = 0;

            OtherPhones = string.Empty;

            Longitude = string.Empty;
            Latitude = string.Empty;

            AllAreas = false;
            IsCompany = false;
            IsFavorite = false;
            IsViewed = false;
            IsReport = false;
            IsSentComment = false;
            AdminAdded = false;
            Rate = 0;
            RatersCount = 0;
            ViewersCount = 0;

            UnreadMessageCount = 0;

            Photo = !String.IsNullOrEmpty(adObj.Photo) ? string.Concat(AppSettings.CommAdPhotos + adObj.Photo) : string.Empty;
            AddedOn = adObj.AddedOn;

            DurationEN = string.Empty;
            DurationAR = string.Empty;

            ServicePhotos = new List<App_ServicePhoto>();

            Areas = new List<App_Area>();

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    CountryId = ad.CountryId.HasValue ? ad.CountryId.Value : 0,
                    CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty,
                    CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty,
                    Photo = ad.Photo,
                    Link = ad.Link
                });
        }
    }
}