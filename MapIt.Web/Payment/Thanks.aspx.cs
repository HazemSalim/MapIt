using System;
using System.Web.UI;
using System.Text;
using System.IO;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.Payment
{
    public partial class Thanks : MapIt.Lib.BasePage
    {
        #region Variables

        PaymentTransactionsRepository paymentTransactionsRepository;

        #endregion

        #region Properties

        public string RefNo
        {
            get
            {
                if (ViewState["RefNo"] != null && !string.IsNullOrEmpty(ViewState["RefNo"].ToString().Trim()))
                    return ViewState["RefNo"].ToString().Trim();

                return string.Empty;
            }
            set
            {
                ViewState["RefNo"] = value;
            }
        }

        #endregion

        #region Methods

        void LoadData()
        {
            try
            {
                paymentTransactionsRepository = new PaymentTransactionsRepository();

                var payObj = paymentTransactionsRepository.GetByPaymentId(RefNo);
                if (payObj != null)
                {
                    if (UserId <= 0 || payObj.UserCredit.UserId != UserId)
                    {
                        Response.Redirect("..");
                    }

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

                    MailHelper.SendEmail(AppSettings.UserMail, payObj.UserCredit.User.Email, AppSettings.SMTPServer, AppSettings.UserMail, AppSettings.PasswordMail, "MapIt - Payment Confirmation", body);

                    // Emails Confirmation
                    AppMails.SendNewCreditToUser(payObj.UserCredit.Id);
                    AppMails.SendNewCreditToAdmin(payObj.UserCredit.Id);
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
                if (Request.QueryString["ref"] != null && !string.IsNullOrEmpty(Request.QueryString["ref"].Trim()))
                {
                    RefNo = Request.QueryString["ref"].Trim();
                    LoadData();
                }
                else
                {
                    Response.Redirect("..");
                }
            }
        }

        protected void btnShowInv_Click(object sender, EventArgs e)
        {
            try
            {
                paymentTransactionsRepository = new PaymentTransactionsRepository();

                var payObj = paymentTransactionsRepository.GetByPaymentId(RefNo);
                if (payObj != null)
                {
                    Response.Redirect("~/Payment/CreditReceipt?ord=" + payObj.UserCredit.TransNo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }
        }

        #endregion
    }
}
