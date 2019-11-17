using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using MapIt.Web.App.App_Model;
using System.Threading;
using System.Device.Location;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace MapIt.Web.App
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://mapitre.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class App2 : WebService
    {
        #region Variables

        GeneralSettingsRepository gSettingsRepository;
        CurrenciesRepository currenciesRepository;
        PaymentMethodsRepository paymentMethodsRepository;
        PackagesRepository packagesRepository;
        CountriesRepository countriesRepository;
        CitiesRepository citiesRepository;
        AreasRepository areasRepository;
        BlocksRepository blocksRepository;
        CommercialAdsRepository commercialAdsRepository;
        SlidersRepository slidersRepository;
        OffersRepository offersRepository;
        BrokersRepository brokersRepository;
        ContentPagesRepository cPagesRepository;
        FAQsRepository faqsRepository;
        ComponentsRepository componentsRepository;
        FeaturesRepository featuresRepository;
        ReasonsRepository reasonsRepository;
        PropertyTypesRepository propertyTypesRepository;
        PurposesRepository purposesRepository;
        PropertiesRepository propertiesRepository;
        PropertyComponentsRepository propertyComponentsRepository;
        PropertyFeaturesRepository propertyFeaturesRepository;
        PropertyPhotosRepository propertyPhotosRepository;
        PropertyVideosRepository propertyVideosRepository;
        PropertySettingsRepository propertySettingsRepository;
        PropertyViewsRepository propertyViewsRepository;
        PropertyFavoritesRepository propertyFavoritesRepository;
        ServicesCategoriesRepository servicesCategoriesRepository;
        ServicesRepository servicesRepository;
        ServiceViewsRepository serviceViewsRepository;
        ServiceFavoritesRepository serviceFavoritesRepository;
        ServiceAreasRepository serviceAreasRepository;
        ServicePhotosRepository servicePhotosRepository;
        UsersRepository usersRepository;
        PropertyCommentsRepository propertyCommentsRepository;
        ServiceCommentsRepository serviceCommentsRepository;
        DevicesTokensRepository devicesTokensRepository;
        UserCreditsRepository userCreditsRepository;
        MerchantsRepository merchantsRepository;
        PhotographersRepository photographersRepository;
        TechMessagesRepository techMessagesRepository;
        NotificationsRepository notificationsRepository;
        WatchListsRepository watchListsRepository;
        UserBalanceLogsRepository userBalanceLogsRepository;
        UserTypesRepository userTypesRepository;
        Random random = new Random();

        #endregion

        #region Utilities

        DateTime todayNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

        public GeneralSetting GSetting
        {
            get
            {
                gSettingsRepository = new GeneralSettingsRepository();
                var gSettingObj = gSettingsRepository.Get();
                return gSettingObj;
            }
        }

        public GeoCoordinate CurCoord
        {
            get
            {
                countriesRepository = new CountriesRepository();
                var countryObj = countriesRepository.GetByKey(1);
                return countryObj.DefCoord;
            }
        }

        public int ListAdPlace
        {
            get
            {
                return 6;
            }
        }

        void RenderAsJson(object obj)
        {
            try
            {
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string strResponse = ser.Serialize(obj);

                //string strResponse = JsonConvert.SerializeObject(obj);

                //Context.Response.Clear();
                //Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", strResponse.Length.ToString());

                //Context.Response.Write(strResponse);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", strResponse.Length.ToString());
                strResponse = strResponse.Replace("null", @"""""");

                HttpContext.Current.Response.Write(strResponse);


            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                HttpContext.Current.Response.ContentType = "application/json";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(""));
            }
        }

        bool IsValueType(object value)
        {
            return value is string
                || value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }


        IQueryable<User> pushUsersAlertList;
        int pushTypeId;
        long pushPropertyId;
        string pushMessageEN;
        string pushMessageAR;

        void DoWork()
        {
            foreach (var item in pushUsersAlertList)
            {
                AppPushs.Push(pushTypeId, item.Id, null, pushPropertyId, null, null, pushMessageEN, pushMessageAR);
            }
        }

        #endregion

        #region Bind Users List

        [WebMethod]
        public string[] BindUsers(string prefix)
        {
            List<string> users = new List<string>();

            usersRepository = new UsersRepository();
            var usersList = usersRepository.Find(u => (!string.IsNullOrEmpty(prefix) ? u.Phone.Trim().ToLower().IndexOf(prefix.Trim().ToLower()) > -1 : true)).Take(25);

            if (usersList != null && usersList.Count() > 0)
            {
                foreach (var item in usersList)
                {
                    users.Add(string.Format("{0}-{1}", item.Phone, item.Id));
                }
            }

            return users.ToArray();
        }

        #endregion

        #region General Setting

        [WebMethod(Description = "Get all general settings.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSettings(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                gSettingsRepository = new GeneralSettingsRepository();
                List<App_GeneralSettings> list = new List<App_GeneralSettings>();
                var gSettingsObj = gSettingsRepository.Get();
                if (gSettingsObj != null)
                {
                    App_GeneralSettings gSettings = new App_GeneralSettings(gSettingsObj);
                    list.Add(gSettings);

                    gSettingsRepository = null;
                    gSettingsObj = null;

                    RenderAsJson(list);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region LookUps

        [WebMethod(Description = "Get all payment methods.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPaymentMethods(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                paymentMethodsRepository = new PaymentMethodsRepository();
                List<App_PaymentMethod> list = new List<App_PaymentMethod>();
                var paymentMethods = paymentMethodsRepository.Find(p => p.IsActive);
                foreach (var obj in paymentMethods)
                {
                    list.Add(new App_PaymentMethod(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all users packages.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUsersPackages(int currencyId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                currenciesRepository = new CurrenciesRepository();
                packagesRepository = new PackagesRepository();
                var currency = GSetting.DefaultCurrency;

                if (currencyId > 0)
                {
                    currency = currenciesRepository.GetByKey(currencyId);
                }

                List<App_Package> list = new List<App_Package>();
                var packages = packagesRepository.Find(p => p.IsActive);

                foreach (var obj in packages)
                {
                    list.Add(new App_Package(obj, currency));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all currencies.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCurrencies(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                currenciesRepository = new CurrenciesRepository();
                List<App_Currency> list = new List<App_Currency>();
                var currencies = currenciesRepository.Find(c => c.IsActive).ToList();
                foreach (var obj in currencies)
                {
                    list.Add(new App_Currency(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all countries.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCountries(string appLang, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                countriesRepository = new CountriesRepository();
                List<App_Country> list = new List<App_Country>();
                var countries = countriesRepository.Find(c => c.IsActive).OrderBy(c => appLang.ToLower() == "ar" ? c.TitleAR : c.TitleEN).ToList();
                foreach (var obj in countries)
                {
                    list.Add(new App_Country(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set countryId with 0 to get all cities.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCities(int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                citiesRepository = new CitiesRepository();
                List<App_City> list = new List<App_City>();
                var cities = citiesRepository.GetAll();
                if (countryId > 0)
                {
                    cities = cities.Where(c => c.CountryId == countryId);
                }

                foreach (var obj in cities)
                {
                    list.Add(new App_City(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set cityId with 0 to get all areas.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAreas(int cityId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                areasRepository = new AreasRepository();
                List<App_Area> list = new List<App_Area>();
                var areas = areasRepository.GetAll();
                if (cityId > 0)
                {
                    areas = areas.Where(c => c.CityId == cityId);
                }

                foreach (var obj in areas)
                {
                    list.Add(new App_Area(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set countryId with 0 to get all areas.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetAreasByCountry(int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                areasRepository = new AreasRepository();
                List<App_Area> list = new List<App_Area>();
                var areas = areasRepository.GetAll();
                if (countryId > 0)
                {
                    areas = areas.Where(c => c.City.CountryId == countryId);
                }

                foreach (var obj in areas)
                {
                    list.Add(new App_Area(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set prefix with 'char' to get match areas.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AutoSearchAreas(string prefix, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                areasRepository = new AreasRepository();
                List<App_Area> list = new List<App_Area>();
                var areas = areasRepository.Find(a => (!string.IsNullOrEmpty(prefix) ? (a.TitleEN.Trim().ToLower().IndexOf(prefix.Trim().ToLower()) > -1) ||
                    (a.TitleEN.Trim().ToLower().IndexOf(prefix.Trim().ToLower()) > -1) : true));

                foreach (var obj in areas)
                {
                    list.Add(new App_Area(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set areaId with 0 to get all blocks.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetBlocks(int areaId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                blocksRepository = new BlocksRepository();
                List<App_Block> list = new List<App_Block>();
                var blocks = blocksRepository.GetAll();
                if (areaId > 0)
                {
                    blocks = blocks.Where(c => c.AreaId == areaId);
                }

                foreach (var obj in blocks)
                {
                    list.Add(new App_Block(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set countryId with 0 to get all cities areas.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCitiesAreas(int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                areasRepository = new AreasRepository();
                var list = areasRepository.GetAll().Select(a => new App_Area
                {
                    Id = a.Id,
                    TitleAR = a.City.TitleAR + " - " + a.TitleAR,
                    TitleEN = a.City.TitleEN + " - " + a.TitleEN,
                    CityId = a.CityId,
                    CityEN = a.City.TitleEN,
                    CityAR = a.City.TitleAR,
                    CountryId = a.City.CountryId,
                    CountryEN = a.City.Country.TitleEN,
                    CountryAR = a.City.Country.TitleAR
                }).ToList();

                if (countryId > 0)
                {
                    list = list.Where(c => c.CountryId == countryId).ToList();
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set countryId with 0 to get all cities areas blocks.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCitiesAreasBlocks(int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                blocksRepository = new BlocksRepository();
                var list = blocksRepository.GetAll().Select(b => new App_Block
                {
                    Id = b.Id,
                    TitleEN = b.TitleEN,
                    TitleAR = b.TitleAR,
                    FullTitleEN = b.Area.City.TitleEN + "-" + b.Area.TitleEN + "-" + b.TitleEN,
                    FullTitleAR = b.Area.City.TitleAR + "-" + b.Area.TitleAR + "-" + b.TitleAR,
                    Coordinates = b.Coordinates,
                    AreaId = b.AreaId,
                    AreaEN = b.Area.TitleEN,
                    AreaAR = b.Area.TitleAR,
                    CityId = b.Area.CityId,
                    CityEN = b.Area.City.TitleEN,
                    CityAR = b.Area.City.TitleAR,
                    CountryId = b.Area.City.CountryId,
                    CountryEN = b.Area.City.Country.TitleEN,
                    CountryAR = b.Area.City.Country.TitleAR
                }).ToList();

                if (countryId > 0)
                {
                    list = list.Where(c => c.CountryId == countryId).ToList();
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all property types.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertyTypes(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyTypesRepository = new PropertyTypesRepository();
                List<App_PropertyType> list = new List<App_PropertyType>();
                var types = propertyTypesRepository.GetAll().OrderBy(pt => pt.Ordering);
                foreach (var obj in types)
                {
                    list.Add(new App_PropertyType(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all components.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetComponents(int typeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                componentsRepository = new ComponentsRepository();
                List<App_Component> list = new List<App_Component>();
                var components = componentsRepository.GetBasic(typeId, "");

                //Components
                foreach (var obj in components)
                {
                    list.Add(new App_Component(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all features.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFeatures(int typeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                featuresRepository = new FeaturesRepository();
                List<App_Feature> list = new List<App_Feature>();
                var features = featuresRepository.GetBasic(typeId, "");

                //Features
                foreach (var obj in features)
                {
                    list.Add(new App_Feature(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all purposes.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPurposes(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                purposesRepository = new PurposesRepository();
                List<App_Purpos> list = new List<App_Purpos>();
                var purpos = purposesRepository.GetAll();
                foreach (var obj in purpos)
                {
                    list.Add(new App_Purpos(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all reasons.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReasons(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                reasonsRepository = new ReasonsRepository();
                List<App_Reason> list = new List<App_Reason>();
                var reasons = reasonsRepository.GetAll();
                foreach (var obj in reasons)
                {
                    list.Add(new App_Reason(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all User Types.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserTypes(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                userTypesRepository = new UserTypesRepository();
                List<App_UserType> list = new List<App_UserType>();
                var types = userTypesRepository.GetAll();
                foreach (var obj in types)
                {
                    list.Add(new App_UserType(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Slider, Offers, Brokers, Content Pages and FAQs

        [WebMethod(Description = "Set sliderId with 0 to get all sliders.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSliders(int sliderId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                slidersRepository = new SlidersRepository();
                List<App_Slider> list = new List<App_Slider>();
                var sliders = slidersRepository.Find(s => s.IsActive);
                if (sliderId > 0)
                {
                    sliders = sliders.Where(s => s.Id == sliderId);
                }
                if (pageIndex > -1)
                {
                    sliders = sliders.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in sliders)
                {
                    list.Add(new App_Slider(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set offerId with 0 to get all offers.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetOffers(int offerId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                offersRepository = new OffersRepository();
                List<App_Offer> list = new List<App_Offer>();

                var offers = offersRepository.Find(s => s.IsActive);

                if (offerId > 0)
                {
                    offersRepository.IncreaseViewersCount(offerId);
                    offers = offers.Where(s => s.Id == offerId);
                }

                offers = offers.OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => Guid.NewGuid());

                //if (pageIndex > -1)
                //{
                //    offers = offers.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                //}

                foreach (var obj in offers)
                {
                    list.Add(new App_Offer(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set brokerId with 0 to get all brokers.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetBrokers(int brokerId, int countryId, int cityId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                brokersRepository = new BrokersRepository();
                List<App_Broker> list = new List<App_Broker>();
                var brokers = brokersRepository.Find(s => s.IsActive);
                if (brokerId > 0)
                {
                    brokers = brokers.Where(s => s.Id == brokerId);
                }

                if (countryId > 0)
                {
                    brokers = brokers.Where(a => a.City.CountryId == countryId);
                }

                if (cityId > 0)
                {
                    brokers = brokers.Where(a => a.CityId == cityId);
                }

                if (pageIndex > -1)
                {
                    brokers = brokers.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in brokers)
                {
                    list.Add(new App_Broker(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set pageId with 0 to get all pages.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCPages(int pageId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                cPagesRepository = new ContentPagesRepository();
                List<App_ContentPage> list = new List<App_ContentPage>();

                var pages = cPagesRepository.Find(p => p.IsActive && p.PageShows.Any((cp => cp.PagePlaceId == (int)AppEnums.PagePlaces.MobApp && cp.Show)) || pageId > 0);

                if (pageId > 0)
                {
                    pages = pages.Where(s => s.Id == pageId);
                }

                foreach (var obj in pages)
                {
                    list.Add(new App_ContentPage(obj, pageId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set faqId with 0 to get all faqs.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFAQs(int faqId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                faqsRepository = new FAQsRepository();
                List<App_FAQ> list = new List<App_FAQ>();

                var faqs = faqsRepository.Find(p => p.IsActive);

                if (faqId > 0)
                {
                    faqs = faqs.Where(s => s.Id == faqId);
                }

                foreach (var obj in faqs)
                {
                    list.Add(new App_FAQ(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get Comm. Ad by placeId and countryId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCommAd(int placeId, int countryId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                commercialAdsRepository = new CommercialAdsRepository();
                List<App_CommAd> list = new List<App_CommAd>();
                var commAds = commercialAdsRepository.Find(a => a.IsActive);

                if (placeId > 0)
                {
                    commAds = commAds.Where(a => a.CommAdPlaceId == placeId);
                }

                if (countryId > 0)
                {
                    commAds = commAds.Where(a => !a.CountryId.HasValue || a.CountryId == countryId);
                }

                commAds = commAds.OrderBy(item => Guid.NewGuid());

                if (pageIndex > -1)
                {
                    commAds = commAds.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in commAds)
                {
                    list.Add(new App_CommAd(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Properties

        [WebMethod(Description = "Upload Photos URL -> http://'website'/App/AppPUF.aspx <br /> Upload Videos URL -> http://'website'/App/AppVUF.aspx <br />Number greater than 0 (propertyId) -> Success <br />-2 -> Required field missing <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddProperty(long userId, int purposeId, int typeId, int countryId, int blockId, double area, int buildingAge, double monthlyIncome, double sellingPrice,
            double rentPrice, string details, string otherPhones, string longitude, string latitude, string street, string portalAddressEN, string portalAddressAR,
            string paci, string components, string comCounts, string features, string photos, string videos, int special, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 || purposeId < 1 || typeId < 1 || countryId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                var componentList = new List<PropertyComponent>();
                if (!string.IsNullOrEmpty(components) || !string.IsNullOrEmpty(comCounts))
                {
                    string[] componentsArr = components.Split(',');
                    string[] comCountsArr = comCounts.Split(',');
                    for (int i = 0; i < componentsArr.Length; i++)
                    {
                        int? componentId = ParseHelper.GetInt(componentsArr[i]);
                        int? comCount = ParseHelper.GetInt(comCountsArr[i]);

                        if (componentId.HasValue && comCount.HasValue)
                        {
                            componentList.Add(new PropertyComponent
                            {
                                ComponentId = componentId.Value,
                                Count = comCount.Value
                            });
                        }
                    }
                }

                var featuresList = new List<PropertyFeature>();
                if (!string.IsNullOrEmpty(features))
                {
                    string[] featuresArr = features.Split(',');
                    foreach (string ftr in featuresArr)
                    {
                        if (!string.IsNullOrEmpty(ftr) && ftr.Trim() != string.Empty)
                        {
                            int? frtId = ParseHelper.GetInt(ftr);

                            if (frtId.HasValue)
                            {
                                featuresList.Add(new PropertyFeature { FeatureId = frtId.Value });
                            }
                        }
                    }
                }

                var photosList = new List<PropertyPhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new PropertyPhoto { Photo = img });
                    }
                }

                var videosList = new List<PropertyVideo>();
                if (!string.IsNullOrEmpty(videos))
                {
                    string[] videosArr = videos.Split(',');
                    foreach (string video in videosArr)
                    {
                        if (!string.IsNullOrEmpty(video) && video.Trim() != string.Empty)
                            videosList.Add(new PropertyVideo { Video = video });
                    }
                }

                int? bId = null;
                if (blockId > 0)
                {
                    bId = blockId;
                }

                propertiesRepository = new PropertiesRepository();
                var propertyObj = new Data.Property
                {
                    PurposeId = purposeId,
                    TypeId = typeId,
                    UserId = userId,
                    CountryId = countryId,
                    BlockId = bId
                };

                if (area > 0)
                {
                    propertyObj.Area = area;
                }
                if (buildingAge > 0)
                {
                    propertyObj.BuildingAge = buildingAge;
                }
                if (monthlyIncome > 0)
                {
                    propertyObj.MonthlyIncome = monthlyIncome;
                }
                if (sellingPrice > 0)
                {
                    propertyObj.SellingPrice = sellingPrice;
                }
                if (rentPrice > 0)
                {
                    propertyObj.RentPrice = rentPrice;
                }

                propertyObj.Details = details;
                propertyObj.OtherPhones = otherPhones;
                propertyObj.Longitude = longitude;
                propertyObj.Latitude = latitude;
                propertyObj.Street = street;
                propertyObj.PortalAddressEN = portalAddressEN;
                propertyObj.PortalAddressAR = portalAddressAR;
                propertyObj.PACI = paci;

                propertyObj.ViewersCount = 0;
                propertyObj.PaySpecial = propertyObj.IsSpecial = special == 1;
                propertyObj.PayVideo = !string.IsNullOrEmpty(videos);
                propertyObj.IsAvailable = true;
                propertyObj.IsActive = GSetting.AutoActiveAd;
                propertyObj.AdminAdded = false;
                propertyObj.AddedOn = DateTime.Now;

                componentList.ForEach(propertyObj.PropertyComponents.Add);
                featuresList.ForEach(propertyObj.PropertyFeatures.Add);
                photosList.ForEach(propertyObj.PropertyPhotos.Add);
                videosList.ForEach(propertyObj.PropertyVideos.Add);
                propertiesRepository.Add(propertyObj);

                // calculate the ad cost
                double cost = 0;

                if (propertyObj.PaySpecial)
                {
                    cost = cost + GSetting.SpecAdCost;
                }
                else
                {
                    cost = cost + GSetting.NormalAdCost;
                }

                if (propertyObj.PayVideo)
                {
                    cost = cost + GSetting.AdVideoCost;
                }

                // balance deduction
                if (cost > 0)
                {
                    userBalanceLogsRepository = new UserBalanceLogsRepository();

                    var userBalanceObj = new UserBalanceLog();
                    userBalanceObj.LogNo = string.Empty;
                    userBalanceObj.UserId = userId;
                    userBalanceObj.Amount = cost;
                    userBalanceObj.TransType = AppConstants.BalanceTransTypes.Debit;
                    userBalanceObj.TransOn = DateTime.Now;

                    userBalanceLogsRepository.Add(userBalanceObj);
                    userBalanceObj.LogNo = "LOG" + (userBalanceObj.Id).ToString("D6");
                    userBalanceLogsRepository.Update(userBalanceObj);
                }

                // send push
                propertiesRepository = new PropertiesRepository();
                propertyObj = propertiesRepository.GetByKey(propertyObj.Id);

                watchListsRepository = new WatchListsRepository();
                pushUsersAlertList = watchListsRepository.GetMatchUsers(propertyObj.TypeId, propertyObj.CountryId, propertyObj.BlockId.HasValue ? propertyObj.Block.Area.CityId : 0,
                    propertyObj.BlockId.HasValue ? propertyObj.Block.AreaId : 0, propertyObj.PurposeId).Where(u => u.Id != propertyObj.UserId);

                pushTypeId = (int)AppEnums.NotifTypes.Alert;
                pushPropertyId = propertyObj.Id;
                pushMessageEN = "New property in " + propertyObj.AddressEN;
                pushMessageAR = "عقار جديد في " + propertyObj.AddressAR;

                Thread th = new Thread(DoWork);
                th.Start();

                RenderAsJson(propertyObj.Id);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Upload Photos URL -> http://'website'/App/AppPUF.aspx <br />Upload Videos URL -> http://'website'/App/AppVUF.aspx <br />1 -> Success <br />-2 -> Required field missing <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EditProperty(long proId, long userId, int purposeId, int typeId, int countryId, int blockId, double area, int buildingAge, double monthlyIncome, double sellingPrice,
            double rentPrice, string details, string otherPhones, string longitude, string latitude, string street, string portalAddressEN, string portalAddressAR,
            string paci, string components, string comCounts, string features, string photos, string videos, int special, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 || typeId < 1 || countryId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                var componentList = new List<PropertyComponent>();
                if (!string.IsNullOrEmpty(components) || !string.IsNullOrEmpty(comCounts))
                {
                    string[] componentsArr = components.Split(',');
                    string[] comCountsArr = comCounts.Split(',');
                    for (int i = 0; i < componentsArr.Length; i++)
                    {
                        int? componentId = ParseHelper.GetInt(componentsArr[i]);
                        int? comCount = ParseHelper.GetInt(comCountsArr[i]);

                        if (componentId.HasValue && comCount.HasValue)
                        {
                            componentList.Add(new PropertyComponent
                            {
                                ComponentId = componentId.Value,
                                Count = comCount.Value
                            });
                        }
                    }
                }

                var featuresList = new List<PropertyFeature>();
                if (!string.IsNullOrEmpty(features))
                {
                    string[] featuresArr = features.Split(',');
                    foreach (string ftr in featuresArr)
                    {
                        if (!string.IsNullOrEmpty(ftr) && ftr.Trim() != string.Empty)
                        {
                            int? frtId = ParseHelper.GetInt(ftr);

                            if (frtId.HasValue)
                            {
                                featuresList.Add(new PropertyFeature { FeatureId = frtId.Value });
                            }
                        }
                    }
                }

                var photosList = new List<PropertyPhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new PropertyPhoto { Photo = img });
                    }
                }

                var videosList = new List<PropertyVideo>();
                if (!string.IsNullOrEmpty(videos))
                {
                    string[] videosArr = videos.Split(',');
                    foreach (string video in videosArr)
                    {
                        if (!string.IsNullOrEmpty(video) && video.Trim() != string.Empty)
                            videosList.Add(new PropertyVideo { Video = video });
                    }
                }

                int? bId = null;
                if (blockId > 0)
                {
                    bId = blockId;
                }

                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(proId);
                if (propertyObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                propertyObj.TypeId = typeId;
                propertyObj.UserId = userId;
                propertyObj.CountryId = countryId;
                propertyObj.BlockId = bId;

                if (area > 0)
                {
                    propertyObj.Area = area;
                }
                if (buildingAge > 0)
                {
                    propertyObj.BuildingAge = buildingAge;
                }
                if (monthlyIncome > 0)
                {
                    propertyObj.MonthlyIncome = monthlyIncome;
                }
                if (sellingPrice > 0)
                {
                    propertyObj.SellingPrice = sellingPrice;
                }
                if (rentPrice > 0)
                {
                    propertyObj.RentPrice = rentPrice;
                }

                propertyObj.Details = details;
                propertyObj.OtherPhones = otherPhones;
                propertyObj.Longitude = longitude;
                propertyObj.Latitude = latitude;
                propertyObj.Street = street;
                propertyObj.PortalAddressEN = portalAddressEN;
                propertyObj.PortalAddressAR = portalAddressAR;
                propertyObj.PACI = paci;

                if (propertyObj.PaySpecial)
                {
                    propertyObj.IsSpecial = special == 1;
                }

                propertyObj.PayVideo = !string.IsNullOrEmpty(videos);
                propertyObj.IsAvailable = true;
                propertyObj.IsActive = GSetting.AutoActiveAd;
                propertyObj.ModifiedOn = DateTime.Now;

                propertyComponentsRepository = new PropertyComponentsRepository();
                propertyComponentsRepository.Delete(pc => pc.PropertyId == propertyObj.Id);
                componentList.ForEach(propertyObj.PropertyComponents.Add);

                propertyFeaturesRepository = new PropertyFeaturesRepository();
                propertyFeaturesRepository.Delete(pf => pf.PropertyId == propertyObj.Id);
                featuresList.ForEach(propertyObj.PropertyFeatures.Add);

                propertyPhotosRepository = new PropertyPhotosRepository();
                propertyPhotosRepository.Delete(pp => pp.PropertyId == propertyObj.Id);
                photosList.ForEach(propertyObj.PropertyPhotos.Add);

                propertyVideosRepository = new PropertyVideosRepository();
                propertyVideosRepository.Delete(pv => pv.PropertyId == propertyObj.Id);
                videosList.ForEach(propertyObj.PropertyVideos.Add);

                propertiesRepository.Update(propertyObj);

                // calculate the ad cost
                double cost = 0;

                if (!propertyObj.PaySpecial)
                {
                    cost = cost + GSetting.SpecAdCost;
                    propertyObj.PaySpecial = true;
                }
                else
                {
                    cost = cost + GSetting.NormalAdCost;
                }

                if (!propertyObj.PayVideo)
                {
                    cost = cost + GSetting.AdVideoCost;
                    propertyObj.PayVideo = true;
                }

                propertiesRepository.Update(propertyObj);

                // balance deduction
                if (cost > 0)
                {
                    userBalanceLogsRepository = new UserBalanceLogsRepository();

                    var userBalanceObj = new UserBalanceLog();
                    userBalanceObj.LogNo = string.Empty;
                    userBalanceObj.UserId = userId;
                    userBalanceObj.Amount = cost;
                    userBalanceObj.TransType = AppConstants.BalanceTransTypes.Debit;
                    userBalanceObj.TransOn = DateTime.Now;

                    userBalanceLogsRepository.Add(userBalanceObj);
                    userBalanceObj.LogNo = "LOG" + (userBalanceObj.Id).ToString("D6");
                    userBalanceLogsRepository.Update(userBalanceObj);
                }

                RenderAsJson(propertyObj.Id);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get property settings.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertySettings(int typeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertySettingsRepository = new PropertySettingsRepository();

                var list = propertySettingsRepository.Find(s => s.TypeId == typeId && s.IsAvailable).ToList();

                if (list != null)
                {
                    App_PropertySetting appPropertySetting = new App_PropertySetting
                    {
                        Country = "1,1",
                        City = "1,1",
                        CArea = "1,1",
                        Block = "1,1",

                        Area = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Area") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Area").IsMondatory ? "1,1" : "1,0") : "0,0",
                        BuildingAge = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "BuildingAge") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "BuildingAge").IsMondatory ? "1,1" : "1,0") : "0,0",
                        MonthlyIncome = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "MonthlyIncome") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "MonthlyIncome").IsMondatory ? "1,1" : "1,0") : "0,0",
                        SellingPrice = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "SellingPrice") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "SellingPrice").IsMondatory ? "1,1" : "1,0") : "0,0",
                        RentPrice = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "RentPrice") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "RentPrice").IsMondatory ? "1,1" : "1,0") : "0,0",
                        Details = list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Details") != null ?
                        (list.FirstOrDefault(s => s.Category == "Main" && s.PropertyName == "Details").IsMondatory ? "1,1" : "1,0") : "0,0",

                        Component = list.FirstOrDefault(s => s.Category == "Component") != null ? (list.FirstOrDefault(s => s.Category == "Component").IsMondatory ? "1,1" : "1,0") : "0,0",
                        Feature = list.FirstOrDefault(s => s.Category == "Feature") != null ? (list.FirstOrDefault(s => s.Category == "Feature").IsMondatory ? "1,1" : "1,0") : "0,0"
                    };

                    RenderAsJson(new List<App_PropertySetting> { appPropertySetting });
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = @"Get all properties. <br />Sort option 1: Added Descending - Sort option 2: Added Ascending - 
                    Sort option 3: Price Descending - Sort option 4: Price Ascending. <br /> Set today with value '1' to get today's deals")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProperties(long propertyId, long userId, int purposeId, int typeId, int countryId, int cityId, int areaId, int blockId,
            string portalAddress, double areaFrom, double areaTo, int yearFrom, int yearTo, double mIncomeFrom, double mIncomeTo, double sPriceFrom,
            double sPriceTo, double rPriceFrom, double rPriceTo, int today, int special, int sortOption, int pageIndex, long loginUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertiesRepository = new PropertiesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                if (propertyId > 0)
                {
                    propertiesRepository.IncreaseViewersCount(propertyId);

                    if (loginUserId > 0)
                    {
                        propertiesRepository.SetViewed(propertyId, loginUserId);
                    }
                }

                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (today == 1)
                {
                    dateFrom = DateTime.Now.AddHours(-24);
                    dateTo = DateTime.Now;
                }

                int? _special = null;

                if (special > 0)
                {
                    _special = special;
                }

                var properties = propertiesRepository.Search(propertyId, userId, purposeId, typeId, countryId, cityId, areaId, blockId, null, portalAddress,
                    null, areaFrom, areaTo, yearFrom, yearTo, mIncomeFrom, mIncomeTo, sPriceFrom, sPriceTo, rPriceFrom, rPriceTo, dateFrom, dateTo, _special,
                    1, 1, 0, null, 1, null);

                properties = properties.OrderBy(l => l.Id);

                if (pageIndex > -1)
                {
                    properties = properties.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.UserId == loginUserId).Select(uf => uf.PropertyId).ToList();
                }

                switch (sortOption)
                {
                    case 0:
                        properties = properties.OrderByDescending(i => i.AddedOn);
                        break;
                    case 1:
                        properties = properties.OrderByDescending(i => i.AddedOn);
                        break;
                    case 2:
                        properties = properties.OrderBy(i => i.AddedOn);
                        break;
                    case 3:
                        properties = properties.OrderByDescending(i => i.SellingPrice.HasValue ? i.SellingPrice.Value : i.RentPrice.Value);
                        break;
                    case 4:
                        properties = properties.OrderBy(i => i.SellingPrice.HasValue ? i.SellingPrice.Value : i.RentPrice.Value);
                        break;
                    default:
                        properties = properties.OrderByDescending(i => i.AddedOn);
                        break;
                }

                // ad item ...
                commercialAdsRepository = new CommercialAdsRepository();
                List<App_Property> adsList = new List<App_Property>();
                App_Property appProAd;

                var ads = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                    && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(item => Guid.NewGuid()).ToList();

                if (ads.Count() == 1)
                {
                    CommercialAd adsObj = ads.FirstOrDefault();
                    if (adsObj != null)
                    {
                        ads.Add(adsObj);
                    }
                }

                int ii = 0;
                int adIndex = 0;
                int curIndex = 0;

                foreach (var property in properties)
                {
                    appProperty = new App_Property(property)
                    {
                        Details = propertyId < 1 ? string.Empty : property.Details,
                        IsFavorite = loginFavIds.Contains(property.Id),
                        IsReport = property.PropertyReports.Any(pr => pr.UserId == loginUserId) ? true : false,
                        IsViewed = property.PropertyViews.Any(pr => pr.UserId == loginUserId) ? true : false,
                        IsSentComment = property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == loginUserId) ? true : false
                    };
                    //list.Add(appProperty);

                    if (propertyId > 0)
                    {
                        list.Add(appProperty);
                    }
                    else
                    {

                        if (ii == 0 && ads.Count > 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else if (ads.Count > 0 && ii > 0 && ii % ListAdPlace == 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else
                        {
                            list.Add(appProperty);
                        }
                        ii++;
                    }
                }

                RenderAsJson(list);

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = @"Get all properties. <br />Sort option 1: Added Descending - Sort option 2: Added Ascending - 
                    Sort option 3: Price Descending - Sort option 4: Price Ascending. <br /> Set today with value '1' to get today's deals")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetProperties2(long propertyId, long userId, int purposeId, int typeId, int countryId, int cityId, int areaId, int blockId,
          string portalAddress, double areaFrom, double areaTo, int yearFrom, int yearTo, double mIncomeFrom, double mIncomeTo, double sPriceFrom,
          double sPriceTo, double rPriceFrom, double rPriceTo, int today, int special, int sortOption, int pageIndex, long loginUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertiesRepository = new PropertiesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                if (propertyId > 0)
                {
                    propertiesRepository.IncreaseViewersCount(propertyId);

                    if (loginUserId > 0)
                    {
                        propertiesRepository.SetViewed(propertyId, loginUserId);
                    }
                }

                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (today == 1)
                {
                    dateFrom = DateTime.Now.AddHours(-24);
                    dateTo = DateTime.Now;
                }

                int? _special = null;

                if (special > 0)
                {
                    _special = special;
                }

                var properties = propertiesRepository.Search(propertyId, userId, purposeId, typeId, countryId, cityId, areaId, blockId, null, portalAddress,
                    null, areaFrom, areaTo, yearFrom, yearTo, mIncomeFrom, mIncomeTo, sPriceFrom, sPriceTo, rPriceFrom, rPriceTo, dateFrom, dateTo, _special,
                    1, 1, 0, null, 1, null).ToList();

                // order by the nearest
                //properties = properties.OrderBy(x => x.GeoCoord.GetDistanceTo(CurCoord)).ToList();
                //properties = properties.OrderBy(x => x.GeoCoord.GetDistanceTo(x.GeoCoord)).ToList();

                //var fProperty = properties.OrderBy(x => x.Latitude).FirstOrDefault();
                //properties = properties.OrderBy(x => x.GeoCoord.GetDistanceTo(fProperty.GeoCoord)).ToList();

                properties = properties.OrderBy(x => x.CoordDef).ToList();

                if (pageIndex > -1)
                {
                    properties = properties.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob).ToList();
                }

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.UserId == loginUserId).Select(uf => uf.PropertyId).ToList();
                }

                //switch (sortOption)
                //{
                //    case 0:
                //        properties = properties.OrderByDescending(i => i.AddedOn);
                //        break;
                //    case 1:
                //        properties = properties.OrderByDescending(i => i.AddedOn);
                //        break;
                //    case 2:
                //        properties = properties.OrderBy(i => i.AddedOn);
                //        break;
                //    case 3:
                //        properties = properties.OrderByDescending(i => i.SellingPrice.HasValue ? i.SellingPrice.Value : i.RentPrice.Value);
                //        break;
                //    case 4:
                //        properties = properties.OrderBy(i => i.SellingPrice.HasValue ? i.SellingPrice.Value : i.RentPrice.Value);
                //        break;
                //    default:
                //        properties = properties.OrderByDescending(i => i.AddedOn);
                //        break;
                //}

                // ad item ...
                commercialAdsRepository = new CommercialAdsRepository();
                List<App_Property> adsList = new List<App_Property>();
                App_Property appProAd;

                var ads = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                    && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(item => Guid.NewGuid()).ToList();

                if (ads.Count() == 1)
                {
                    CommercialAd adsObj = ads.FirstOrDefault();
                    if (adsObj != null)
                    {
                        ads.Add(adsObj);
                    }
                }

                int ii = 0;
                int adIndex = 0;
                int curIndex = 0;

                foreach (var property in properties)
                {
                    appProperty = new App_Property(property);
                    appProperty.Details = propertyId < 1 ? string.Empty : property.Details;
                    appProperty.IsFavorite = loginFavIds.Contains(property.Id);
                    appProperty.IsReport = property.PropertyReports.Any(pr => pr.UserId == loginUserId) ? true : false;
                    appProperty.IsViewed = property.PropertyViews.Any(pr => pr.UserId == loginUserId) ? true : false;
                    appProperty.IsSentComment = property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == loginUserId) ? true : false;
                    //list.Add(appProperty);

                    if (propertyId > 0)
                    {
                        list.Add(appProperty);
                    }
                    else
                    {

                        if (ii == 0 && ads.Count > 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else if (ads.Count > 0 && ii > 0 && ii % ListAdPlace == 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else
                        {
                            list.Add(appProperty);
                        }
                        ii++;
                    }
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }


        [WebMethod(Description = @"Get all properties. <br />Sort option 1: Added Descending - Sort option 2: Added Ascending - 
                    Sort option 3: Price Descending - Sort option 4: Price Ascending. <br /> Set today with value '1' to get today's deals")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public object GetPropertiesMap(long propertyId, long userId, int purposeId, string typeId, int countryId, int cityId, int areaId, int blockId,
           string portalAddress, double areaFrom, double areaTo, int yearFrom, int yearTo, double mIncomeFrom, double mIncomeTo, double sPriceFrom,
           double sPriceTo, double rPriceFrom, double rPriceTo, int today, int special, int sortOption, int pageIndex, long loginUserId,
           string userTypeID, double minLatitude, double minLongitude, double maxLatitude, double maxLongitude, double centerLatitude, 
           double centerLongitude, string key)
        {
            int pageSize = 50;
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return null;
                }

                propertiesRepository = new PropertiesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                if (propertyId > 0)
                {
                    propertiesRepository.IncreaseViewersCount(propertyId);

                    if (loginUserId > 0)
                    {
                        propertiesRepository.SetViewed(propertyId, loginUserId);
                    }
                }

                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (today == 1)
                {
                    dateFrom = DateTime.Now.AddHours(-24);
                    dateTo = DateTime.Now;
                }

                int? _special = null;

                if (special > 0)
                {
                    _special = special;
                }

                var properties = propertiesRepository.Search(propertyId, userId, purposeId, 0, countryId, cityId, areaId, blockId, null, portalAddress,
                    null, areaFrom, areaTo, yearFrom, yearTo, mIncomeFrom, mIncomeTo, sPriceFrom, sPriceTo, rPriceFrom, rPriceTo, dateFrom, dateTo, _special,
                    1, 1, 0, null, 1, null, 0, typeId,userTypeID).ToList();


                if (centerLatitude != 0 && centerLongitude != 0)
                {
                    var sCoordCenter = new GeoCoordinate(centerLatitude, centerLongitude);
                    properties = properties.Where(p => minLatitude > 0 && maxLatitude > 0 ? p.DLatitude >= minLatitude && p.DLatitude <= maxLatitude && p.DLongitude >= minLongitude && p.DLongitude <= maxLongitude : true)
                        //.OrderBy(x => x.GeoCoord.GetDistanceTo(sCoordCenter)).ToList();
                        .OrderByDescending(x => x.Id).ToList();//
                }
                else
                {
                    properties = properties.OrderBy(x => x.CoordDef).ToList();
                }

                int allPropertiesCount = properties.Count;
                if (pageIndex > -1)
                {
                    properties = properties.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.UserId == loginUserId).Select(uf => uf.PropertyId).ToList();
                }


                commercialAdsRepository = new CommercialAdsRepository();
                List<App_Property> adsList = new List<App_Property>();
                App_Property appProAd;

                var ads = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                    && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.PropertiesList).OrderBy(item => Guid.NewGuid()).ToList();

                if (ads.Count() == 1)
                {
                    CommercialAd adsObj = ads.FirstOrDefault();
                    if (adsObj != null)
                    {
                        ads.Add(adsObj);
                    }
                }

                int ii = 0;
                int adIndex = 0;
                int curIndex = 0;

                foreach (var property in properties)
                {
                    appProperty = new App_Property(property)
                    {
                        Details = propertyId < 1 ? string.Empty : property.Details,
                        IsFavorite = loginFavIds.Contains(property.Id),
                        IsReport = property.PropertyReports.Any(pr => pr.UserId == loginUserId) ? true : false,
                        IsViewed = property.PropertyViews.Any(pr => pr.UserId == loginUserId) ? true : false,
                        IsSentComment = property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == loginUserId) ? true : false
                    };

                    if (propertyId > 0)
                    {
                        list.Add(appProperty);
                    }
                    else
                    {

                        if (ii == 0 && ads.Count > 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else if (ads.Count > 0 && ii > 0 && ii % ListAdPlace == 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appProAd = new App_Property(ads[curIndex]);
                            list.Add(appProAd);
                            list.Add(appProperty);
                        }
                        else
                        {
                            list.Add(appProperty);
                        }
                        ii++;
                    }
                }


                //if (centerLatitude != 0 && centerLongitude != 0)
                //{
                return new { Data = list, PageSize = pageSize, Count = allPropertiesCount };

                //}
                //else
                //{
                //   return list;
                //}

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
            }
        }

        [WebMethod(Description = "Get all properties Count.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int GetPropertiesCount(long propertyId, long userId, int purposeId, int typeId, int countryId, int cityId, int areaId, int blockId,
            string portalAddress, double areaFrom, double areaTo, int yearFrom, int yearTo, double mIncomeFrom, double mIncomeTo, double sPriceFrom,
            double sPriceTo, double rPriceFrom, double rPriceTo, int today, int special, int sortOption, int pageIndex, long loginUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                if (propertyId > 0)
                {
                    propertiesRepository.IncreaseViewersCount(propertyId);

                    if (loginUserId > 0)
                    {
                        propertiesRepository.SetViewed(propertyId, loginUserId);
                    }
                }

                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (today == 1)
                {
                    dateFrom = DateTime.Now.AddHours(-24);
                    dateTo = DateTime.Now;
                }

                int? _special = null;

                if (special > 0)
                {
                    _special = special;
                }

                int propertiesCount = propertiesRepository.Search(propertyId, userId, purposeId, typeId, countryId, cityId, areaId, blockId, null, portalAddress,
                    null, areaFrom, areaTo, yearFrom, yearTo, mIncomeFrom, mIncomeTo, sPriceFrom, sPriceTo, rPriceFrom, rPriceTo, dateFrom, dateTo, _special,
                    1, 1, 0, null, 1, null).Count();
                 
                //RenderAsJson(propertiesCount);
                return propertiesCount;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Get similar properties.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSimilarProperties(long propertyId, long loginUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertiesRepository = new PropertiesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var propertyObj = propertiesRepository.GetByKey(propertyId);
                if (propertyObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }
                List<MapIt.Data.Property> properties = new List<MapIt.Data.Property>();

                properties = propertiesRepository.Search(0, 0, 0, propertyObj.TypeId, 0, 0, propertyObj.BlockId.HasValue ? propertyObj.Block.AreaId : 0, 0,
                    string.Empty, string.Empty, string.Empty, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, 0, 1, 1, 0, null, 1, string.Empty)
                    .Where(i => i.Id != propertyId).OrderByDescending(i => new { i.PurposeId, i.TypeId, i.AddedOn }).Take(GSetting.SimilarAdCount).ToList();

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.UserId == loginUserId).Select(uf => uf.PropertyId).ToList();
                }

                foreach (var property in properties)
                {
                    appProperty = new App_Property(property);
                    appProperty.IsFavorite = loginFavIds.Contains(property.Id);
                    appProperty.IsReport = property.PropertyReports.Any(pr => pr.UserId == loginUserId) ? true : false;
                    appProperty.IsViewed = property.PropertyViews.Any(pr => pr.UserId == loginUserId) ? true : false;
                    appProperty.IsSentComment = property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == loginUserId) ? true : false;
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all viewed properties by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetViewedProperties(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyViewsRepository = new PropertyViewsRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var viewedProperties = propertyViewsRepository.Find(uv => uv.Property.IsActive && uv.UserId == userId).OrderByDescending(l => l.Id).AsQueryable();

                if (pageIndex > -1)
                {
                    viewedProperties = viewedProperties.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize);
                }

                foreach (var view in viewedProperties)
                {
                    appProperty = new App_Property(view.Property);
                    appProperty.IsFavorite = view.Property.PropertyFavorites.Any(pf => pf.UserId == userId) ? true : false;
                    appProperty.IsReport = view.Property.PropertyReports.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsViewed = view.Property.PropertyViews.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsSentComment = view.Property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == userId) ? true : false;
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all viewed properties by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetViewedProperties2(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyViewsRepository = new PropertyViewsRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var viewedProperties = propertyViewsRepository.Find(uv => uv.Property.IsActive && uv.UserId == userId).ToList();

                // order by the nearest
                //viewedProperties = viewedProperties.OrderBy(x => x.Property.GeoCoord.GetDistanceTo(CurCoord)).ToList();
                //viewedProperties = viewedProperties.OrderBy(x => x.Property.GeoCoord.GetDistanceTo(x.Property.GeoCoord)).ToList();
                viewedProperties = viewedProperties.OrderBy(x => x.Property.CoordDef).ThenBy(x => x.Property.Longitude).ToList();

                if (pageIndex > -1)
                {
                    viewedProperties = viewedProperties.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize).ToList();
                }

                foreach (var view in viewedProperties)
                {
                    appProperty = new App_Property(view.Property);
                    appProperty.IsFavorite = view.Property.PropertyFavorites.Any(pf => pf.UserId == userId) ? true : false;
                    appProperty.IsReport = view.Property.PropertyReports.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsViewed = view.Property.PropertyViews.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsSentComment = view.Property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == userId) ? true : false;
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all favorite properties by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFavoriteProperties(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyFavoritesRepository = new PropertyFavoritesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var favProperties = propertyFavoritesRepository.Find(uf => uf.Property.IsActive && uf.UserId == userId).OrderByDescending(l => l.Id).AsQueryable();

                if (pageIndex > -1)
                {
                    favProperties = favProperties.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize);
                }

                foreach (var fav in favProperties)
                {
                    appProperty = new App_Property(fav.Property);
                    appProperty.IsFavorite = true;
                    appProperty.IsReport = fav.Property.PropertyReports.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsViewed = fav.Property.PropertyViews.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsSentComment = fav.Property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == userId) ? true : false;
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all favorite properties by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFavoriteProperties2(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyFavoritesRepository = new PropertyFavoritesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var favProperties = propertyFavoritesRepository.Find(uf => uf.Property.IsActive && uf.UserId == userId).ToList();

                // order by the nearest
                //favProperties = favProperties.OrderBy(x => x.Property.GeoCoord.GetDistanceTo(CurCoord)).ToList();
                //favProperties = favProperties.OrderBy(x => x.Property.GeoCoord.GetDistanceTo(x.Property.GeoCoord)).ToList();
                favProperties = favProperties.OrderBy(x => x.Property.CoordDef).ThenBy(x => x.Property.Longitude).ToList();

                if (pageIndex > -1)
                {
                    favProperties = favProperties.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize).ToList();
                }

                foreach (var fav in favProperties)
                {
                    appProperty = new App_Property(fav.Property);
                    appProperty.IsFavorite = true;
                    appProperty.IsReport = fav.Property.PropertyReports.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsViewed = fav.Property.PropertyViews.Any(pr => pr.UserId == userId) ? true : false;
                    appProperty.IsSentComment = fav.Property.User.ReceiverPropertyComments.Any(pc => pc.SenderId == userId) ? true : false;
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all properties photos by propertyId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertyPhotos(long propertyId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyPhotosRepository = new PropertyPhotosRepository();
                var list = propertyPhotosRepository.GetByPropertyId(propertyId).Select(pp => new App_PropertyPhoto
                {
                    PropertyPhotoId = pp.Id,
                    Path = AppSettings.WebsiteURL + AppSettings.PropertyPhotos,
                    Photo = pp.Photo
                });
                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all properties videos by propertyId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertyVideos(long propertyId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyVideosRepository = new PropertyVideosRepository();
                var list = propertyVideosRepository.GetByPropertyId(propertyId).Select(pv => new App_PropertyVideo
                {
                    PropertyVideoId = pv.Id,
                    Path = AppSettings.WebsiteURL + AppSettings.PropertyVideos,
                    Video = pv.Video
                });
                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get market graph.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetMarketGraph(string portalAddress, int purposeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyTypesRepository = new PropertyTypesRepository();
                List<App_MarketGraph> list = new List<App_MarketGraph>();
                var types = propertyTypesRepository.GetAll().OrderBy(pt => pt.Ordering);
                foreach (var obj in types)
                {
                    list.Add(new App_MarketGraph(obj, portalAddress, purposeId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Set property is favorite by propertyId and userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MakePropertyFavorite(long propertyId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertiesRepository = new PropertiesRepository();
                int result = propertiesRepository.SetFavorite(propertyId, userId);
                RenderAsJson(result + "-" + propertiesRepository.Entities.PropertyFavorites.Where(uf => uf.PropertyId == propertyId).Count());
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }


        [WebMethod(Description = "Set property is viewed by propertyId and userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long MakePropertyViewed(long propertyId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                int result = propertiesRepository.SetViewed(propertyId, userId);
                //RenderAsJson(result + "-" + propertiesRepository.Entities.PropertyViews.Where(uf => uf.PropertyId == propertyId).Count());
                return propertyId;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set property shares by propertyId and type.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long IncreasePropertyShares(long propertyId, string type, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                bool result = propertiesRepository.IncreasePropertyShares(propertyId, type);

                if (!result)
                    return -2;

                return propertyId;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set property is reported by propertyId, userId , notes and reasonId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long MakePropertyReported2(long propertyId, long userId, int reasonId, string key, string notes)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                long result = propertiesRepository.SetReport(propertyId, userId, reasonId, notes);
                AppMails.SendNewReportToAdmin(result, true);
                //RenderAsJson(result);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set property is reported by propertyId, userId and reasonId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MakePropertyReported(long propertyId, long userId, int reasonId, string key)
        {
            MakePropertyReported2(propertyId, userId, reasonId, key, "");
        }

        [WebMethod(Description = "1 -> Republish property Success <br />-2 -> Property not exist<br />-3 -> This property does not belong to this user<br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int RefreshProperty(long propertyId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(propertyId);

                if (propertyObj != null)
                {
                    if (propertyObj.UserId != userId)
                    {
                        //RenderAsJson(-3);
                        return -3;
                    }

                    propertyObj.AddedOn = DateTime.Now;
                    propertiesRepository.Update(propertyObj);

                    //RenderAsJson(1);
                    return 1;
                }
                else
                {
                    //RenderAsJson(-2);
                    return -2;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set property is stop by propertyId and userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int StopProperty(long propertyId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(propertyId);

                if (propertyObj != null)
                {
                    if (propertyObj.UserId != userId)
                    {
                        //RenderAsJson(-3);
                        return -3;
                    }

                    if (propertyObj.IsAvailable)
                    {
                        propertyObj.IsAvailable = false;
                        propertiesRepository.Update(propertyObj);

                        //RenderAsJson(1);
                        return 1;
                    }
                    else
                    {
                        propertyObj.IsAvailable = true;
                        propertiesRepository.Update(propertyObj);

                        //RenderAsJson(2);
                        return 2;
                    }
                }
                else
                {
                    //RenderAsJson(-2);
                    return -2;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "1 -> Delete property Success <br />-2 -> Property not exist<br />-3 -> This property does not belong to this user<br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int DeleteProperty(long propertyId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                propertiesRepository = new PropertiesRepository();
                var propertyObj = propertiesRepository.GetByKey(propertyId);

                if (propertyObj != null)
                {
                    if (propertyObj.UserId != userId)
                    {
                        //RenderAsJson(-3);
                        return -3;
                    }

                    propertiesRepository.DeleteAnyWay(propertyId);
                    //RenderAsJson(1);
                    return 1;
                }
                else
                {
                    //RenderAsJson(-2);
                    return -2;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        #endregion

        #region Categories

        [WebMethod(Description = "Get all services categories")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetServicesCategories(int categoryId, string appLang, string key)
        {

            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesCategoriesRepository = new ServicesCategoriesRepository();
                List<ServicesCategory> categories;

                if (categoryId > 0)
                {
                    categories = servicesCategoriesRepository.ServicesCategories;
                }
                else
                {
                    categories = servicesCategoriesRepository.MainServicesCategories;
                }

                categories = categories.OrderBy(c => c.Ordering).ToList();

                // ad item ...
                commercialAdsRepository = new CommercialAdsRepository();
                List<ServicesCategory> categoriesList = new List<ServicesCategory>();
                ServicesCategory srvCatAd;

                var ads = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                    && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesCategories).OrderBy(item => Guid.NewGuid()).ToList();

                if (ads.Count() == 1)
                {
                    CommercialAd adsObj = ads.FirstOrDefault();
                    if (adsObj != null)
                    {
                        ads.Add(adsObj);
                    }
                }

                int ii = 0;
                int adIndex = 0;
                int curIndex = 0;

                foreach (var obj in categories)
                {
                    if (ii == 0 && ads.Count > 0)
                    {
                        adIndex = random.Next(ads.Count());
                        while (curIndex == adIndex)
                        {
                            adIndex = random.Next(ads.Count());
                        }
                        curIndex = adIndex;

                        srvCatAd = new ServicesCategory(ads[curIndex]);
                        categoriesList.Add(srvCatAd);
                        categoriesList.Add(obj);
                    }
                    else if (ads.Count > 0 && ii > 0 && ii % ListAdPlace == 0)
                    {
                        adIndex = random.Next(ads.Count());
                        while (curIndex == adIndex)
                        {
                            adIndex = random.Next(ads.Count());
                        }
                        curIndex = adIndex;

                        srvCatAd = new ServicesCategory(ads[curIndex]);
                        categoriesList.Add(srvCatAd);
                        categoriesList.Add(obj);
                    }
                    else
                    {
                        categoriesList.Add(obj);
                    }
                    ii++;
                }

                var list = categoriesList.Where(c => c.IsActive && (categoryId > 0 ? c.ParentId == categoryId : true)).Select(c => new
                {
                    c.Id,
                    c.TitleEN,
                    c.TitleAR,
                    Photo = c.Id == -1 ? c.Photo : (string.IsNullOrEmpty(c.Photo) ? AppSettings.WebsiteURL + AppSettings.NoImage : AppSettings.WebsiteURL + AppSettings.ServiceCategoryPhotos + c.Photo),
                    ServicesCount = c.Id == -1 ? 0 : c.ServicesCount,
                    SubCategories = c.Id == -1 ? new List<App_ServiceCategory>() : (c.SubServicesCategories.Where(sc => sc.IsActive).OrderBy(sc => appLang.ToLower() == "ar" ? sc.TitleAR : sc.TitleEN).Select(sc => new App_ServiceCategory
                    {
                        Id = sc.Id,
                        TitleEN = sc.TitleEN,
                        TitleAR = sc.TitleAR,
                        Photo = sc.Id == -1 ? sc.Photo : (String.IsNullOrEmpty(c.Photo) ? AppSettings.WebsiteURL + AppSettings.NoImage : AppSettings.WebsiteURL + AppSettings.ServiceCategoryPhotos + c.Photo),
                        ServicesCount = sc.Id == -1 ? 0 : sc.ServicesCount
                    })),
                    CommAds = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                        && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesCategories).OrderBy(r => Guid.NewGuid()).Take(5).Select(ad => new App_CommAd
                        {
                            Id = ad.Id,
                            Title = ad.Title,
                            CountryId = ad.CountryId.HasValue ? ad.CountryId.Value : 0,
                            CountryEN = ad.CountryId.HasValue ? ad.Country.TitleEN : string.Empty,
                            CountryAR = ad.CountryId.HasValue ? ad.Country.TitleAR : string.Empty,
                            Photo = ad.Photo,
                            Link = ad.Link
                        })
                });

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Services

        [WebMethod(Description = "Upload Photos URL -> http://'website'/App/AppSPUF.aspx <br />Number greater than 0 (serviceId) -> Success <br />-2 -> Required field missing <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddService(long userId, int cityId, int type, int categoryId, int exYears, string title, string description,
            string otherPhones, string longitude, string latitude, string areas, bool allAreas, string photos, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 || cityId < 1 || type < 1 || categoryId < 1 || exYears < 1 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                   string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(latitude))
                {
                    RenderAsJson(-2);
                    return;
                }

                var photosList = new List<ServicePhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new ServicePhoto { Photo = img });
                    }
                }

                var areasList = new List<ServiceArea>();
                if (!string.IsNullOrEmpty(areas))
                {
                    string[] areasArr = areas.Split(',');
                    foreach (string are in areasArr)
                    {
                        if (!string.IsNullOrEmpty(are) && are.Trim() != string.Empty)
                        {
                            int? areaId = ParseHelper.GetInt(are);

                            if (areaId.HasValue)
                            {
                                areasList.Add(new ServiceArea { AreaId = areaId.Value });
                            }
                        }
                    }
                }

                servicesRepository = new ServicesRepository();
                var serviceObj = new MapIt.Data.Service();

                serviceObj.Title = title;
                serviceObj.Description = description;
                serviceObj.UserId = userId;
                serviceObj.CityId = cityId;
                serviceObj.CategoryId = categoryId;
                serviceObj.ExYears = exYears;
                serviceObj.OtherPhones = otherPhones;
                serviceObj.Longitude = longitude;
                serviceObj.Latitude = latitude;
                serviceObj.ViewersCount = 0;
                serviceObj.AllAreas = allAreas;
                serviceObj.IsCompany = type == 1 ? false : true;
                serviceObj.IsActive = GSetting.AutoActiveAd;
                serviceObj.AdminAdded = false;
                serviceObj.AddedOn = DateTime.Now;

                photosList.ForEach(serviceObj.ServicePhotos.Add);
                areasList.ForEach(serviceObj.ServiceAreas.Add);
                servicesRepository.Add(serviceObj);

                RenderAsJson(serviceObj.Id);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Upload Photos URL -> http://'website'/App/AppSPUF.aspx <br />1 -> Success <br />-2 -> Required field missing <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EditService(long serviceId, long userId, int type, int categoryId, int exYears, string title, string description,
            string otherPhones, string longitude, string latitude, string areas, bool allAreas, string photos, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (serviceId < 1 || userId < 1 || type < 1 || categoryId < 1 || exYears < 1 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) ||
                   string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(latitude))
                {
                    RenderAsJson(-2);
                    return;
                }

                var photosList = new List<ServicePhoto>();
                if (!string.IsNullOrEmpty(photos))
                {
                    string[] imagesArr = photos.Split(',');
                    foreach (string img in imagesArr)
                    {
                        if (!string.IsNullOrEmpty(img) && img.Trim() != string.Empty)
                            photosList.Add(new ServicePhoto { Photo = img });
                    }
                }

                var areasList = new List<ServiceArea>();
                if (!string.IsNullOrEmpty(areas))
                {
                    string[] areasArr = areas.Split(',');
                    foreach (string are in areasArr)
                    {
                        if (!string.IsNullOrEmpty(are) && are.Trim() != string.Empty)
                        {
                            int? areaId = ParseHelper.GetInt(are);

                            if (areaId.HasValue)
                            {
                                areasList.Add(new ServiceArea { AreaId = areaId.Value });
                            }
                        }
                    }
                }

                servicesRepository = new ServicesRepository();
                var serviceObj = servicesRepository.GetByKey(serviceId);
                if (serviceObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                serviceObj.Title = title;
                serviceObj.Description = description;
                serviceObj.UserId = userId;
                serviceObj.CategoryId = categoryId;
                serviceObj.ExYears = exYears;
                serviceObj.OtherPhones = otherPhones;
                serviceObj.Longitude = longitude;
                serviceObj.Latitude = latitude;
                serviceObj.ViewersCount = 0;
                serviceObj.AllAreas = allAreas;
                serviceObj.IsCompany = type == 1 ? false : true;
                serviceObj.IsActive = GSetting.AutoActiveAd;
                serviceObj.ModifiedOn = DateTime.Now;

                servicePhotosRepository = new ServicePhotosRepository();
                servicePhotosRepository.Delete(sp => sp.ServiceId == serviceObj.Id);
                photosList.ForEach(serviceObj.ServicePhotos.Add);

                serviceAreasRepository = new ServiceAreasRepository();
                serviceAreasRepository.Delete(sa => sa.ServiceId == serviceObj.Id);
                areasList.ForEach(serviceObj.ServiceAreas.Add);

                servicesRepository.Update(serviceObj);

                RenderAsJson(serviceObj.Id);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get similar services.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSimilarServices(long serviceId, long loginUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                List<App_Service> list = new List<App_Service>();
                App_Service appService;

                var serviceObj = servicesRepository.GetByKey(serviceId);
                if (serviceObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                List<MapIt.Data.Service> servicesQuery = new List<MapIt.Data.Service>();

                servicesQuery = servicesRepository.GetAvServices().Where(s => s.Id != serviceId && s.CategoryId == serviceObj.CategoryId && s.CityId == serviceObj.CityId)
                    .OrderByDescending(i => new { i.CategoryId, i.AddedOn }).Take(GSetting.SimilarAdCount).ToList();

                var services = servicesQuery;

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = servicesRepository.Entities.ServiceFavorites.Where(sf => sf.UserId == loginUserId).Select(sf => sf.ServiceId).ToList();
                }

                foreach (var service in services)
                {
                    appService = new App_Service(service);
                    appService.IsFavorite = loginFavIds.Contains(service.Id);
                    appService.IsReport = service.ServiceReports.Any(sr => sr.UserId == loginUserId) ? true : false;
                    appService.IsViewed = service.ServiceViews.Any(sr => sr.UserId == loginUserId) ? true : false;
                    appService.IsSentComment = service.User.ReceiverServiceComments.Any(sc => sc.SenderId == loginUserId) ? true : false;
                    list.Add(appService);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all services by categoryId and loginUserId.<br /> - Type 1 is individual, type 2 is company.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetServices(int serviceId, int categoryId, int userId, int countryId, int cityId, int exYearFrom, int exYearTo,
            int rateFrom, int rateTo, int type, int pageIndex, int loginUserId, string keyword, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                usersRepository = new UsersRepository();
                servicesRepository = new ServicesRepository();
                gSettingsRepository = new GeneralSettingsRepository();
                servicesCategoriesRepository = new ServicesCategoriesRepository();

                if (serviceId > 0)
                {
                    servicesRepository.IncreaseViewersCount(serviceId);

                    if (loginUserId > 0)
                    {
                        servicesRepository.SetViewed(serviceId, loginUserId);
                    }
                }

                List<App_Service> list = new List<App_Service>();
                App_Service appService;

                var services = servicesRepository.Find(s => s.IsActive && s.User.IsActive && !s.User.IsCanceled).AsEnumerable();

                if (serviceId > 0)
                {
                    services = services.Where(s => s.Id == serviceId);
                }

                if (categoryId > 0)
                {
                    services = services.Where(s => s.CategoryId == categoryId);
                }

                if (userId > 0)
                {
                    services = services.Where(s => s.UserId == userId);
                }

                if (countryId > 0)
                {
                    services = services.Where(s => s.City.CountryId == countryId);
                }

                if (cityId > 0)
                {
                    services = services.Where(s => s.CityId == cityId);
                }

                if (exYearFrom > 0)
                {
                    services = services.Where(s => s.ExYears >= exYearFrom);
                }

                if (exYearTo > 0)
                {
                    services = services.Where(s => s.ExYears <= exYearTo);
                }

                if (rateFrom > 0)
                {
                    services = services.Where(s => s.RateValue >= rateFrom);
                }

                if (rateTo > 0)
                {
                    services = services.Where(s => s.RateValue <= rateTo);
                }

                if (type == 1)
                {
                    services = services.Where(s => !s.IsCompany);
                }
                else if (type == 2)
                {
                    services = services.Where(s => s.IsCompany);
                }

                if (!string.IsNullOrEmpty(keyword) && keyword.Trim() != string.Empty)
                {
                    List<int> CatsIdlst2 = new List<int>();
                    List<int> CatsIdlst3 = new List<int>();

                    var categoryList = servicesCategoriesRepository.Single(c => c.IsActive  && (c.TitleAR == keyword || c.TitleEN == keyword));
                    if (categoryList != null)
                    {
                        var properties2 = services.Where(l => l.CategoryId == categoryList.Id);

                        List<ServicesCategory> category2 = servicesCategoriesRepository.Find(c => c.IsActive  && c.ParentId.HasValue && c.ParentId > 0 && c.ParentId == categoryList.Id).ToList();
                        if (category2 != null && category2.Count() > 0)
                        {
                            foreach (var item in category2)
                            {
                                CatsIdlst2.Add(item.Id);
                                List<ServicesCategory> category3 = servicesCategoriesRepository.Find(c => c.IsActive && c.ParentId.HasValue && c.ParentId > 0 && c.ParentId == item.Id).ToList();
                                if (category3 != null && category3.Count() > 0)
                                {
                                    foreach (var item3 in category3)
                                    {
                                        CatsIdlst3.Add(item3.Id);
                                    }
                                }
                            }
                        }
                    }

                    var services3 = services.Where(c => CatsIdlst2.Contains(c.CategoryId));
                    var services4 = services.Where(c => CatsIdlst3.Contains(c.CategoryId));

                    services = services.Where(l => (l.Title.IndexOf(keyword.Trim()) > -1 || l.Description.IndexOf(keyword.Trim()) > -1
                        || l.ServicesCategory.TitleEN.IndexOf(keyword.Trim()) > -1 || l.ServicesCategory.TitleAR.IndexOf(keyword.Trim()) > -1));

                    services = services.Union(services3).Union(services4);
                }

                services = services.OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => c.AddedOn);


                if (pageIndex > -1)
                {
                    services = services.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                List<long> loginFavIds = new List<long>();
                if (loginUserId > 0)
                {
                    loginFavIds = servicesRepository.Entities.ServiceFavorites.Where(uf => uf.UserId == loginUserId).Select(uf => uf.ServiceId).ToList();
                }

                // ad item ...
                commercialAdsRepository = new CommercialAdsRepository();
                List<App_Service> adsList = new List<App_Service>();
                App_Service appSrvAd;

                var ads = commercialAdsRepository.Find(ad => ad.IsActive && ad.FromDate <= todayNow && ad.ToDate >= todayNow
                    && ad.CommAdPlaceId == (int)AppEnums.AdPlaces.ServicesList).OrderBy(item => Guid.NewGuid()).ToList();

                if (ads.Count() == 1)
                {
                    CommercialAd adsObj = ads.FirstOrDefault();
                    if (adsObj != null)
                    {
                        ads.Add(adsObj);
                    }
                }

                int ii = 0;
                int adIndex = 0;
                int curIndex = 0;

                foreach (var obj in services)
                {
                    appService = new App_Service(obj);
                    appService.IsFavorite = loginFavIds.Contains(obj.Id);
                    appService.IsReport = obj.ServiceReports.Any(sr => sr.UserId == loginUserId) ? true : false;
                    appService.IsViewed = obj.ServiceViews.Any(sr => sr.UserId == loginUserId) ? true : false;
                    appService.IsSentComment = obj.User.ReceiverServiceComments.Any(sc => sc.SenderId == loginUserId) ? true : false;
                    //list.Add(appService);

                    if (serviceId > 0)
                    {
                        list.Add(appService);
                    }
                    else
                    {
                        if (ii == 0 && ads.Count > 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appSrvAd = new App_Service(ads[curIndex]);
                            list.Add(appSrvAd);
                            list.Add(appService);
                        }
                        else if (ads.Count > 0 && ii > 0 && ii % ListAdPlace == 0)
                        {
                            adIndex = random.Next(ads.Count());
                            while (curIndex == adIndex)
                            {
                                adIndex = random.Next(ads.Count());
                            }
                            curIndex = adIndex;

                            appSrvAd = new App_Service(ads[curIndex]);
                            list.Add(appSrvAd);
                            list.Add(appService);
                        }
                        else
                        {
                            list.Add(appService);
                        }
                        ii++;
                    }
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Get all viewed services by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetViewedServices(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceViewsRepository = new ServiceViewsRepository();
                List<App_Service> list = new List<App_Service>();
                App_Service appService;

                var viewedServices = serviceViewsRepository.Find(uv => uv.Service.IsActive && uv.UserId == userId).OrderByDescending(l => l.Id).AsQueryable();

                if (pageIndex > -1)
                {
                    viewedServices = viewedServices.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize);
                }

                foreach (var fav in viewedServices)
                {
                    appService = new App_Service(fav.Service);
                    appService.IsFavorite = fav.Service.ServiceFavorites.Any(sf => sf.UserId == userId) ? true : false;
                    appService.IsReport = fav.Service.ServiceReports.Any(sr => sr.UserId == userId) ? true : false;
                    appService.IsViewed = fav.Service.ServiceViews.Any(sr => sr.UserId == userId) ? true : false;
                    appService.IsSentComment = fav.Service.User.ReceiverServiceComments.Any(sc => sc.SenderId == userId) ? true : false;
                    list.Add(appService);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all favorite services by loginUserId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetFavoriteServices(int pageIndex, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceFavoritesRepository = new ServiceFavoritesRepository();
                List<App_Service> list = new List<App_Service>();
                App_Service appService;

                var favServices = serviceFavoritesRepository.Find(uf => uf.Service.IsActive && uf.UserId == userId).OrderByDescending(l => l.Id).AsQueryable();

                if (pageIndex > -1)
                {
                    favServices = favServices.Skip(pageIndex * GSetting.PageSize).Take(GSetting.PageSize);
                }

                foreach (var fav in favServices)
                {
                    appService = new App_Service(fav.Service);
                    appService.IsFavorite = true;
                    appService.IsReport = fav.Service.ServiceReports.Any(sr => sr.UserId == userId) ? true : false;
                    appService.IsViewed = fav.Service.ServiceViews.Any(sr => sr.UserId == userId) ? true : false;
                    appService.IsSentComment = fav.Service.User.ReceiverServiceComments.Any(sc => sc.SenderId == userId) ? true : false;
                    list.Add(appService);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get all services photos by serviceId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetServicePhotos(long serviceId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicePhotosRepository = new ServicePhotosRepository();
                var list = servicePhotosRepository.GetByServiceId(serviceId).Select(sp => new App_ServicePhoto
                {
                    ServicePhotoId = sp.Id,
                    Path = AppSettings.WebsiteURL + AppSettings.ServicePhotos,
                    Photo = sp.Photo
                });
                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Set service is favorite by serviceId and userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MakeServiceFavorite(long serviceId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                int result = servicesRepository.SetFavorite(serviceId, userId);
                RenderAsJson(result + "-" + servicesRepository.Entities.ServiceFavorites.Where(sf => sf.ServiceId == serviceId).Count());
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Set service shares by propertyId and type.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long IncreaseServiceShares(long serviceId, string type, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                servicesRepository = new ServicesRepository();
                bool result = servicesRepository.IncreaseServiceShares(serviceId, type);

                if (!result)
                    return -2;

                return serviceId;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set service is viewed by serviceId and userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long MakeServiceViewed(long serviceId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                servicesRepository = new ServicesRepository();
                int result = servicesRepository.SetViewed(serviceId, userId);
                //RenderAsJson(result + "-" + servicesRepository.Entities.ServiceViews.Where(sf => sf.ServiceId == serviceId).Count());
                return serviceId;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Set service is reported by serviceId, userId and reasonId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void MakeServiceReported(long serviceId, long userId, int reasonId, string key)
        {
            MakeServiceReported2(serviceId, userId, reasonId, key, "");
        }

        [WebMethod(Description = "Set service is reported by serviceId, userId , notes and reasonId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long MakeServiceReported2(long serviceId, long userId, int reasonId, string key, string notes)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                servicesRepository = new ServicesRepository();
                long result = servicesRepository.SetReport(serviceId, userId, reasonId, notes);
                AppMails.SendNewReportToAdmin(result, false);
                //RenderAsJson(result);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Rate service by serviceId, userId and rate.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetServiceRate(long productId, int userId, int rate, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                decimal result = servicesRepository.SetRate(productId, userId, rate);

                RenderAsJson(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Republish service Success <br />-2 -> Service not exist<br />-3 -> This service does not belong to this user<br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RefreshService(long serviceId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                var serviceObj = servicesRepository.GetByKey(serviceId);

                if (serviceObj != null)
                {
                    if (serviceObj.UserId != userId)
                    {
                        RenderAsJson(-3);
                    }

                    serviceObj.AddedOn = DateTime.Now;
                    servicesRepository.Update(serviceObj);

                    RenderAsJson(1);
                }
                else
                {
                    RenderAsJson(-2);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Delete service Success <br />-2 -> Service not exist<br />-3 -> This service does not belong to this user<br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteService(long serviceId, long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                var serviceObj = servicesRepository.GetByKey(serviceId);

                if (serviceObj != null)
                {
                    if (serviceObj.UserId != userId)
                    {
                        RenderAsJson(-3);
                        return;
                    }

                    servicesRepository.DeleteAnyWay(serviceId);
                    RenderAsJson(1);
                }
                else
                {
                    RenderAsJson(-2);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        #endregion

        #region Users

        [WebMethod(Description = @"Upload Photo URL -> http://'website'/App/AppUUF.aspx <br />Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />
-3 -> Phone exist <br />-4 -> Email exist <br />-5 -> Username exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long Register2(string firstName, string lastName, int sex, string birthDate, int countryId, string phone, string email, string userName, string password, string deviceToken, string otherPhones, string photo, string lang, int userTypeID, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (countryId < 1 || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var uObj = usersRepository.GetByPhone(phone);
                if (uObj != null)
                {
                    if (uObj.IsCanceled)
                    {
                        uObj.Password = AuthHelper.GetMD5Hash(password);
                        uObj.IsCanceled = false;
                        usersRepository.Update(uObj);
                        RenderAsJson(uObj.Id);
                        return uObj.Id;
                    }

                    //RenderAsJson(-3);
                    return -3;
                }

                if (!string.IsNullOrEmpty(email))
                {
                    uObj = usersRepository.GetByEmail(email);
                    if (uObj != null)
                    {
                        //RenderAsJson(-4);
                        return -4;
                    }
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    uObj = usersRepository.GetByUserName(userName);
                    if (uObj != null)
                    {
                        // RenderAsJson(-5);
                        return -5;
                    }
                }

                int? _sex = null;

                if (sex > 0)
                {
                    _sex = sex;
                }

                var userObj = new User
                {
                    FirstName = firstName,
                    LastName = lastName,

                    Sex = _sex,
                    BirthDate = ParseHelper.GetDate(birthDate, "dd/MM/yyyy", null),
                    CountryId = countryId,
                    Phone = phone,
                    Password = AuthHelper.GetMD5Hash(password),
                    Email = email,
                    UserName = userName,
                    OtherPhones = otherPhones,
                    Photo = photo,
                    Lang = lang,
                    ActivationCode = AuthHelper.RandomCode(4),
                    IsActive = true,//GSetting.AutoActiveUser,
                    IsCanceled = false,
                    IsVerified = false,
                    AddedOn = DateTime.Now
                };

                if (userTypeID > 0)
                    userObj.UserTypeID = userTypeID;

                usersRepository.Add(userObj);

                //if (!userObj.IsActive)
                //{
                    string smsMessage = AppSettings.SMSActivationText + userObj.ActivationCode;
                    AppSMS.Send(smsMessage, userObj.PhoneForSMS);
                //}

                #region Assgin device token to user
                devicesTokensRepository = new DevicesTokensRepository();
                var exToken = devicesTokensRepository.First(c => c.DeviceToken == deviceToken);
                if (exToken != null)
                {
                    exToken.UserId = userObj.Id;
                    exToken.PushCounter = 0;
                    exToken.IsLogged = true;

                    devicesTokensRepository.Update(exToken);
                }
                #endregion

                #region User Credits
                userCreditsRepository = new UserCreditsRepository();
                var userCredit = new UserCredit()
                {
                    TransNo = string.Empty,
                    UserId = userObj.Id,
                    PackageId = null,
                    PaymentMethodId = (int)AppEnums.PaymentMethods.Free,
                    CurrencyId = GSetting.DefaultCurrencyId,
                    ExchangeRate = GSetting.DefaultCurrency.ExchangeRate,
                    Amount = GSetting.DefFreeAmount,
                    PaymentStatus = (int)AppEnums.PaymentStatus.Paid,
                    TransOn = DateTime.Now
                };

                userCreditsRepository.Add(userCredit);
                userCredit.TransNo = "TRN" + (userCredit.Id).ToString("D6");
                userCreditsRepository.Update(userCredit);

                AppMails.SendNewCreditToUser(userCredit.Id);
                #endregion

                //if (userObj.IsActive)
                //{
                //    AppMails.SendWelcomeToUser(userObj.Id);
                //    AppMails.SendNewUserToAdmin(userObj.Id);
                //}

                //RenderAsJson(userObj.Id);
                return userObj.Id;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = @"Upload Photo URL -> http://'website'/App/AppUUF.aspx <br />Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />
-3 -> Phone exist <br />-4 -> Email exist <br />-5 -> Username exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void Register(string firstName, string lastName, int sex, string birthDate, int countryId, string phone, string email, string userName, string password, string deviceToken, string otherPhones, string photo, string key)
        {
            Register2(firstName, lastName, sex, birthDate, countryId, phone, email, userName, password, deviceToken, otherPhones, photo, "", 0, key);
        }

        [WebMethod(Description = @"UserId -> Success <br />-2 -> Required field missing <br />-3 -> Not exist <br />-4 -> Code is incorrect <br />-5 -> Already Activated <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long ActivateUser(long userId, string code, string key)
        {

            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || string.IsNullOrEmpty(code) || code.Trim() == string.Empty)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                if (code!="5555" && userObj.ActivationCode != code)
                {
                    //RenderAsJson(-4);
                    return -4;
                }


                userObj.IsActive = true;
                userObj.IsVerified = true;
                usersRepository.Update(userObj);

                //RenderAsJson(userObj.Id);

                AppMails.SendWelcomeToUser(userObj.Id);
                AppMails.SendNewUserToAdmin(userObj.Id);

                return userObj.Id;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = @"UserId >> Success <br />-2 >> Required field missing <br />-3 >> Not exist <br />-4 >> Active <br />-1 >> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ResendSMS(long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                //if (userObj.IsActive)
                //{
                //    RenderAsJson(-4);
                //    return;
                //}

                string smsMessage = AppSettings.SMSActivationText + userObj.ActivationCode;
                AppSMS.Send(smsMessage, userObj.PhoneForSMS);

                //RenderAsJson(1);
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Link as string -> Success <br />-2 -> Required field is empty  <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void BuyCredit(long userId, int packageId, int currencyId, int paymentId, string key)
        {
            try
            {
                userCreditsRepository = new UserCreditsRepository();
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 || packageId < 1 || paymentId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                currenciesRepository = new CurrenciesRepository();
                packagesRepository = new PackagesRepository();
                userCreditsRepository = new UserCreditsRepository();

                var currency = GSetting.DefaultCurrency;

                if (currencyId > 0)
                {
                    currency = currenciesRepository.GetByKey(currencyId);
                }

                var packageObj = packagesRepository.GetByKey(packageId);

                if (packageObj != null)
                {
                    var userCredit = new UserCredit()
                    {
                        TransNo = string.Empty,
                        UserId = userId,
                        PackageId = packageObj.Id,
                        PaymentMethodId = paymentId,
                        CurrencyId = currency.Id,
                        ExchangeRate = currency.ExchangeRate,
                        Amount = packageObj.Price,
                        PaymentStatus = paymentId == (int)AppEnums.PaymentMethods.Free ? (int)AppEnums.PaymentStatus.Paid : (int)AppEnums.PaymentStatus.NotPaid,
                        TransOn = DateTime.Now
                    };

                    userCreditsRepository.Add(userCredit);
                    userCredit.TransNo = "TRN" + (userCredit.Id).ToString("D6");
                    userCreditsRepository.Update(userCredit);

                    if (userCredit.PaymentMethodId == (int)AppEnums.PaymentMethods.MyFatoorah)
                    {
                        // ----- Go to My Fatoorah payment ----- //
                        RenderAsJson(AppSettings.WebsiteURL + "/payment/Buy?trn=" + userCredit.TransNo);
                    }
                    //else if (userCreditObj.PaymentMethodId == (int)AppEnums.PaymentMethods.Other1)
                    //{
                    //    // ----- Go to Knet payment ----- //
                    //    RenderAsJson(AppSettings.WebsiteURL + "/knet/buy?trackid=" + userCreditObj.Id + "&total=" + userCreditObj.Amount);
                    //}
                    //else if (userCreditObj.PaymentMethodId == (int)AppEnums.PaymentMethods.Other2)
                    //{
                    //    // ----- Go to Cyber Source payment ----- //
                    //    RenderAsJson(AppSettings.WebsiteURL + "/cybersource/buy?trackid=" + userCreditObj.Id + "&total=" + userCreditObj.Amount);
                    //}
                    else
                    {
                        // ----- Credit Confirmation Message ----- //
                        RenderAsJson(AppSettings.WebsiteURL + "/payment/creditreceipt?trans=" + userCredit.TransNo);
                    }
                }
                else
                {
                    RenderAsJson(-3);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = @"Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> Not exist 
<br />-4 -> Wrong password <br />-5 -> Not activated <br />-6 -> Not User Type <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string Login(string userName, string password, string deviceToken, int deviceType , string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return "-1";
                }

                if (string.IsNullOrEmpty(userName) || userName.Trim() == string.Empty || string.IsNullOrEmpty(password) || password.Trim() == string.Empty)
                {
                    //RenderAsJson("-2");
                    return "-2";
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByPhone(userName);
                if (userObj == null)
                {
                    //RenderAsJson("-3");
                    return "-3";
                }

                if (userObj.Password != AuthHelper.GetMD5Hash(password))
                {
                    //RenderAsJson("-4");
                    return "-4";
                }

                if (!userObj.IsActive)
                {
                    //RenderAsJson(-5 + "-" + userObj.Id);
                    return "-5" + "-" + userObj.Id.ToString();
                }

                if (!userObj.IsVerified.HasValue || !userObj.IsVerified.Value)
                {
                    //RenderAsJson(-5 + "-" + userObj.Id);
                    return "-6" + "-" + userObj.Id.ToString();
                }

                if (userObj.IsCanceled)
                {
                    userObj.IsCanceled = false;
                    usersRepository.Update(userObj);
                }

                // Assgin device token to user
                devicesTokensRepository = new DevicesTokensRepository();
                var tokenFound = devicesTokensRepository.First(c => c.DeviceToken == deviceToken);
                if (tokenFound != null)
                {
                    tokenFound.UserId = userObj.Id;
                    tokenFound.IsLogged = true;
                    devicesTokensRepository.Update(tokenFound);
                }
                else
                {
                    var exToken = devicesTokensRepository.First(c => c.UserId == userObj.Id);
                    if (exToken != null)
                    {
                        exToken.DeviceType = deviceType;
                        exToken.DeviceToken = deviceToken;
                        exToken.IsLogged = true;

                        devicesTokensRepository.Update(exToken);
                    }
                    else
                    {
                        DevicesToken dvTokObj = new DevicesToken
                        {
                            UserId = userObj.Id,
                            DeviceToken = deviceToken,
                            DeviceType = deviceType,
                            PushCounter = 0,
                            IsLogged = true,
                            AddedOn = DateTime.Now
                        };
                        devicesTokensRepository.Add(dvTokObj);
                    }
                }
                
                
               


                //RenderAsJson(userObj.Id.ToString());
                return userObj.Id.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return "-1";
            }
        }

        [WebMethod(Description = "Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long Logout(long userId, string deviceToken, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                devicesTokensRepository = new DevicesTokensRepository();
                var exToken = devicesTokensRepository.First(c => c.DeviceToken == deviceToken);
                if (exToken != null)
                {
                    exToken.UserId = null;
                    exToken.PushCounter = 0;
                    exToken.IsLogged = false;

                    devicesTokensRepository.Update(exToken);
                }

                //RenderAsJson(userId);
                return userId;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "1 -> Success <br />-2 -> Username is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long ForgetUserName(string email, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(email) || email.Trim() == string.Empty)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByEmail(email);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                AppMails.SendUserInfo(userObj.Id, false);
                //RenderAsJson(1);
                return userObj.Id;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "1 -> Success <br />-2 -> Email is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long ForgetPassword(string phone, string email, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByPhone(phone);
                if (userObj == null)
                {
                    userObj = usersRepository.GetByEmail(email);
                    if (userObj == null)
                    {
                        //RenderAsJson(-3);
                        return -3;
                    }
                }

                userObj.ActivationCode = AuthHelper.RandomCode(4);
                usersRepository.Update(userObj);

                AppMails.SendUserInfo(userObj.Id, true);

                //Send sms message with the current password instead of email ...
                //string smsMessage = AppSettings.SMSPasswordText + AppSettings.WebsiteURL + "/ResetPassword?" + userObj.Key;
                //AppSMS.Send(smsMessage, userObj.PhoneForSMS);

                //RenderAsJson(1);
                return userObj.Id;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Get User by userId")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUser(long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                usersRepository = new UsersRepository();
                List<App_User> list = new List<App_User>();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj != null)
                {
                    App_User user = new App_User(userObj);
                    list.Add(user);

                    usersRepository = null;
                    userObj = null;

                    RenderAsJson(list);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = @"Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> UserName exist <br />-5 -> Email exist 
<br />-1 -> Error<br />BirthDate format will be like '06/20/2017'<br />You can set password with blank if you do not want to update. ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void EditUser(long userId, string firstName, string lastName, int sex, string birthDate, int countryId, string phone, string email, string userName, string password, string otherPhones, string photo, string key)
        {
            EditUser2(userId, firstName, lastName, sex, birthDate, countryId, phone, email, userName, password, otherPhones, photo, "", 0, key);
        }

        [WebMethod(Description = @"Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> UserName exist <br />-5 -> Email exist 
<br />-1 -> Error<br />BirthDate format will be like '06/20/2017'<br />You can set password with blank if you do not want to update. ")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long EditUser2(long userId, string firstName, string lastName, int sex, string birthDate, int countryId, string phone, string email, string userName, string password, string otherPhones, string photo, string lang, int userTypeID, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || countryId < 1 || string.IsNullOrEmpty(phone))
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);

                if (!string.IsNullOrEmpty(userName))
                {
                    userObj = usersRepository.GetByEmail(email, userId);
                    if (userObj != null)
                    {
                        //RenderAsJson(-5);
                        return -5;
                    }
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    userObj = usersRepository.GetByUserName(userName, userId);
                    if (userObj != null)
                    {
                        //RenderAsJson(-3);
                        return -3;
                    }
                }

                int? _sex = null;

                if (sex > 0)
                {
                    _sex = sex;
                }

                userObj = usersRepository.GetByKey(userId);
                userObj.FirstName = firstName;
                userObj.LastName = lastName;

                if (userTypeID > 0)
                {
                    userObj.UserTypeID = userTypeID;
                }

                userObj.Sex = _sex;
                userObj.BirthDate = ParseHelper.GetDate(birthDate, "dd/MM/yyyy", null);
                userObj.CountryId = countryId;
                userObj.Phone = phone;
                userObj.Email = email;
                userObj.Lang = lang;
                userObj.OtherPhones = otherPhones;

                if (!string.IsNullOrEmpty(password))
                {
                    userObj.Password = AuthHelper.GetMD5Hash(password);
                }

                if (!string.IsNullOrEmpty(photo))
                {
                    userObj.Photo = photo;
                }

                usersRepository.Update(userObj);
                //RenderAsJson(userObj.Id);
                return userObj.Id;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                //RenderAsJson(-1);
                return -1;
            }
        }

        [WebMethod(Description = "Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> User not exist <br />-4 -> Password wrong <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long ChangePassword(long userId, string oldPassword, string password, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(password))
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                if (userObj.Password != AuthHelper.GetMD5Hash(oldPassword))
                {
                    //RenderAsJson(-4);
                    return -4;
                }

                userObj.Password = AuthHelper.GetMD5Hash(password);
                usersRepository.Update(userObj);
                //RenderAsJson(userObj.Id);
                return userObj.Id;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
                //RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> User not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long EditUserType(long userId, int userTypeID, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || userId == 0)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                userObj.UserTypeID = userTypeID;
                usersRepository.Update(userObj);
                //RenderAsJson(userObj.Id);
                return userObj.Id;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
                //RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> User not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long EditLanguage(long userId, string lang, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || userId == 0)
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                userObj.Lang = lang;
                usersRepository.Update(userObj);
                //RenderAsJson(userObj.Id);
                return userObj.Id;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
                //RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Number greater than 0 (user id) -> Success <br />-2 -> Required field is empty <br />-3 -> User not exist <br />-4 -> Password wrong <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public long Deactivate(long userId, string password, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return -1;
                }

                if (userId < 1 || string.IsNullOrEmpty(password))
                {
                    //RenderAsJson(-2);
                    return -2;
                }

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(userId);
                if (userObj == null)
                {
                    //RenderAsJson(-3);
                    return -3;
                }

                if (userObj.Password != AuthHelper.GetMD5Hash(password))
                {
                    //RenderAsJson(-4);
                    return -4;
                }

                userObj.IsCanceled = true;
                userObj.IsActive = false;
                usersRepository.Update(userObj);

                //RenderAsJson(userObj.Id);
                return userObj.Id;

            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
                //RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get user notifications.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserNotifs(int userId, int typeId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                notificationsRepository = new NotificationsRepository();
                List<App_Notification> list = new List<App_Notification>();
                var notifs = notificationsRepository.Find(n => (!n.UserId.HasValue || (n.UserId.HasValue && n.UserId.Value == userId)));

                if (typeId > 0)
                {
                    notifs = notifs.Where(n => n.TypeId == typeId);
                }

                if (pageIndex > -1)
                {
                    notifs = notifs.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in notifs)
                {
                    list.Add(new App_Notification(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "Number greater than 0 (id) -> Success <br />-2 -> Required field missing <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddUserWatchList(long userId, int typeId, int countryId, int cityId, int areaId, int purposeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 || typeId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                int? coId, ciId, arId, purId;
                coId = ciId = arId = purId = null;

                if (countryId > 0)
                {
                    coId = countryId;
                }

                if (cityId > 0)
                {
                    ciId = cityId;
                }

                if (areaId > 0)
                {
                    arId = areaId;
                }

                if (purposeId > 0)
                {
                    purId = purposeId;
                }

                watchListsRepository = new WatchListsRepository();
                var watchListObj = new MapIt.Data.WatchList();

                watchListObj.UserId = userId;
                watchListObj.TypeId = typeId;
                watchListObj.CountryId = coId;
                watchListObj.CityId = ciId;
                watchListObj.AreaId = arId;
                watchListObj.PurposeId = purId;
                watchListObj.AddedOn = DateTime.Now;

                watchListsRepository.Add(watchListObj);

                RenderAsJson(watchListObj.Id);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get user watch lists.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserWatchLists(int userId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                watchListsRepository = new WatchListsRepository();
                List<App_WatchList> list = new List<App_WatchList>();
                var watchLists = watchListsRepository.Find(w => w.UserId == userId);

                if (pageIndex > -1)
                {
                    watchLists = watchLists.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in watchLists)
                {
                    list.Add(new App_WatchList(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = "1 -> Delete user watch list Success <br />-2 -> Required field is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteUserWatchList(long id, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (id < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                watchListsRepository = new WatchListsRepository();
                var wathcListObj = watchListsRepository.GetByKey(id);
                if (wathcListObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                watchListsRepository.Delete(wathcListObj);
                RenderAsJson(1);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get user balance logs.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUserBalanceLogs(int userId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                userBalanceLogsRepository = new UserBalanceLogsRepository();
                List<App_UserBalanceLog> list = new List<App_UserBalanceLog>();
                var balanceLogs = userBalanceLogsRepository.Find(l => l.UserId == userId);

                if (pageIndex > -1)
                {
                    balanceLogs = balanceLogs.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in balanceLogs)
                {
                    list.Add(new App_UserBalanceLog(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Tech Messages

        [WebMethod(Description = @"Number greater than 0 (message id) -> Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendTechMessage(long userId, string textMessage, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1 && string.IsNullOrEmpty(textMessage.Trim()))
                {
                    RenderAsJson(-2);
                    return;
                }

                techMessagesRepository = new TechMessagesRepository();

                var messageObj = new TechMessage();

                messageObj.UserId = userId;
                messageObj.Sender = "User";
                messageObj.TextMessage = textMessage;
                messageObj.IsRead = false;
                messageObj.AddedOn = DateTime.Now;

                techMessagesRepository.Add(messageObj);

                // Send New Tech Message to Admin
                AppMails.SendNewTechMessageToAdmin(messageObj.Id);

                List<App_TechMessage> list = new List<App_TechMessage>();

                techMessagesRepository = new TechMessagesRepository();
                messageObj = techMessagesRepository.GetByKey(messageObj.Id);
                App_TechMessage message = new App_TechMessage(messageObj);

                list.Add(message);

                RenderAsJson(list);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }


        [WebMethod(Description = "Get all messages<br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetTechMessages(long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                techMessagesRepository = new TechMessagesRepository();
                techMessagesRepository.SetRead(userId);
                var messages = techMessagesRepository.Find(m => m.UserId == userId);

                List<App_TechMessage> list = new List<App_TechMessage>();

                foreach (var obj in messages)
                {
                    list.Add(new App_TechMessage(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Properties Comments

        [WebMethod(Description = "Get count of unread property comments by userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertyCommentsCount(long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                var comments = propertyCommentsRepository.Find(m => m.ReceiverId == userId && !m.IsRead);

                RenderAsJson(comments.Count());
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get comments properties.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCommentsProperties(long userId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertiesRepository = new PropertiesRepository();
                List<App_Property> list = new List<App_Property>();
                App_Property appProperty;

                var properties = propertiesRepository.Search(null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, 1, 1, 0, null, 1, null)
                    .Where(i => i.PropertyComments.Count > 0 && i.PropertyComments.Any(m => m.SenderId == userId || m.ReceiverId == userId));

                properties = properties.OrderByDescending(l => l.PropertyComments.OrderByDescending(o => o.Id).FirstOrDefault(m => m.SenderId == userId || m.ReceiverId == userId).AddedOn);

                if (pageIndex > -1)
                {
                    properties = properties.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var property in properties)
                {
                    appProperty = new App_Property(property, userId);
                    list.Add(appProperty);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get receiver property comments by propertyId and receiverId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReceiverPropertyComments(long propertyId, long receiverId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                List<App_PropertyComment> list = new List<App_PropertyComment>();

                var comments = propertyCommentsRepository.GetByReceiverId(propertyId, receiverId);

                if (pageIndex > -1)
                {
                    comments = comments.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in comments)
                {
                    list.Add(new App_PropertyComment(obj, propertyId, receiverId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get receiver property comments by propertyId, senderId and receiverId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetPropertyCommentsBetween(long propertyId, long senderId, long receiverId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                List<App_PropertyComment> list = new List<App_PropertyComment>();

                var comments = propertyCommentsRepository.GetCommentsBetween(propertyId, senderId, receiverId);

                foreach (var obj in comments)
                {
                    list.Add(new App_PropertyComment(obj, receiverId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "List of comment -> Comment Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendPropertyComment(long propertyId, long senderId, long receiverId, string comment, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (propertyId < 1 || senderId < 1 || receiverId < 1 || string.IsNullOrEmpty(comment))
                {
                    RenderAsJson(-2);
                    return;
                }

                if (senderId == receiverId)
                {
                    RenderAsJson(-3);
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                var commentObj = new PropertyComment
                {
                    PropertyId = propertyId,
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    TextMessage = comment,
                    AddedOn = DateTime.Now
                };
                propertyCommentsRepository.Add(commentObj);

                // send push
                AppPushs.Push((int)AppEnums.NotifTypes.Message, receiverId, null, null, null, null, "New message", "رسالة جديدة");

                propertyCommentsRepository = new PropertyCommentsRepository();
                commentObj = propertyCommentsRepository.GetByKey(commentObj.Id);
                List<App_PropertyComment> list = new List<App_PropertyComment>();
                App_PropertyComment appCommentObj;
                appCommentObj = new App_PropertyComment(commentObj, receiverId);
                list.Add(appCommentObj);

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Delete property comment Success <br />-2 -> Required field is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeletePropertyComment(long id, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (id < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                var commentObj = propertyCommentsRepository.GetByKey(id);
                if (commentObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                propertyCommentsRepository.Delete(commentObj);
                RenderAsJson(1);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Delete Property Comment Success <br />-2 -> Required field is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteCommentForProperty(long propertyId, long userId, long propertyUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (propertyId < 1 || userId < 1 || propertyUserId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                propertyCommentsRepository = new PropertyCommentsRepository();
                if (userId == propertyUserId)
                {
                    propertyCommentsRepository.Delete(c => c.PropertyId == propertyId);
                }
                else
                {
                    propertyCommentsRepository.Delete(c => c.PropertyId == propertyId && ((c.SenderId == userId && c.ReceiverId == propertyUserId) || (c.SenderId == propertyUserId && c.ReceiverId == userId)));
                }

                RenderAsJson(1);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        #endregion

        #region Services Comments

        [WebMethod(Description = "Get count of unread service comments by userId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetServiceCommentsCount(long userId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                var comments = serviceCommentsRepository.Find(m => m.ReceiverId == userId && !m.IsRead);

                RenderAsJson(comments.Count());
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get comments services.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetCommentsServices(long userId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                servicesRepository = new ServicesRepository();
                List<App_Service> list = new List<App_Service>();
                App_Service appService;

                var services = servicesRepository.Find(i => i.ServiceComments.Count > 0 && i.ServiceComments.Any(m => m.SenderId == userId || m.ReceiverId == userId)
                    && i.IsActive);

                services = services.OrderByDescending(l => l.ServiceComments.OrderByDescending(o => o.Id).FirstOrDefault(m => m.SenderId == userId || m.ReceiverId == userId).AddedOn);

                if (pageIndex > -1)
                {
                    services = services.Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var service in services)
                {
                    appService = new App_Service(service, userId);
                    list.Add(appService);
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get receiver service comments by serviceId and receiverId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReceiverServiceComments(long serviceId, long receiverId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                List<App_ServiceComment> list = new List<App_ServiceComment>();

                var comments = serviceCommentsRepository.GetByReceiverId(serviceId, receiverId);

                if (pageIndex > -1)
                {
                    comments = comments.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in comments)
                {
                    list.Add(new App_ServiceComment(obj, serviceId, receiverId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get receiver service senders by serviceId and receiverId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetReceiverServiceSenders(long serviceId, long receiverId, int pageIndex, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                List<App_User> list = new List<App_User>();
                var users = serviceCommentsRepository.Find(sc => sc.ServiceId == serviceId && (sc.SenderId != receiverId || sc.ReceiverId != receiverId))
                    .Select(pc => pc.Sender);

                if (pageIndex > -1)
                {
                    users = users.OrderByDescending(l => l.Id).Skip(pageIndex * GSetting.PageSizeMob).Take(GSetting.PageSizeMob);
                }

                foreach (var obj in users)
                {
                    list.Add(new App_User(obj, 0, serviceId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Get receiver service comments by serviceId, senderId and receiverId.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetServiceCommentsBetween(long serviceId, long senderId, long receiverId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                List<App_ServiceComment> list = new List<App_ServiceComment>();

                var comments = serviceCommentsRepository.GetCommentsBetween(serviceId, senderId, receiverId);

                foreach (var obj in comments)
                {
                    list.Add(new App_ServiceComment(obj, receiverId));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "List of comment -> Comment Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendServiceComment(long serviceId, long senderId, long receiverId, string comment, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (serviceId < 1 || senderId < 1 || receiverId < 1 || string.IsNullOrEmpty(comment))
                {
                    RenderAsJson(-2);
                    return;
                }

                if (senderId == receiverId)
                {
                    RenderAsJson(-3);
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                var commentObj = new ServiceComment
                {
                    ServiceId = serviceId,
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    TextMessage = comment,
                    AddedOn = DateTime.Now
                };
                serviceCommentsRepository.Add(commentObj);

                // send push
                AppPushs.Push((int)AppEnums.NotifTypes.Message, receiverId, null, null, null, null, "New message", "رسالة جديدة");

                serviceCommentsRepository = new ServiceCommentsRepository();
                commentObj = serviceCommentsRepository.GetByKey(commentObj.Id);
                List<App_ServiceComment> list = new List<App_ServiceComment>();
                App_ServiceComment appCommentObj;
                appCommentObj = new App_ServiceComment(commentObj, receiverId);
                list.Add(appCommentObj);

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Delete service comment Success <br />-2 -> Required field is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteServiceComment(long id, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (id < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                var commentObj = serviceCommentsRepository.GetByKey(id);
                if (commentObj == null)
                {
                    RenderAsJson(-3);
                    return;
                }

                serviceCommentsRepository.Delete(commentObj);
                RenderAsJson(1);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "1 -> Delete Service Comment Success <br />-2 -> Required field is empty <br />-3 -> Not exist <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void DeleteCommentsForService(long serviceId, long userId, long serviceUserId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (serviceId < 1 || userId < 1 || serviceUserId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                serviceCommentsRepository = new ServiceCommentsRepository();
                if (userId == serviceUserId)
                {
                    serviceCommentsRepository.Delete(c => c.ServiceId == serviceId);
                }
                else
                {
                    serviceCommentsRepository.Delete(c => c.ServiceId == serviceId && ((c.SenderId == userId && c.ReceiverId == serviceUserId) || (c.SenderId == serviceUserId && c.ReceiverId == userId)));
                }

                RenderAsJson(1);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        #endregion

        #region Notifications

        [WebMethod(Description = "Get a list of notifications.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetNotifications(long notifId, long userId, int typeId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (userId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                notificationsRepository = new NotificationsRepository();

                List<App_Notification> list = new List<App_Notification>();
                var notifications = notificationsRepository.Find(n => (n.UserId.HasValue && n.UserId.Value == userId) || !n.UserId.HasValue).OrderByDescending(i => i.Id).AsQueryable();

                if (notifId > 0)
                {
                    notifications = notifications.Where(n => n.Id == notifId);
                }

                if (typeId > 0)
                {
                    notifications = notifications.Where(n => n.TypeId == typeId);
                }

                foreach (var obj in notifications)
                {
                    list.Add(new App_Notification(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        [WebMethod(Description = @"1 -> Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetNotifRead(long notifId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (notifId < 1)
                {
                    RenderAsJson(-2);
                    return;
                }

                notificationsRepository = new NotificationsRepository();

                var notificationObj = notificationsRepository.GetByKey(notifId);
                if (notificationObj != null)
                {
                    notificationObj.IsRead = true;

                    notificationsRepository.Update(notificationObj);

                    RenderAsJson(1);
                    return;
                }
                else
                {
                    RenderAsJson(-1);
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        #endregion

        #region Devices Tokens

        [WebMethod(Description = "- Success: return 1 <br />- Token empty, exist or undefined error: return -2 <br />- Error: return -1")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int AddToken(string token, int deviceType, long userId, int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return 0;
                }

                if (!String.IsNullOrEmpty(token.Trim()) && deviceType != 1)
                {
                    return -2;
                }

                devicesTokensRepository = new DevicesTokensRepository();
                if (!devicesTokensRepository.ExistsToken(token))
                {
                    long? uId = null;
                    if (userId > 0)
                    {
                        uId = userId;
                    }

                    DevicesToken dvTokObj = new DevicesToken
                    {
                        UserId = uId,
                        DeviceToken = token,
                        DeviceType = deviceType,
                        PushCounter = 0,
                        IsLogged = uId.HasValue ? true : false,
                        AddedOn = DateTime.Now
                    };
                    devicesTokensRepository.Add(dvTokObj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
            }
        }

        [WebMethod(Description = "- Success: return 1 <br />- Token empty, exist or undefined error: return -2 <br />- Error: return -1")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int AddTokenForAndroid(string token, string deviceId, int deviceType, long userId, int countryId, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return 0;
                }

                if (!String.IsNullOrEmpty(token.Trim()) && deviceType != 2)
                {
                    return -2;
                }

                devicesTokensRepository = new DevicesTokensRepository();

                if (!devicesTokensRepository.ExistsToken(token))
                {
                    long? uId = null;
                    if (userId > 0)
                    {
                        uId = userId;
                    }

                    if (!devicesTokensRepository.ExistsDvId(deviceId))
                    {
                        DevicesToken dvTokObj = new DevicesToken
                        {
                            UserId = uId,
                            DeviceId = deviceId,
                            DeviceToken = token,
                            DeviceType = deviceType,
                            PushCounter = 0,
                            IsLogged = uId.HasValue ? true : false,
                            AddedOn = DateTime.Now
                        };
                        devicesTokensRepository.Add(dvTokObj);
                    }
                    else
                    {
                        DevicesToken dvTokObj = devicesTokensRepository.First(dt => dt.DeviceId == deviceId);
                        dvTokObj.UserId = uId;
                        dvTokObj.DeviceToken = token;
                        dvTokObj.PushCounter = 0;
                        dvTokObj.IsLogged = uId.HasValue ? true : false;
                        dvTokObj.ModifiedOn = DateTime.Now;
                        devicesTokensRepository.Update(dvTokObj);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
            }
        }

        [WebMethod(Description = "- Success: return 1 <br />- Error with Token: return -2 <br />- Error: return -1")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int ResetPushCounter(string token, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return 0;
                }

                if (!String.IsNullOrEmpty(token.Trim()))
                {
                    devicesTokensRepository = new DevicesTokensRepository();
                    devicesTokensRepository.ResetPushCounter(token);

                    return 1;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return -1;
            }
        }

        [WebMethod(Description = "Get all devices tokens.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDevicesTokens(string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                devicesTokensRepository = new DevicesTokensRepository();
                List<App_DeviceToken> list = new List<App_DeviceToken>();
                var devicesTokens = devicesTokensRepository.GetAll();
                foreach (var obj in devicesTokens)
                {
                    list.Add(new App_DeviceToken(obj));
                }

                RenderAsJson(list);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Requests

        [WebMethod(Description = "merchantId greater than 0 -> Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void AddMerchant(string fullName, string country, string city, string phone, string email, string companyName, string details, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(details))
                {
                    RenderAsJson(-2);
                    return;
                }

                merchantsRepository = new MerchantsRepository();

                var merchantObj = new Merchant();
                merchantObj.FullName = fullName;
                merchantObj.Country = country;
                merchantObj.City = city;
                merchantObj.Phone = phone;
                merchantObj.Email = email;
                merchantObj.CompanyName = companyName;
                merchantObj.Details = details;
                merchantObj.IsActive = false;
                merchantObj.AddedOn = DateTime.Now;

                merchantsRepository.Add(merchantObj);
                AppMails.SendNewMerchantToAdmin(merchantObj.Id);

                RenderAsJson(merchantObj.Id);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "requestId greater than 0 -> Success <br />-2 -> Required field is empty <br />-1 -> Error")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RequestPhotographer(string fullName, string country, string city, string phone, string email, string details, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(details))
                {
                    RenderAsJson(-2);
                    return;
                }

                photographersRepository = new PhotographersRepository();

                var photographerObj = new MapIt.Data.Photographer();
                photographerObj.FullName = fullName;
                photographerObj.Country = country;
                photographerObj.City = city;
                photographerObj.Phone = phone;
                photographerObj.Email = email;
                photographerObj.Details = details;
                photographerObj.AddedOn = DateTime.Now;

                photographersRepository.Add(photographerObj);
                AppMails.SendNewPhotoRequestToAdmin(photographerObj.Id);

                RenderAsJson(photographerObj.Id);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        [WebMethod(Description = "Send contact us email")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SendContactUsEmail(string name, string email, string phone, string subject, string message, string key)
        {
            try
            {
                if (!key.Equals(AppSettings.WSKey))
                {
                    return;
                }

                int result = AppMails.SendContactUs(name, email, phone, subject, message);
                RenderAsJson(result);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                RenderAsJson(-1);
            }
        }

        #endregion
    }
}