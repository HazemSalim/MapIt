using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web
{
    public partial class ChangePassword : MapIt.Lib.BasePage
    {
        #region Variables

        UsersRepository usersRepository;

        #endregion

        #region Methods

        void Save()
        {
            try
            {
                usersRepository = new UsersRepository();
                var userObj = usersRepository.GetByKey(UserId);

                if (userObj != null)
                {
                    if (userObj.Password != AuthHelper.GetMD5Hash(txtOldPassword.Text))
                    {
                        PresentHelper.ShowScriptMessage(Resources.Resource.wrong_password);
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
                        userObj.Password = AuthHelper.GetMD5Hash(txtNewPassword.Text);

                    usersRepository.Update(userObj);
                    PresentHelper.ShowScriptMessage(Resources.Resource.save_success, "Account.aspx");
                }
                else
                {
                    PresentHelper.ShowScriptMessage(Resources.Resource.error);
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
            if (UserId <= 0)
            {
                Response.Redirect("~/Login?ReturnUrl=" + Request.RawUrl, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);
            if (Page.IsValid)
            {
                Save();
                Response.Redirect("~/Account", true);
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