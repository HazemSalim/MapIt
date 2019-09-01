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
    public partial class Newsletter : System.Web.UI.Page
    {
        #region Properties

        UsersRepository usersRepository;
        NewsSubscribersRepository newsSubscribersRepository;

        #endregion

        #region Methods

        void SendBulkEmail(string emails)
        {
            try
            {
                string[] emailList = emails.Split(';');

                string toEmails = string.Empty;
                Int64 currentIndex = 0;

                while (currentIndex < emailList.Length)
                {
                    toEmails += emailList[currentIndex] + ";";
                    currentIndex += 1;

                    if ((currentIndex % 25) == 0)
                    {
                        MailHelper.SendEmail(AppSettings.UserMail, toEmails.Substring(0, toEmails.Length - 1), AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, txtSubject.Text, txtMessage.Text);
                        toEmails = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(toEmails))
                {
                    MailHelper.SendEmail(AppSettings.UserMail, toEmails.Substring(0, toEmails.Length - 1), AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, txtSubject.Text, txtMessage.Text);
                }

                PresentHelper.ShowScriptMessage("Message has been sent successfully");
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        void Send()
        {
            try
            {
                string emails = string.Empty;
                List<string> list = new List<string>();
                int? groupId = ParseHelper.GetInt(ddlEmailGroups.SelectedValue);

                if (groupId.HasValue)
                {
                    if (groupId.Value == 1)
                    {
                        usersRepository = new UsersRepository();
                        list = usersRepository.GetAll().Select(u => u.Email).ToList();
                    }
                    else if (groupId.Value == 2)
                    {
                        newsSubscribersRepository = new NewsSubscribersRepository();
                        list = newsSubscribersRepository.GetAll().Select(s => s.Email).ToList();
                    }
                }
                else
                {
                    usersRepository = new UsersRepository();
                    list = usersRepository.GetAll().Select(u => u.Email).ToList();

                    newsSubscribersRepository = new NewsSubscribersRepository();
                    list.AddRange(newsSubscribersRepository.GetAll().Select(s => s.Email).ToList());
                }

                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.Trim()))
                        emails = item.Trim() + ";";
                }

                emails += txtTo.Text.Trim();

                if (string.IsNullOrEmpty(emails))
                {
                    PresentHelper.ShowScriptMessage("Select at least one email or write email at [To]");
                    return;
                }

                SendBulkEmail(emails);
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
                    !(new AdminPermissionsRepository().GetByPageId((int)ParseHelper.GetInt(Session["AdminUserId"].ToString()), (int)AppEnums.AdminPages.Newsletter)))
                {
                    Response.Redirect(".");
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Send();
        }


        protected override void OnPreRenderComplete(EventArgs e)
        {
            Page.Validate(btnSend.ValidationGroup);

            base.OnPreRenderComplete(e);
        }

        #endregion
    }
}