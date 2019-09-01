using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using MapIt.Web.MyFatoorahServiceReference;

namespace MapIt.Web.Payment
{
    public partial class Response : MapIt.Lib.BasePage
    {
        #region Variables

        PaymentTransactionsRepository paymentTransactionsRepository;
        UserCreditsRepository userCreditsRepository;
        UserBalanceLogsRepository userBalanceLogsRepository;

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

        private void GetResponse()
        {
            try
            {
                //PayGateway
                PayGatewayServiceSoapClient cli = new PayGatewayServiceSoapClient();

                // Get Order Status
                var getOrderStatusRequestDCObj = new GetOrderStatusRequestDC();
                getOrderStatusRequestDCObj.merchant_code = ConfigurationManager.AppSettings["merchant_code"];
                getOrderStatusRequestDCObj.merchant_password = ConfigurationManager.AppSettings["merchant_password"];
                getOrderStatusRequestDCObj.merchant_username = ConfigurationManager.AppSettings["merchant_username"];
                getOrderStatusRequestDCObj.referenceID = RefNo;

                OrderStatusResponseDC ResponseSattus = new OrderStatusResponseDC();
                ResponseSattus = cli.GetOrderStatusRequest(getOrderStatusRequestDCObj);

                paymentTransactionsRepository = new PaymentTransactionsRepository();
                var ptObj = paymentTransactionsRepository.GetByPaymentId(RefNo);

                if (ptObj != null)
                {
                    ptObj.RefId = ResponseSattus.RefID;
                    ptObj.Result = ResponseSattus.result;
                    paymentTransactionsRepository.Update(ptObj);
                }

                if (ResponseSattus.result == "CAPTURED")
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

                    Response.Redirect("Thanks?ref=" + RefNo);
                }
                else
                {
                    Response.Redirect("Error?ref=" + RefNo);
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
                if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"].Trim()))
                {
                    RefNo = Request.QueryString["id"].Trim();
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