using System;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace MapIt.Web.Payment
{
    public partial class Response : BasePage
    {
        #region Variables

        PaymentTransactionsRepository paymentTransactionsRepository;
        UserCreditsRepository userCreditsRepository;
        UserBalanceLogsRepository userBalanceLogsRepository;

        #endregion

        #region Properties

        public string PaymentId
        {
            get
            {
                if (ViewState["PaymentId"] != null && !string.IsNullOrEmpty(ViewState["PaymentId"].ToString().Trim()))
                    return ViewState["PaymentId"].ToString().Trim();

                return string.Empty;
            }
            set
            {
                ViewState["PaymentId"] = value;
            }
        }

        #endregion

        #region Methods

        private void GetResponse()
        {
            string redirectTo = "";
            try
            {
                EnquiryRequest enquiryRequest = new EnquiryRequest
                {
                    KeyType = "PaymentId",
                    Key = PaymentId
                };

                string json = JsonConvert.SerializeObject(enquiryRequest);
                string result = MyfatoorahPayment.PostRequest("GetPaymentStatus", json);

                var enquiryPaymentResult = JsonConvert.DeserializeObject<PaymentResponse>(result);

                JObject tmpObj = enquiryPaymentResult.Data as JObject;
                EnquiryPaymentData data = tmpObj.ToObject<EnquiryPaymentData>();


                paymentTransactionsRepository = new PaymentTransactionsRepository();
                var ptObj = paymentTransactionsRepository.GetByInvoiceId(data.InvoiceId.ToString());

                if (ptObj != null)
                {
                    ptObj.PaymentId = PaymentId;
                    ptObj.TranId = data.InvoiceTransactions.Count > 0 ? data.InvoiceTransactions[0].TransactionId : "";
                    ptObj.Result = data.InvoiceStatus;
                    ptObj.PaymentMethod = "2"; //Myfatoorah not Free (1)
                    paymentTransactionsRepository.Update(ptObj);
                }

                if (data.InvoiceStatus == "Paid")
                {
                    userCreditsRepository = new UserCreditsRepository();
                    var creditObj = userCreditsRepository.GetByKey(ptObj.CreditId);
                    if (creditObj != null)
                    {
                        creditObj.PaymentStatus = (int)AppEnums.PaymentStatus.Paid;
                        userCreditsRepository.Update(creditObj);
                    }

                    // balance adding
                    if (creditObj.Amount > 0)
                    {
                        userBalanceLogsRepository = new UserBalanceLogsRepository();

                        var userBalanceObj = new UserBalanceLog();
                        userBalanceObj.LogNo = string.Empty;
                        userBalanceObj.UserId = creditObj.UserId;
                        userBalanceObj.Amount = creditObj.Amount;
                        userBalanceObj.TransType = AppConstants.BalanceTransTypes.Credit;
                        userBalanceObj.TransOn = DateTime.Now;

                        userBalanceLogsRepository.Add(userBalanceObj);
                        userBalanceObj.LogNo = "LOG" + (userBalanceObj.Id).ToString("D6");
                        userBalanceLogsRepository.Update(userBalanceObj);
                    }

                    redirectTo = "~/Payment/Thanks.aspx?ref=" + PaymentId;
                    // end response

                }
                else
                {
                    redirectTo = "~/Payment/Error.aspx?paymentId=" + PaymentId;
                }
            }
            catch (ThreadAbortException)
            {
                // Do nothing. ASP.NET is redirecting.
                // Always comment this so other developers know why the exception 
                // is being swallowed.
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
            }

            Response.Redirect(redirectTo, false);
            Context.ApplicationInstance.CompleteRequest();

        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"].Trim()))
                {
                    PaymentId = Request.QueryString["id"].Trim();
                    GetResponse();
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