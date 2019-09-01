using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class GeneralSettings : System.Web.UI.Page
    {
        #region Variables

        CountriesRepository countriesRepository;
        CurrenciesRepository currenciesRepository;
        GeneralSettingsRepository gSettingsRepository;

        #endregion

        #region Properties

        public int? RecordId
        {
            get
            {
                int id = 0;

                if (ViewState["RecordId"] != null && int.TryParse(ViewState["RecordId"].ToString(), out id))
                    return id;

                return null;
            }
            set
            {
                ViewState["RecordId"] = value;
            }
        }

        #endregion

        #region Methods

        void BindCountries()
        {
            try
            {
                countriesRepository = new CountriesRepository();
                var data = countriesRepository.Find(c => c.IsActive).OrderBy(c => c.TitleEN).ToList();
                if (data != null && data.Count > 0)
                {
                    ddlDefaultCountry.DataValueField = "Id";
                    ddlDefaultCountry.DataTextField = "TitleEN";

                    ddlDefaultCountry.DataSource = data;
                    ddlDefaultCountry.DataBind();
                }
                data = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindCurrencies()
        {
            try
            {
                currenciesRepository = new CurrenciesRepository();
                var data = currenciesRepository.Find(c => c.IsActive).ToList();
                if (data != null && data.Count > 0)
                {
                    ddlDefaultCurrency.DataValueField = "Id";
                    ddlDefaultCurrency.DataTextField = "TitleEN";

                    ddlDefaultCurrency.DataSource = data;
                    ddlDefaultCurrency.DataBind();
                }
                data = null;
                currenciesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void LoadData()
        {
            try
            {
                gSettingsRepository = new GeneralSettingsRepository();
                GeneralSetting gSettingObj = gSettingsRepository.Get();
                if (gSettingObj != null)
                {
                    RecordId = gSettingObj.Id;
                    txtTitleEN.Text = gSettingObj.TitleEN;
                    txtTitleAR.Text = gSettingObj.TitleAR;
                    txtMetaDescription.Text = gSettingObj.MetaDesc;
                    txtMetaKW.Text = gSettingObj.MetaKW;
                    txtWebsite.Text = gSettingObj.Website;
                    txtFacebook.Text = gSettingObj.Facebook;
                    txtTwitter.Text = gSettingObj.Twitter;
                    txtInstagram.Text = gSettingObj.Instagram;
                    txtYoutube.Text = gSettingObj.Youtube;
                    txtLinkedin.Text = gSettingObj.Linkedin;
                    txtSnapchat.Text = gSettingObj.Snapchat;
                    txtTumblr.Text = gSettingObj.Tumblr;
                    txtAppStore.Text = gSettingObj.AppStore;
                    txtGooglePlay.Text = gSettingObj.GooglePlay;
                    txtLongitude.Text = gSettingObj.Longitude;
                    txtLatitude.Text = gSettingObj.Latitude;
                    txtAddress.Text = gSettingObj.Address;
                    txtEmail.Text = gSettingObj.Email;
                    txtPhone.Text = gSettingObj.Phone;
                    txtFax.Text = gSettingObj.Fax;
                    ddlDefaultCountry.SelectedValue = gSettingObj.DefaultCountryId.ToString();
                    ddlDefaultCurrency.SelectedValue = gSettingObj.DefaultCurrencyId.ToString();
                    txtInvoiceTerms.Text = gSettingObj.InvoiceTerms;
                    txtWorkingHours.Text = gSettingObj.WorkingHours;
                    txtDefFreeAmount.Text = gSettingObj.DefFreeAmount.ToString();
                    txtNormalAdCost.Text = gSettingObj.NormalAdCost.ToString();
                    txtSpecAdCost.Text = gSettingObj.SpecAdCost.ToString();
                    txtAdVideoCost.Text = gSettingObj.AdVideoCost.ToString();
                    txtNAdDuration.Text = gSettingObj.NormalAdDuration.ToString();
                    txtSAdDuration.Text = gSettingObj.SpecAdDuration.ToString();
                    txtAvPhotos.Text = gSettingObj.AvPhotos.ToString();
                    txtAvVideos.Text = gSettingObj.AvVideos.ToString();
                    txtSimilarAdCount.Text = gSettingObj.SimilarAdCount.ToString();
                    txtPageSize.Text = gSettingObj.PageSize.ToString();
                    txtPageSizeMob.Text = gSettingObj.PageSizeMob.ToString();
                    chkAutoActiveUser.Checked = gSettingObj.AutoActiveUser;
                    chkAutoActiveAd.Checked = gSettingObj.AutoActiveAd;
                    txtVersion.Text = gSettingObj.Version.ToString();
                }
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
                if (Session["AdminUserId"] != null && (int)ParseHelper.GetInt(Session["AdminUserId"].ToString()) > 1 &&
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.GeneralSettings)))
                {
                    Response.Redirect(".");
                }

                BindCountries();
                BindCurrencies();
                LoadData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int? country = ParseHelper.GetInt(ddlDefaultCountry.SelectedValue);
                if (!country.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select default country");
                    return;
                }

                int? currency = ParseHelper.GetInt(ddlDefaultCurrency.SelectedValue);
                if (!currency.HasValue)
                {
                    PresentHelper.ShowScriptMessage("Select default currency");
                    return;
                }

                gSettingsRepository = new GeneralSettingsRepository();
                GeneralSetting gSettingObj = new GeneralSetting();

                if (RecordId.HasValue)
                {
                    gSettingObj = gSettingsRepository.GetByKey(RecordId.Value);
                    if (gSettingObj != null)
                    {
                        gSettingObj.TitleAR = txtTitleAR.Text;
                        gSettingObj.TitleEN = txtTitleEN.Text;
                        gSettingObj.MetaDesc = txtMetaDescription.Text;
                        gSettingObj.MetaKW = txtMetaKW.Text;
                        gSettingObj.Website = txtWebsite.Text;
                        gSettingObj.Facebook = txtFacebook.Text;
                        gSettingObj.Twitter = txtTwitter.Text;
                        gSettingObj.Instagram = txtInstagram.Text;
                        gSettingObj.Youtube = txtYoutube.Text;
                        gSettingObj.Linkedin = txtLinkedin.Text;
                        gSettingObj.Snapchat = txtSnapchat.Text;
                        gSettingObj.Tumblr = txtTumblr.Text;
                        gSettingObj.AppStore = txtAppStore.Text;
                        gSettingObj.GooglePlay = txtGooglePlay.Text;
                        gSettingObj.Longitude = txtLongitude.Text;
                        gSettingObj.Latitude = txtLatitude.Text;
                        gSettingObj.Address = txtAddress.Text;
                        gSettingObj.Email = txtEmail.Text;
                        gSettingObj.Phone = txtPhone.Text;
                        gSettingObj.Fax = txtFax.Text;
                        gSettingObj.DefaultCountryId = country.Value;
                        gSettingObj.DefaultCurrencyId = currency.Value;
                        gSettingObj.InvoiceTerms = txtInvoiceTerms.Text;
                        gSettingObj.WorkingHours = txtWorkingHours.Text;
                        gSettingObj.DefFreeAmount = ParseHelper.GetInt(txtDefFreeAmount.Text).Value;
                        gSettingObj.NormalAdCost = ParseHelper.GetInt(txtNormalAdCost.Text).Value;
                        gSettingObj.SpecAdCost = ParseHelper.GetInt(txtSpecAdCost.Text).Value;
                        gSettingObj.AdVideoCost = ParseHelper.GetInt(txtAdVideoCost.Text).Value;
                        gSettingObj.NormalAdDuration = ParseHelper.GetInt(txtNAdDuration.Text).Value;
                        gSettingObj.SpecAdDuration = ParseHelper.GetInt(txtSAdDuration.Text).Value;
                        gSettingObj.AvPhotos = ParseHelper.GetInt(txtAvPhotos.Text).Value;
                        gSettingObj.AvVideos = ParseHelper.GetInt(txtAvVideos.Text).Value;
                        gSettingObj.SimilarAdCount = ParseHelper.GetInt(txtSimilarAdCount.Text).Value;
                        gSettingObj.PageSize = ParseHelper.GetInt(txtPageSize.Text).Value;
                        gSettingObj.PageSizeMob = ParseHelper.GetInt(txtPageSizeMob.Text).Value;
                        gSettingObj.AutoActiveUser = chkAutoActiveUser.Checked;
                        gSettingObj.AutoActiveAd = chkAutoActiveAd.Checked;
                        gSettingObj.Version = ParseHelper.GetDouble(txtVersion.Text).Value;
                        gSettingObj.ModifiedOn = DateTime.Now;
                        gSettingObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                        gSettingsRepository.Update(gSettingObj);
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage("Error in saving");
                        return;
                    }
                }
                else
                {
                    gSettingObj.Id = 0;
                    gSettingObj.TitleAR = txtTitleAR.Text;
                    gSettingObj.TitleEN = txtTitleEN.Text;
                    gSettingObj.MetaDesc = txtMetaDescription.Text;
                    gSettingObj.MetaKW = txtMetaKW.Text;
                    gSettingObj.Website = txtWebsite.Text;
                    gSettingObj.Facebook = txtFacebook.Text;
                    gSettingObj.Twitter = txtTwitter.Text;
                    gSettingObj.Instagram = txtInstagram.Text;
                    gSettingObj.Youtube = txtYoutube.Text;
                    gSettingObj.Linkedin = txtLinkedin.Text;
                    gSettingObj.Snapchat = txtSnapchat.Text;
                    gSettingObj.Tumblr = txtTumblr.Text;
                    gSettingObj.AppStore = txtAppStore.Text;
                    gSettingObj.GooglePlay = txtGooglePlay.Text;
                    gSettingObj.Longitude = txtLongitude.Text;
                    gSettingObj.Latitude = txtLatitude.Text;
                    gSettingObj.Address = txtAddress.Text;
                    gSettingObj.Email = txtEmail.Text;
                    gSettingObj.Phone = txtPhone.Text;
                    gSettingObj.Fax = txtFax.Text;
                    gSettingObj.DefaultCountryId = country.Value;
                    gSettingObj.DefaultCurrencyId = currency.Value;
                    gSettingObj.InvoiceTerms = txtInvoiceTerms.Text;
                    gSettingObj.WorkingHours = txtWorkingHours.Text;
                    gSettingObj.DefFreeAmount = ParseHelper.GetInt(txtDefFreeAmount.Text).Value;
                    gSettingObj.NormalAdCost = ParseHelper.GetInt(txtNormalAdCost.Text).Value;
                    gSettingObj.SpecAdCost = ParseHelper.GetInt(txtSpecAdCost.Text).Value;
                    gSettingObj.AdVideoCost = ParseHelper.GetInt(txtAdVideoCost.Text).Value;
                    gSettingObj.NormalAdDuration = ParseHelper.GetInt(txtNAdDuration.Text).Value;
                    gSettingObj.SpecAdDuration = ParseHelper.GetInt(txtSAdDuration.Text).Value;
                    gSettingObj.AvPhotos = ParseHelper.GetInt(txtAvPhotos.Text).Value;
                    gSettingObj.AvVideos = ParseHelper.GetInt(txtAvVideos.Text).Value;
                    gSettingObj.SimilarAdCount = ParseHelper.GetInt(txtSimilarAdCount.Text).Value;
                    gSettingObj.PageSize = ParseHelper.GetInt(txtPageSize.Text).Value;
                    gSettingObj.PageSizeMob = ParseHelper.GetInt(txtPageSizeMob.Text).Value;
                    gSettingObj.AutoActiveUser = chkAutoActiveUser.Checked;
                    gSettingObj.AutoActiveAd = chkAutoActiveAd.Checked;
                    gSettingObj.Version = ParseHelper.GetDouble(txtVersion.Text).Value;
                    gSettingObj.AddedOn = DateTime.Now;
                    gSettingObj.AdminUserId = ParseHelper.GetInt(Session["AdminUserId"].ToString());

                    gSettingsRepository.Add(gSettingObj);
                    LoadData();
                }
                PresentHelper.ShowScriptMessage("Save successfully");
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in saving");
                LogHelper.LogException(ex);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}