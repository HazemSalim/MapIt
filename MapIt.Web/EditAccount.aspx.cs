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
    public partial class EditAccount : MapIt.Lib.BasePage
    {
        #region Variables

        UsersRepository usersRepository;
        CountriesRepository countriesRepository;

        #endregion

        #region Properties



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
                list = null;
                countriesRepository = null;
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void BindUser()
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(UserId);
                if (userObj != null)
                {
                    ddlSex.SelectedValue = userObj.Sex.ToString();
                    txtBDate.Text = userObj.BirthDate.HasValue ? userObj.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty;

                    var phone = userObj.Phone.Split(' ');
                    ddlCode.SelectedValue = phone[0];
                    txtPhone.Text = phone[1];

                    txtEmail.Text = userObj.Email;
                }
                else
                {
                    Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Edit()
        {
            try
            {
                usersRepository = new UsersRepository();

                var userObj = usersRepository.GetByKey(UserId);
                if (userObj == null)
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.error);
                    return;
                }

                userObj.Sex = ParseHelper.GetInt(ddlSex.SelectedValue).Value;
                userObj.BirthDate = !string.IsNullOrEmpty(txtBDate.Text) ? ParseHelper.GetDate(txtBDate.Text, "yyyy-MM-dd", null) : null;
                userObj.Phone = ddlCode.SelectedValue + " " + txtPhone.Text;
                userObj.Email = txtEmail.Text;
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    userObj.Password = AuthHelper.GetMD5Hash(txtPassword.Text);
                }
                userObj.ModifiedOn = DateTime.Now;

                usersRepository.Update(userObj);

                PresentHelper.ShowScriptMessage(Resources.Resource.save_success);
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
            if (UserId <= 0)
            {
                Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
            }

            if (!IsPostBack)
            {
                BindCountries();
                BindUser();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);
            if (Page.IsValid)
            {
                Edit();
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