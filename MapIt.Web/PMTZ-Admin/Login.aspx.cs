using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        #region Variables

        AdminUsersRepository adminUsersRepository;

        #endregion Variables

        #region Methods

        public void CheckUserData()
        {
            try
            {
                adminUsersRepository = new AdminUsersRepository();
                AdminUser adminUserObj = adminUsersRepository.Login(txtUserName.Text, AuthHelper.GetMD5Hash(txtPassword.Text));
                if (adminUserObj != null)
                {
                    if (!adminUserObj.IsActive)
                    {
                        PresentHelper.ShowScriptMessage(txtUserName.Text + " " + " is not active, please review the website administrator");
                        return;
                    }

                    if (chkRememberMe.Checked)
                    {
                        // Clear any other tickets that are already in the response
                        Response.Cookies.Clear();

                        // Set the new expiry date - to thirty days from now
                        DateTime expiryDate = DateTime.Now.AddDays(30);

                        // Create a new forms auth ticket
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, adminUserObj.Id.ToString(), DateTime.Now, expiryDate, true, String.Empty);

                        // Encrypt the ticket
                        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                        // Create a new authentication cookie - and set its expiration date
                        HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        authenticationCookie.Expires = ticket.Expiration;

                        // Add the cookie to the response.
                        Response.Cookies.Add(authenticationCookie);
                    }

                    Session["AdminUserId"] = adminUserObj.Id.ToString();
                    Session["AdminUserName"] = adminUserObj.UserName;
                    Session["AdminFullName"] = adminUserObj.FullName;

                    adminUserObj.LastLoginOn = DateTime.Now;
                    adminUsersRepository.Update(adminUserObj);

                    if (Request.QueryString["ReturnUrl"] != null && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    {
                        Response.Redirect(Request.QueryString["ReturnUrl"], false);
                    }
                    else
                    {
                        Response.Redirect(".", false);
                    }
                }
                else
                {
                    PresentHelper.ShowScriptMessage(txtUserName.Text + " " + " is invalid user Or Wrong Password");
                    return;
                }
            }
            catch (Exception ex)
            {
                PresentHelper.ShowScriptMessage("Error in logging");
                LogHelper.LogException(ex);
            }
        }

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["AdminUserId"] = Session["AdminUserName"] = Session["AdminFullName"] = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            CheckUserData();
        }

        #endregion Events
    }
}
