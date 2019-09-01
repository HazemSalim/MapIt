using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using MapIt.Data;
using MapIt.Repository;
using MapIt.Helpers;

namespace MapIt.Lib
{
    public class BasePage : System.Web.UI.Page
    {
        #region Properties

        public string Culture
        {
            get
            {
                HttpCookie CultureCookie = Request.Cookies["Culture"];
                if (CultureCookie != null && !string.IsNullOrEmpty(CultureCookie.Value))
                    return CultureCookie.Value;
                else
                    return "en-US";
            }
        }


        private GeneralSetting _generalSetting = null;
        public GeneralSetting GeneralSetting
        {
            get
            {
                var repository = new GeneralSettingsRepository();
                _generalSetting = repository.Get();

                return _generalSetting;
            }
        }

        public int CountryId
        {
            get
            {
                HttpCookie CountryCookie = Request.Cookies["CountryId"];
                int id = -1;
                if (CountryCookie != null && !string.IsNullOrEmpty(CountryCookie.Value) && int.TryParse(CountryCookie.Value, out id))
                {
                    return id;
                }

                return GeneralSetting.DefaultCountryId;
            }
        }

        private Country _country = null;
        public Country Country
        {
            get
            {
                var repository = new CountriesRepository();
                _country = repository.GetByKey(CountryId);

                return _country;
            }
        }

        public long UserId
        {
            get
            {
                int id = 0;
                if (Session["UserId"] != null && int.TryParse(Session["UserId"].ToString(), out id))
                    return id;

                return 0;
            }
            set
            {
                Session["UserId"] = value;
            }
        }

        private User _user = null;
        public User User
        {
            get
            {
                var repository = new UsersRepository();
                _user = repository.GetByKey(UserId);

                return _user;
            }
        }

        #endregion

        #region Methods

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Culture);
            base.InitializeCulture();
        }

        #endregion
    }
}