using System;
using System.Web.UI;
using System.Text;
using System.IO;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Payment
{
    public partial class Error : BasePage
    {
        PaymentTransactionsRepository paymentTransactionsRepository;

        #region Methods

        void LoadData(string ID,string Type)
        {
            try
            {
                paymentTransactionsRepository = new PaymentTransactionsRepository();

                Data.PaymentTransaction payObj = null;

                if (Type == "InvoiceID")
                    payObj = paymentTransactionsRepository.GetByInvoiceId(ID);
                else if (Type == "PaymentID")
                    payObj = paymentTransactionsRepository.GetByPaymentId(ID);
                
                if (payObj != null)
                {
                    //if (UserId <= 0 || payObj.UserCredit.UserId != UserId)
                    //{
                    //    Response.Redirect("..");
                    //}

                    lbl_SuccessOrNot.Text = payObj.Result;
                    lbl_Amount.Text = payObj.Amount.ToString();
                    lbl_ReferenceId.Text = payObj.RefId;
                    lbl_PaymentId.Text = payObj.PaymentId;
                    lbl_MerchantTrackId.Text = payObj.UserCredit.TransNo;
                    lbl_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

                    var sb = new StringBuilder();
                    divcontent.RenderControl(new HtmlTextWriter(new StringWriter(sb)));

                    string body = "<table style='direction:ltr;'>";
                    body += "<tr><td colspan='2'>Hello,<br /><br /></td></tr>";
                    body += "<tr><td colspan='2'>   Please find blow your payment details. <br /><br /></td></tr>";
                    body += sb;
                    body += "<tr><td colspan='2'>Best Regards,</br><a href='" + AppSettings.WebsiteURL + "'>MapIt</a></td></tr></table>";

                    MailHelper.SendEmail(AppSettings.UserMail, payObj.UserCredit.User.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, "MapIt - Payment Error", body);
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
                string param = string.Empty;
                if (Request.QueryString["ref"] != null && !string.IsNullOrEmpty(Request.QueryString["ref"].Trim()))
                {
                    LoadData(Request.QueryString["ref"].Trim(),"InvoiceID");
                }
                else if (Request.QueryString["paymentId"] != null && !string.IsNullOrEmpty(Request.QueryString["paymentId"].Trim()))
                {
                    LoadData(Request.QueryString["paymentId"].Trim(), "PaymentID");
                }
                else
                {
                    Response.Redirect("..");
                }
            }
        }

        #endregion
    }
}