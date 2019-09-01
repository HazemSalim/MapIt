using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Register : MapIt.Lib.BasePage
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;

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

        void Save()
        {
            try
            {
                usersRepository = new UsersRepository();
                var uObj = usersRepository.GetByPhone(txtPhone.Text);
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

                MapIt.Data.User userObj = new MapIt.Data.User();

                userObj.CountryId = ParseHelper.GetInt(ddlCountry.SelectedValue).Value;
                userObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                userObj.Email = txtEmail.Text;
                userObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                userObj.ActivationCode = AuthHelper.RandomCode(4);
                userObj.IsActive = GeneralSetting.AutoActiveUser;
                userObj.AddedOn = DateTime.Now;

                usersRepository.Add(userObj);

                AppMails.SendWelcomeToUser(userObj.Id);
                AppMails.SendNewUserToAdmin(userObj.Id);

                if (userObj.IsActive)
                {
                    NewUserId = userObj.Id;
                    UserId = userObj.Id;
                    Response.Redirect("~/BuyCredit");
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.register_save_success);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
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

        #endregion
    }
}
