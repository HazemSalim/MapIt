using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class Login : MapIt.Lib.BasePage
    {
        #region Variables

        CountriesRepository countriesRepository;
        UsersRepository usersRepository;

        #endregion

        #region Properties

        public int LoginUserId
        {
            get
            {
                int id = 0;
                if (ViewState["LoginUserId"] != null && int.TryParse(ViewState["LoginUserId"].ToString(), out id))
                    return id;

                return 0;
            }
            set
            {
                ViewState["LoginUserId"] = value;
            }
        }

        public string LoginUserName
        {
            get
            {
                return (ViewState["LoginUserName"] != null) ? ViewState["LoginUserName"].ToString() : string.Empty;
            }
            set
            {
                ViewState["LoginUserName"] = value;
            }
        }

        #endregion

        #region Methods

        void BindCodes()
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

        void LoginUser()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    Page.Validate(btnLogin.ValidationGroup);
                    PresentHelper.ShowScriptMessage(Resources.Resource.enter_required_fields);
                    return;
                }

                string phone = ddlCode.SelectedValue + " " + txtPhone.Text;

                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByPhone(phone);

                //check user
                if (userObj != null)
                {
                    //check password
                    if (userObj.Password == AuthHelper.GetMD5Hash(txtPassword.Text))
                    {
                        if (userObj.IsActive)
                        {
                            UserId = userObj.Id;

                            if (Request.QueryString["ReturnUrl"] != null && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                            {
                                Response.Redirect(Request.QueryString["ReturnUrl"], false);
                            }
                            else
                            {
                                Response.Redirect(".", false);
                            }

                            // Set last login datetime
                            usersRepository = new UsersRepository();
                            userObj.LastLoginOn = DateTime.Now;
                            usersRepository.Update(userObj);
                        }
                        else
                        {
                            PresentHelper.ShowScriptMessage(Resources.Resource.not_active);
                            return;
                        }
                    }
                    else
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.wrong_password);
                        return;
                    }
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.invalid_user);
                    return;
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
            if (!IsPostBack)
            {
                BindCodes();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.Validate(btnLogin.ValidationGroup);
            if (Page.IsValid)
            {
                LoginUser();
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnLogin.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}