﻿using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Register : BasePage
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;
        UserTypesRepository userTypesRepository;
        UserCreditsRepository userCreditsRepository;
        GeneralSettingsRepository gSettingsRepository;

        #endregion

        #region Properties

        public long NewUserId
        {
            get
            {
                int id = 0;
                if (ViewState["NewUserId"] != null && int.TryParse(ViewState["NewUserId"].ToString(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["NewUserId"] = value;
            }
        }

        #endregion

        #region Methods

        void BindCountries()
        {
            try
            {
                countriesRepository = new CountriesRepository();
                var list = countriesRepository.Find(c => c.IsActive).ToList();
                list = Culture.ToLower() == "ar-kw" ? list.OrderBy(c => c.TitleAR).ToList() : list.OrderBy(c => c.TitleEN).ToList();

                if (list != null && list.Count > 0)
                {
                    ddlCountry.DataValueField = "Id";
                    ddlCountry.DataTextField = Resources.Resource.db_title_col;

                    ddlCode.DataValueField = "CCode";
                    ddlCode.DataTextField = "FullCode";

                    ddlCountry.DataSource = list;
                    ddlCountry.DataBind();

                    ddlCode.DataSource = list.OrderBy(c => c.ISOCode).ToList();
                    ddlCode.DataBind();
                }
                string country = "Kuwait";
                var defaultcountryobj = countriesRepository.Find(x => x.TitleEN.Trim().ToLower().Contains(country.ToLower())).SingleOrDefault();
                if (defaultcountryobj != null)
                {
                    ddlCountry.SelectedValue = defaultcountryobj.Id.ToString();
                    ddlCode.SelectedValue = defaultcountryobj.CCode.ToString();
                }
                list = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindUserTypes()
        {
            try
            {
                userTypesRepository = new UserTypesRepository();
                var list = userTypesRepository.GetAll().ToList();
                list = Culture.ToLower() == "ar-kw" ? list.OrderBy(c => c.TitleAR).ToList() : list.OrderBy(c => c.TitleEN).ToList();

                if (list != null && list.Count > 0)
                {
                    ddlUserTypes.DataValueField = "Id";
                    ddlUserTypes.DataTextField = Resources.Resource.db_title_col;

                    ddlUserTypes.DataSource = list;
                    ddlUserTypes.DataBind();
                }
                list = null;
                userTypesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }
        
        void Save()
        {
            try
            {
                usersRepository = new UsersRepository();

                string phone = ddlCode.SelectedValue + " " + txtPhone.Text;

                var uObj = usersRepository.GetByPhone(phone);
                if (uObj != null)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.phone_exist);
                    return;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    uObj = usersRepository.GetByEmail(txtEmail.Text);
                    if (uObj != null)
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.email_exist);
                        return;
                    }
                }

                if (!chekTerms.Checked)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.msg_terms);
                    return;
                }

                User userObj = new User
                {
                    CountryId = ParseHelper.GetInt(ddlCountry.SelectedValue).Value,
                    Phone = phone,
                    Email = txtEmail.Text,
                    UserTypeID = int.Parse(ddlUserTypes.SelectedValue),
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    //Lang = ddlLanguage.SelectedValue,
                    Password = AuthHelper.GetMD5Hash(txtPassword.Text),
                    ActivationCode = AuthHelper.RandomCode(4),
                    IsActive = false,//GeneralSetting.AutoActiveUser,
                    IsCanceled = false,
                    AddedOn = DateTime.Now
                };

                usersRepository.Add(userObj);

                if (!userObj.IsActive)
                {
                    string smsMessage = AppSettings.SMSActivationText + userObj.ActivationCode;
                    AppSMS.Send(smsMessage, userObj.PhoneForSMS);
                }


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

                if (!userObj.IsActive)
                {
                    AppMails.SendWelcomeToUser(userObj.Id);
                    AppMails.SendNewUserToAdmin(userObj.Id);
                }

                NewUserId = userObj.Id;
                UserId = userObj.Id;

                registerDiv.Visible = false;
                smsVerificationDiv.Visible = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
            }
        }

        void smsVerification()
        {
            if (string.IsNullOrEmpty(txtActivationCode.Text))
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.wrong_activation_code);
            }
            else
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(NewUserId);

                if (userObj.ActivationCode != txtActivationCode.Text)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.wrong_activation_code);
                    return;
                }


                userObj.IsActive = true;
                usersRepository.Update(userObj);

                AppMails.SendWelcomeToUser(userObj.Id);
                AppMails.SendNewUserToAdmin(userObj.Id);

                Response.Redirect("~/BuyCredit");

            }
        }

        void resendSMS()
        {
            var userObj = usersRepository.GetByKey(NewUserId);
            if (userObj != null)
            {
                string smsMessage = AppSettings.SMSActivationText + userObj.ActivationCode;
                AppSMS.Send(smsMessage, userObj.PhoneForSMS);

                PresentHelper.ShowScriptMessage(Resources.Resource.send_email_success);
            }

        }

        public GeneralSetting GSetting
        {
            get
            {
                gSettingsRepository = new GeneralSettingsRepository();
                var gSettingObj = gSettingsRepository.Get();
                return gSettingObj;
            }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserId != 0)
            {
                UserId = 0;
            }

            if (!IsPostBack)
            {
                BindCountries();
                BindUserTypes();
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Page.Validate(btnRegister.ValidationGroup);
            if (Page.IsValid)
            {
                Save();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnRegister.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        protected void btnSMSVerification_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSMSVerification.ValidationGroup);
            if (Page.IsValid)
            {
                smsVerification();
            }
        }

        protected void btnResendSMS_Click(object sender, EventArgs e)
        {
            resendSMS();
        }

        #endregion


    }
}
