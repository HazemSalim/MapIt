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
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Variables

        AdminUsersRepository adminUsersRepository;

        #endregion Variables

        #region Methods

        public void ClearControls()
        {
            txtNewPassword.Text = txtNewPassConfirm.Text = txtOldPassword.Text = string.Empty;
        }

        #endregion Methods

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                AdminUser adminUserObj = adminUsersRepository.Login(Session["AdminUserName"].ToString(), AuthHelper.GetMD5Hash(txtOldPassword.Text));
                if (adminUserObj != null)
                {
                    if (adminUserObj.IsActive == false)
                    {
                        PresentHelper.ShowScriptMessage(Session["AdminUserName"].ToString() + " is not active, please review the administrator");
                    }

                    adminUserObj.Password = AuthHelper.GetMD5Hash(txtNewPassword.Text);
                    adminUsersRepository.Update(adminUserObj);
                    ClearControls();
                    PresentHelper.ShowScriptMessage("Update successfully");
                }
                else
                {
                    PresentHelper.ShowScriptMessage("Old Password not valid");
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in updating");
                LogHelper.LogException(ex);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSave.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion Events
    }
}