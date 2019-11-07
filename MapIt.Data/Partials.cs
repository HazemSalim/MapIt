using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Device.Location;

namespace MapIt.Data
{
    public partial class Country
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String FullCode
        {
            get
            {
                return this.ISOCode + " " + this.CCode;
            }
        }

        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["CountryPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public GeoCoordinate DefCoord
        {
            get
            {
                return new GeoCoordinate(Math.Round(double.Parse(this.Latitude)), Math.Round(double.Parse(this.Longitude)));
            }
        }
    }

    public partial class Area
    {

    }

    public partial class CommercialAd
    {
        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["CommAdPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class Component
    {
        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["ComponentPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class Slider
    {
        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["SliderPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class Offer
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["OfferPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class Broker
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.FullName, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["BrokerPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class Package
    {
        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["PackagePhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }
    }

    public partial class ContentPage
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String FinalLink
        {
            get
            {
                return this.HasLink ? this.Link : ("../Page/" + this.PageName);
            }
        }
    }

    public partial class User
    {
        public String FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public String PhoneForSMS
        {
            get
            {
                return this.Phone.Substring(1).Replace(" ", "");
            }
        }

        public String PageName
        {
            get
            {
                return Regex.Replace(this.UserName, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String Key
        {
            get
            {
                return this.ActivationCode + "m" + this.Id + "t" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        public String UserPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["UserPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public String CountryFlag
        {
            get
            {
                return !string.IsNullOrEmpty(this.Country.Photo) ? ConfigurationManager.AppSettings["CountryPhotos"] + this.Country.Photo
                    : string.Empty;
            }
        }

        public Double UsedCredit
        {
            get
            {
                return this.UserBalanceLogs.Where(l => l.TransType.ToLower() == "debit").Sum(s => s.Amount);
            }
        }

        public Double AddedFreeCredit
        {
            get
            {
                return this.UserCredits.Where(l => l.PaymentMethodId == 1).Sum(s => s.Amount);
            }
        }

        public Double AddedActualCredit
        {
            get
            {
                return this.UserCredits.Where(l => l.PaymentMethodId > 1).Sum(s => s.Amount);
            }
        }

        public Double AvFreeCredit
        {
            get
            {
                return this.UsedCredit >= this.AddedFreeCredit ? 0 : (this.AddedFreeCredit - this.UsedCredit);
            }
        }

        public Double AvActualCredit
        {
            get
            {
                return this.UsedCredit >= this.AddedFreeCredit ?
                    this.AddedActualCredit - (this.UsedCredit - this.AddedFreeCredit)
                    : this.AddedActualCredit;
            }
        }

        public bool HasPendingMessages
        {
            get
            {
                return this.TechMessages.Any(tm => !tm.IsRead);
            }
        }
    }

    public partial class PropertyType
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public double GetPropertiesAvg(string portalAddress, int purposeId)
        {
            var propertiesList = this.Properties.Where(p => (p.PurposeId == purposeId) && ((!string.IsNullOrEmpty(portalAddress) ? p.PortalAddressEN.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true) ||
                            (!string.IsNullOrEmpty(portalAddress) ? p.PortalAddressAR.Replace(" ", string.Empty).ToLower().Contains(portalAddress.Replace(" ", string.Empty).ToLower()) : true)));

            if (purposeId == 1)
            {
                propertiesList = propertiesList.Where(p => p.SellingPrice.HasValue);
            }
            else if (purposeId == 2)
            {
                propertiesList = propertiesList.Where(p => p.RentPrice.HasValue);
            }

            if (propertiesList.Count() > 0)
            {
                if (purposeId == 1)
                {
                    return propertiesList.Average(p => p.SellingPrice).Value;
                }
                else if (purposeId == 2)
                {
                    return propertiesList.Average(p => p.RentPrice).Value;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    public partial class Property
    {
        public String TitleEN
        {
            get
            {
                return this.PropertyType.TitleEN + " " + this.Purpos.TitleEN;
            }
        }

        public String TitleAR
        {
            get
            {
                return this.PropertyType.TitleAR + " " + this.Purpos.TitleAR;
            }
        }

        public String AddressEN
        {
            get
            {
                if (this.BlockId.HasValue)
                {
                    string add = this.Block.Area.City.TitleEN + " " + this.Block.Area.TitleEN + " " + this.Block.TitleEN;

                    if (add.Length > 0)
                    {
                        int i = add.IndexOf(",") + 1;
                        return add.Substring(i).Replace("Governorate", "Gov");
                    }
                    return string.Empty;
                }
                else
                {
                    if (this.PortalAddressEN.Length > 0)
                    {
                        int i = this.PortalAddressEN.IndexOf(",") + 1;
                        return this.PortalAddressEN.Substring(i).Replace("Governorate", "Gov");
                    }
                    return string.Empty;
                }
            }
        }

        public String AddressAR
        {
            get
            {
                if (this.BlockId.HasValue)
                {
                    string add = this.Block.Area.City.TitleAR + " " + this.Block.Area.TitleAR + " " + this.Block.TitleAR;

                    if (add.Length > 0)
                    {
                        int i = add.IndexOf(",") + 1;
                        return add.Substring(i);
                    }
                    return string.Empty;
                }
                else
                {
                    if (this.PortalAddressAR.Length > 0)
                    {
                        int i = this.PortalAddressAR.IndexOf(",") + 1;
                        return this.PortalAddressAR.Substring(i);
                    }
                    return string.Empty;
                }
            }
        }
        public string Cordinates
        {
            get
            {
                return this.Latitude + "," + this.Longitude;
            }
        }
        public Double DLatitude
        {
            get
            {
                return double.Parse(this.Latitude);
            }
        }

        public Double DLongitude
        {
            get
            {
                return double.Parse(this.Longitude);
            }
        }

        public Double CoordDef
        {
            get
            {
                return this.DLongitude - this.DLatitude;
            }
        }

        public GeoCoordinate GeoCoord
        {
            get
            {
                return new GeoCoordinate(this.DLatitude, this.DLongitude);
            }
        }

        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public Country ProCountry
        {
            get
            {
                return this.Block.Area.City.Country;
            }
        }

        public String PropertyPhoto
        {
            get
            {
                return this.PropertyPhotos.Count > 0 ? ConfigurationManager.AppSettings["PropertyWMPhotos"] + this.PropertyPhotos.FirstOrDefault().Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public String PropertyVideo
        {
            get
            {
                return this.PropertyVideos.Count > 0 ? ConfigurationManager.AppSettings["PropertyVideos"] + this.PropertyVideos.FirstOrDefault().Video
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public Boolean IsValid
        {
            get
            {
                return true;
            }
        }

        public bool IsReported
        {
            get
            {
                return this.PropertyReports.Count > 0;
            }
        }


        public int ReportsCount
        {
            get
            {
                return this.PropertyReports.Count;
            }
        }

        public bool IsViewed
        {
            get
            {
                return this.PropertyViews.Count > 0;
            }
        }


        public int ViewsCount
        {
            get
            {
                return this.PropertyViews.Count;
            }
        }
    }

    public partial class UserCredit
    {
        public Boolean IsPaid
        {
            get
            {
                return this.PaymentStatus == 2 ? true : false;
            }
        }
    }

    public partial class ServicesCategory
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.TitleEN, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public String SubLink
        {
            get
            {
                return this.SubServicesCategories.Count > 0 ? "../SrvCat/" + this.PageName : "../Srvs/" + this.PageName;
            }
        }

        public Int32 ServicesCount
        {
            get
            {
                if (this.SubServicesCategories.Count > 0)
                {
                    return this.SubServicesCategories.Sum(c => c.Services.Where(p => p.IsActive).Count());
                }

                return this.Services.Where(p => p.IsActive).Count();
            }
        }

        public int CountProducts(int? countryId)
        {
            if (this.SubServicesCategories.Count > 0)
            {
                return this.SubServicesCategories.Sum(c => c.Services.Where(s => s.IsActive && s.ServicesCategory.IsActive && s.User.IsActive &&
                    ((countryId.HasValue && countryId != 0) ? s.User.CountryId == countryId.Value : true)).Count());
            }

            return this.Services.Where(s => s.IsActive && s.User.IsActive &&
                ((countryId.HasValue && countryId != 0) ? s.User.CountryId == countryId.Value : true)).Count();
        }

        public String FinalPhoto
        {
            get
            {
                return !String.IsNullOrEmpty(this.Photo) ? ConfigurationManager.AppSettings["ServiceCategoryPhotos"] + this.Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public ServicesCategory(MapIt.Data.CommercialAd adObj)
        {
            Id = -1;
            ParentId = 0;
            TitleEN = adObj.Id.ToString();
            TitleAR = adObj.Link;
            Photo = ConfigurationManager.AppSettings["WebsiteURL"] + adObj.FinalPhoto;
            Ordering = 0;
            IsActive = adObj.IsActive;
            AddedOn = adObj.AddedOn;
            ModifiedOn = adObj.ModifiedOn;
            AdminUserId = adObj.AdminUserId;
        }
    }

    public partial class Service
    {
        public String PageName
        {
            get
            {
                return Regex.Replace(this.Title, @"[^0-9a-zA-Z\u0600-\u06FF]+", "-") + "-" + this.Id;
            }
        }

        public Country ProCountry
        {
            get
            {
                return this.User.Country;
            }
        }

        public String ServicePhoto
        {
            get
            {
                return this.ServicePhotos.Count > 0 ? ConfigurationManager.AppSettings["ServiceWMPhotos"] + this.ServicePhotos.FirstOrDefault().Photo
                    : ConfigurationManager.AppSettings["NoImage"];
            }
        }

        public Int32 RateValue
        {
            get
            {
                return 3;
                //return Int32.Parse(Math.Ceiling(decimal.Parse(this.UserRates.Average(x => x.Rate).ToString())).ToString());
            }
        }

        public bool IsReported
        {
            get
            {
                return this.ServiceReports.Count > 0;
            }
        }

        public int ReportsCount
        {
            get
            {
                return this.ServiceReports.Count;
            }
        }

        public bool IsViewed
        {
            get
            {
                return this.ServiceViews.Count > 0;
            }
        }


        public int ViewsCount
        {
            get
            {
                return this.ServiceViews.Count;
            }
        }
    }
}
