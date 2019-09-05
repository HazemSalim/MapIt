using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        #region Variables

        CountriesRepository countriesRepository;
        //OffersRepository offersRepository;

        #endregion

        #region Properties



        #endregion

        #region Methods

        void BindSettings()
        {
            try
            {
                BasePage pageObj = Page as BasePage;
                if (pageObj != null)
                {
                    mDes.Attributes["content"] = pageObj.GeneralSetting.MetaDesc;
                    mKey.Attributes["content"] = pageObj.GeneralSetting.MetaKW;
                    //litEmail.Text = pageObj.GeneralSetting.Email;

                    aFacebook.HRef = pageObj.GeneralSetting.Facebook;
                    aYoutube.HRef = pageObj.GeneralSetting.Youtube;
                    aTwitter.HRef = pageObj.GeneralSetting.Twitter;

                    aPhone.HRef = "tel:" + pageObj.GeneralSetting.Phone;
                    aPhone.InnerText = pageObj.GeneralSetting.Phone;

                    aEmail.HRef = "mailto:" + pageObj.GeneralSetting.Email;
                    aEmail.InnerText = pageObj.GeneralSetting.Email;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindCountries()
        {
            try
            {
                countriesRepository = new CountriesRepository();
                var countries = countriesRepository.Find(c => c.IsActive).ToList();
                if (countries != null && countries.Count > 0)
                {
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataTextField = Resources.Resource.db_title_col;

                    ddlCountry.DataSource = countries;
                    ddlCountry.DataBind();
                }

                countries = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void CheckUserSession()
        {
            try
            {
                BasePage pageObj = Page as BasePage;
                if (pageObj != null)
                {
                    if (pageObj.UserId > 0)
                    {
                        aAcc.Visible = aMobAcc.Visible = false;
                        aAcc.HRef = aMobAcc.HRef = "/Account";
                        litAcc.Text = litMobAcc.Text = Resources.Resource.account;

                        aLog.HRef = aMobLog.HRef = "/Logout";
                        litLog.Text = litMobLog.Text = Resources.Resource.logout;
                    }
                    else
                    {
                        aAcc.Visible = aMobAcc.Visible = false;
                        aAcc.HRef = aMobAcc.HRef = "/Register";
                        litAcc.Text = litMobAcc.Text = Resources.Resource.register;

                        aLog.HRef = aMobLog.HRef = "/Login";
                        litLog.Text = litMobLog.Text = Resources.Resource.login;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void UpdateLocal()
        {
            try
            {
                BasePage pageObj = Page as BasePage;
                if (pageObj != null)
                {
                    ddlCountry.SelectedValue = pageObj.CountryId.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void UpdateLang()
        {
            try
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ar-kw")
                {
                    HttpCookie CultureCookie = Request.Cookies["Culture"];
                    if (CultureCookie == null)
                        CultureCookie = new HttpCookie("Culture");
                    CultureCookie.Value = "en-US";
                    CultureCookie.Expires = DateTime.Now.AddYears(1);
                    Response.SetCookie(CultureCookie);
                    Response.Redirect(Request.Url.ToString(), false);
                }
                else
                {
                    HttpCookie CultureCookie = Request.Cookies["Culture"];
                    if (CultureCookie == null)
                        CultureCookie = new HttpCookie("Culture");
                    CultureCookie.Value = "ar-KW";
                    CultureCookie.Expires = DateTime.Now.AddYears(1);
                    Response.SetCookie(CultureCookie);
                    Response.Redirect(Request.Url.ToString(), false);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void UpdateCountry(int countryId)
        {
            try
            {
                HttpCookie CountryCookie = Request.Cookies["CountryId"];
                if (CountryCookie == null)
                    CountryCookie = new HttpCookie("CountryId");
                CountryCookie.Value = countryId.ToString();
                CountryCookie.Expires = DateTime.Now.AddYears(1);
                Response.SetCookie(CountryCookie);
                Response.Redirect(Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindOffers()
        {
            try
            {
                //offersRepository = new OffersRepository();
                //var list = offersRepository.Find(n => n.IsActive).OrderBy(c => c.Ordering.HasValue ? 0 : 1).ThenBy(c => c.Ordering).ThenByDescending(c => Guid.NewGuid()).Take(4);
                //if (list != null && list.Count() > 0)
                //{
                //    rOffers.DataSource = list;
                //    rOffers.DataBind();
                //}
                //list = null;
                //offersRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSettings();
                BindCountries();
                BindOffers();
                UpdateLocal();
            }
        }

        protected void lnkLanguage_Click(object sender, EventArgs e)
        {
            UpdateLang();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCountry(ParseHelper.GetInt(ddlCountry.SelectedValue).Value);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            CheckUserSession();
        }

        #endregion
    }
}