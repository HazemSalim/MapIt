using System;
using System.Collections.Generic;
using System.Linq;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Web.Script.Serialization;

namespace MapIt.Web.App.App_Model
{
    public class App_Property
    {
        public long Id { get; set; }
        public string PageURL { get; set; }

        public long UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string UserPhoto { get; set; }
        public string PropertyUrl { get; set; }

        public int PurposeId { get; set; }
        public string PurposeEN { get; set; }
        public string PurposeAR { get; set; }

        public int TypeId { get; set; }
        public string TypeEN { get; set; }
        public string TypeAR { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyAR { get; set; }
        public string CurrencyEN { get; set; }

        public int CityId { get; set; }
        public string CityEN { get; set; }
        public string CityAR { get; set; }

        public int AreaId { get; set; }
        public string AreaEN { get; set; }
        public string AreaAR { get; set; }

        public int BlockId { get; set; }
        public string BlockEN { get; set; }
        public string BlockAR { get; set; }

        public double Area { get; set; }
        public int BuildingAge { get; set; }
        public double MonthlyIncome { get; set; }
        public double SellingPrice { get; set; }
        public double RentPrice { get; set; }

        public string Details { get; set; }

        public string OtherPhones { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string AddressEN { get; set; }
        public string AddressAR { get; set; }

        public string Street { get; set; }
        public string PACI { get; set; }

        public int ViewersCount { get; set; }
        public int FavoritesCount { get; set; }

        public int IsSpecial { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsReport { get; set; }
        public bool IsViewed { get; set; }
        public bool IsSentComment { get; set; }
        public bool AdminAdded { get; set; }

        public int UnreadMessageCount { get; set; }

        string _Photo;
        public string Photo
        {
            get
            {
                if (!string.IsNullOrEmpty(_Photo))
                    return AppSettings.WebsiteURL + AppSettings.PropertyPhotos.Trim() + _Photo;

                return string.Empty;
            }
            set { _Photo = value; }
        }

        public string AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public string DurationEN { get; set; }
        public string DurationAR { get; set; }

        public IEnumerable<App_PropertyComponent> PropertyComponents { get; set; }
        public IEnumerable<App_PropertyFeature> PropertyFeatures { get; set; }
        public IEnumerable<App_PropertyPhoto> PropertyPhotos { get; set; }
        public IEnumerable<App_PropertyVideo> PropertyVideos { get; set; }
        public IEnumerable<App_CommAd> CommAds { get; set; }

        CommercialAdsRepository commercialAdsRepository = new CommercialAdsRepository();

        public App_Property()
        {

        }

        public App_Property(Data.Property property)
        {
            Id = property.Id;
            PageURL = property.PageName;
            UserId = property.UserId;
            Name = !String.IsNullOrEmpty(property.User.FullName) ? property.User.FullName : string.Empty;
            Phone = property.User.Phone;
            UserPhoto = AppSettings.WebsiteURL + property.User.UserPhoto;
            PropertyUrl = AppSettings.WebsiteURL + "Pro/" + property.PageName;

            PurposeId = property.PurposeId;
            PurposeEN = property.Purpos.TitleEN;
            PurposeAR = property.Purpos.TitleAR;

            TypeId = property.TypeId;
            TypeEN = property.PropertyType.TitleEN;
            TypeAR = property.PropertyType.TitleAR;

            CountryId = property.CountryId;
            CountryEN = property.Country.TitleEN;
            CountryAR = property.Country.TitleAR;
            CountryCode = property.Country.CCode;
            CurrencyEN = string.Empty;
            CurrencyAR = string.Empty;

            CityId = property.BlockId.HasValue ? property.Block.Area.CityId : 0;
            CityEN = property.BlockId.HasValue ? property.Block.Area.City.TitleEN : string.Empty;
            CityAR = property.BlockId.HasValue ? property.Block.Area.City.TitleAR : string.Empty;

            AreaId = property.BlockId.HasValue ? property.Block.AreaId : 0;
            AreaEN = property.BlockId.HasValue ? property.Block.Area.TitleEN : string.Empty;
            AreaAR = property.BlockId.HasValue ? property.Block.Area.TitleAR : string.Empty;

            BlockId = property.BlockId ?? 0;
            BlockEN = property.BlockId.HasValue ? property.Block.TitleEN : string.Empty;
            BlockAR = property.BlockId.HasValue ? property.Block.TitleAR : string.Empty;

            Area = property.Area ?? 0;
            BuildingAge = property.BuildingAge ?? 0;
            MonthlyIncome = property.MonthlyIncome ?? 0;
            SellingPrice = property.SellingPrice ?? 0;
            RentPrice = property.RentPrice ?? 0;

            Details = !string.IsNullOrEmpty(property.Details) ? property.Details : string.Empty;

            OtherPhones = !string.IsNullOrEmpty(property.OtherPhones) ? property.OtherPhones :
                (!string.IsNullOrEmpty(property.User.OtherPhones) ? property.User.OtherPhones : property.User.Phone);

            Longitude = !string.IsNullOrEmpty(property.DLongitude.ToString()) ? property.DLongitude.ToString() : string.Empty;
            Latitude = !string.IsNullOrEmpty(property.DLatitude.ToString()) ? property.DLatitude.ToString() : string.Empty;

            AddressEN = property.AddressEN;
            AddressAR = property.AddressAR;

            Street = property.Street;
            PACI = property.PACI;

            ViewersCount = property.ViewersCount;
            FavoritesCount = property.PropertyFavorites.Count;

            IsSpecial = property.IsSpecial ? 1 : 0;
            IsFavorite = false;
            IsReport = false;
            IsViewed = false;
            IsSentComment = false;
            AdminAdded = property.AdminAdded;

            UnreadMessageCount = 0;

            Photo = property.PropertyPhotos.Count > 0 ? property.PropertyPhotos.FirstOrDefault().Photo : string.Empty;

            AddedOn = property.AddedOn;

            DurationEN = PresentHelper.GetDurationEn(property.AddedOn);
            DurationAR = PresentHelper.GetDurationAr(property.AddedOn);

            PropertyComponents = property.PropertyComponents.Where(pc => pc.Count > 0).OrderBy(c => c.Component.Ordering).Select(pc => new App_PropertyComponent
            {
                PropertyComponentId = pc.Id,
                ComponentId = pc.ComponentId,
                ComponentEN = pc.Component.TitleEN,
                ComponentAR = pc.Component.TitleAR,
                Count = pc.Count ?? 0,
                Photo = pc.Component.Photo
            });

            PropertyFeatures = property.PropertyFeatures.Select(pf => new App_PropertyFeature
            {
                PropertyFeatureId = pf.Id,
                FeatureId = pf.FeatureId,
                FeatureEN = pf.Feature.TitleEN,
                FeatureAR = pf.Feature.TitleAR,
            });

            PropertyPhotos = property.PropertyPhotos.Select(pp => new App_PropertyPhoto
            {
                PropertyPhotoId = pp.Id,
                Path = AppSettings.WebsiteURL + AppSettings.PropertyWMPhotos,
                Photo = pp.Photo
            });

            PropertyVideos = property.PropertyVideos.Select(pv => new App_PropertyVideo
            {
                PropertyVideoId = pv.Id,
                Path = AppSettings.WebsiteURL + AppSettings.PropertyVideos,
                Video = pv.Video
            });

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    CountryId = ad.CountryId ?? 0,
                    CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty,
                    CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty,
                    Photo = ad.Photo,
                    Link = ad.Link
                });
        }

        public App_Property(Data.Property property, long userId)
        {
            Id = property.Id;
            PageURL = property.PageName;
            UserId = property.UserId;
            Name = !String.IsNullOrEmpty(property.User.FullName) ? property.User.FullName : string.Empty;
            Phone = property.User.Phone;
            UserPhoto = AppSettings.WebsiteURL + property.User.UserPhoto;

            PurposeId = property.PurposeId;
            PurposeEN = property.Purpos.TitleEN;
            PurposeAR = property.Purpos.TitleAR;

            TypeId = property.TypeId;
            TypeEN = property.PropertyType.TitleEN;
            TypeAR = property.PropertyType.TitleAR;

            CountryId = property.CountryId;
            CountryEN = property.Country.TitleEN;
            CountryAR = property.Country.TitleAR;
            CountryCode = property.Country.CCode;
            CurrencyEN = string.Empty;
            CurrencyAR = string.Empty;

            CityId = property.BlockId.HasValue ? property.Block.Area.CityId : 0;
            CityEN = property.BlockId.HasValue ? property.Block.Area.City.TitleEN : string.Empty;
            CityAR = property.BlockId.HasValue ? property.Block.Area.City.TitleAR : string.Empty;

            AreaId = property.BlockId.HasValue ? property.Block.AreaId : 0;
            AreaEN = property.BlockId.HasValue ? property.Block.Area.TitleEN : string.Empty;
            AreaAR = property.BlockId.HasValue ? property.Block.Area.TitleAR : string.Empty;

            BlockId = property.BlockId.HasValue ? property.BlockId.Value : 0;
            BlockEN = property.BlockId.HasValue ? property.Block.TitleEN : string.Empty;
            BlockAR = property.BlockId.HasValue ? property.Block.TitleAR : string.Empty;

            Area = property.Area.HasValue ? property.Area.Value : 0;
            BuildingAge = property.BuildingAge.HasValue ? property.BuildingAge.Value : 0;
            MonthlyIncome = property.MonthlyIncome.HasValue ? property.MonthlyIncome.Value : 0;
            SellingPrice = property.SellingPrice.HasValue ? property.SellingPrice.Value : 0;
            RentPrice = property.RentPrice.HasValue ? property.RentPrice.Value : 0;

            Details = !string.IsNullOrEmpty(property.Details) ? property.Details : string.Empty;

            OtherPhones = !string.IsNullOrEmpty(property.OtherPhones) ? property.OtherPhones :
                (!string.IsNullOrEmpty(property.User.OtherPhones) ? property.User.OtherPhones : property.User.Phone);

            Longitude = !string.IsNullOrEmpty(property.DLongitude.ToString()) ? property.DLongitude.ToString() : string.Empty;
            Latitude = !string.IsNullOrEmpty(property.DLatitude.ToString()) ? property.DLatitude.ToString() : string.Empty;

            AddressEN = property.AddressEN;
            AddressAR = property.AddressAR;

            Street = property.Street;
            PACI = property.PACI;

            ViewersCount = property.ViewersCount;
            FavoritesCount = property.PropertyFavorites.Count;

            IsSpecial = property.IsSpecial ? 1 : 0;
            IsFavorite = false;
            IsReport = false;
            IsViewed = false;
            IsSentComment = false;
            AdminAdded = property.AdminAdded;

            UnreadMessageCount = property.PropertyComments.Count(pc => !pc.IsRead && pc.ReceiverId == userId);

            Photo = property.PropertyPhotos.Count > 0 ? property.PropertyPhotos.FirstOrDefault().Photo : string.Empty;

            AddedOn = property.AddedOn;

            DurationEN = PresentHelper.GetDurationEn(property.AddedOn);
            DurationAR = PresentHelper.GetDurationAr(property.AddedOn);

            PropertyComponents = property.PropertyComponents.Where(pc => pc.Count > 0).Select(pc => new App_PropertyComponent
            {
                PropertyComponentId = pc.Id,
                ComponentId = pc.ComponentId,
                ComponentEN = pc.Component.TitleEN,
                ComponentAR = pc.Component.TitleAR,
                Count = pc.Count.HasValue ? pc.Count.Value : 0,
                Photo = pc.Component.Photo
            });

            PropertyFeatures = property.PropertyFeatures.Select(pf => new App_PropertyFeature
            {
                PropertyFeatureId = pf.Id,
                FeatureId = pf.FeatureId,
                FeatureEN = pf.Feature.TitleEN,
                FeatureAR = pf.Feature.TitleAR,
            });

            PropertyPhotos = property.PropertyPhotos.Select(pp => new App_PropertyPhoto
            {
                PropertyPhotoId = pp.Id,
                Path = AppSettings.WebsiteURL + AppSettings.PropertyWMPhotos,
                Photo = pp.Photo
            });

            PropertyVideos = property.PropertyVideos.Select(pv => new App_PropertyVideo
            {
                PropertyVideoId = pv.Id,
                Path = AppSettings.WebsiteURL + AppSettings.PropertyVideos,
                Video = pv.Video
            });

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
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

        public App_Property(CommercialAd adObj)
        {
            Id = -1;
            PageURL = string.Empty;
            UserId = 0;
            Name = adObj.Id.ToString();
            Phone = string.Empty;
            UserPhoto = string.Empty;

            PurposeId = 0;
            PurposeEN = string.Empty;
            PurposeAR = string.Empty;

            TypeId = 0;
            TypeEN = string.Empty;
            TypeAR = string.Empty;

            CountryId = adObj.CountryId.HasValue ? adObj.CountryId.Value : 0;
            CountryEN = adObj.CountryId.HasValue ? adObj.Country.TitleEN : string.Empty;
            CountryAR = adObj.CountryId.HasValue ? adObj.Country.TitleAR : string.Empty;
            CountryCode = adObj.CountryId.HasValue ? adObj.Country.CCode : string.Empty;
            CurrencyEN = string.Empty;
            CurrencyAR = string.Empty;

            CityId = 0;
            CityEN = string.Empty;
            CityAR = string.Empty;

            AreaId = 0;
            AreaEN = string.Empty;
            AreaAR = string.Empty;

            BlockId = 0;
            BlockEN = string.Empty;
            BlockAR = string.Empty;

            Area = 0;
            BuildingAge = 0;
            MonthlyIncome = 0;
            SellingPrice = 0;
            RentPrice = 0;

            Details = !string.IsNullOrEmpty(adObj.Photo) ? adObj.Link : string.Empty;

            OtherPhones = string.Empty;

            Longitude = string.Empty;
            Latitude = string.Empty;

            AddressEN = string.Empty;
            AddressAR = string.Empty;

            Street = string.Empty;
            PACI = string.Empty;

            ViewersCount = 0;
            FavoritesCount = 0;

            IsSpecial = 0;
            IsFavorite = false;
            IsReport = false;
            IsViewed = false;
            IsSentComment = false;
            AdminAdded = false;

            UnreadMessageCount = 0;

            Photo = !string.IsNullOrEmpty(adObj.Photo) ? string.Concat(AppSettings.CommAdPhotos + adObj.Photo) : string.Empty;

            AddedOn = adObj.AddedOn;

            DurationEN = string.Empty;
            DurationAR = string.Empty;

            PropertyComponents = new List<App_PropertyComponent>();

            PropertyFeatures = new List<App_PropertyFeature>();

            PropertyPhotos = new List<App_PropertyPhoto>();

            PropertyVideos = new List<App_PropertyVideo>();

            CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= DateTime.Now && ad.ToDate >= DateTime.Now
                && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
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
