using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Forget : MapIt.Lib.BasePage
    {
        #region Variables

        CountriesRepository countriesRepository;
        UsersRepository usersRepository;

        #endregion

        #region Properties

        public string Op
        {
            get
            {
                if (ViewState["op"] != null)
                    return ViewState["op"].ToString();
                return null;
            }
            set
            {
                ViewState["op"] = value;
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
                    ddlCode.DataValueField = "CCode";
                    ddlCode.DataTextField = "FullCode";

                    ddlCode.DataSource = list.OrderBy(c => c.ISOCode).ToList();
                    ddlCode.DataBind();
                }
                string country = "Kuwait";
                var defaultcountryobj = countriesRepository.Find(x => x.TitleEN.Trim().ToLower().Contains(country.ToLower())).SingleOrDefault();
                if (defaultcountryobj != null)
                {
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

        void Submit()
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByPhone(txtPhone.Text);
                if (userObj == null)
                {
                    userObj = usersRepository.GetByEmail(txtEmail.Text);
                    if (userObj == null)
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.phone_email_not_exist);
                        return;
                    }
                }

                string password = AuthHelper.RandomString(6);

                userObj.Password = AuthHelper.GetMD5Hash(password);
                usersRepository.Update(userObj);

                if (Op == "password")
                {
                    AppMails.SendUserInfo(userObj.Id, true);
                    PresentHelper.ShowScriptMessage(Resources.Resource.reset_password_send_success, "/Login");
                    return;
                }
                else
                {
                    AppMails.SendUserInfo(userObj.Id, false);
                    PresentHelper.ShowScriptMessage(Resources.Resource.user_name_send_success, "/Login");
                    return;
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage(Resources.Resource.error);
                LogHelper.LogException(ex);
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
                if (Request.QueryString["op"] != null && Request.QueryString["op"].Trim() != string.Empty)
                {
                    Op = Request.QueryString["op"].ToLower().Trim();

                    if (Op == "username")
                    {
                        Title = Resources.Resource.web_title + " | " + Resources.Resource.forget_username;
                        litTitle.Text = Resources.Resource.forget_username;
                    }
                    else if (Op == "password")
                    {
                        Title = Resources.Resource.web_title + " | " + Resources.Resource.forget_username;
                        litTitle.Text = Resources.Resource.forget_password;
                    }
                    else
                    {
                        Response.Redirect("~/Login", true);
                    }
                }
                else
                {
                    Response.Redirect("~/Login", true);
                }

                BindCountries();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);
            if (Page.IsValid)
            {
                Submit();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSubmit.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}