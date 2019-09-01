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
    public partial class Buy : MapIt.Lib.BasePage
    {
        #region Variables

        PaymentTransactionsRepository paymentTransactionsRepository;
        UserCreditsRepository userCreditsRepository;

        #endregion

        #region Properties

        public string TransNo
        {
            get
            {
                if (ViewState["TransNo"] != null && !string.IsNullOrEmpty(ViewState["TransNo"].ToString().Trim()))
                    return ViewState["TransNo"].ToString().Trim();

                return string.Empty;
            }
            set
            {
                ViewState["TransNo"] = value;
            }
        }

        #endregion

        #region Methods

        void SendRequest()
        {
            try
            {
                userCreditsRepository = new UserCreditsRepository();
                var creditObj = userCreditsRepository.First(o => o.TransNo == TransNo);
                if (creditObj == null)
                {
                    Response.Redirect("..");
                }

                //if (UserId <= 0 || creditObj.UserId != UserId)
                //{
                //    Response.Redirect("..");
                //}

                string product = "Buy from MapIt Co. :: Credit";
                var lst = new List<ProductDC>();

                // Merchnat
                var merchantDCObj = new MerchantDC();
                merchantDCObj.merchant_ReferenceID = creditObj.Id.ToString();
                merchantDCObj.merchant_username = AppSettings.MyF_merchant_username;
                merchantDCObj.merchant_password = AppSettings.MyF_merchant_password;
                merchantDCObj.merchant_code = AppSettings.MyF_merchant_code;
                merchantDCObj.ReturnURL = AppSettings.MyF_merchant_return_url;
                merchantDCObj.merchant_error_url = AppSettings.MyF_merchant_error_url;
                merchantDCObj.udf1 = creditObj.Id.ToString();

                //Product
                var productDCObj = new ProductDC();
                productDCObj.product_name = product;
                productDCObj.qty = 1;
                productDCObj.unitPrice = creditObj.Amount;
                lst.Add(productDCObj);

                //Customer 
                var customerDCObj = new CustomerDC();
                customerDCObj.Name = creditObj.User.FullName;
                customerDCObj.Email = creditObj.User.Email;
                customerDCObj.Mobile = creditObj.User.Phone;

                //payReq
                var payRequestDCObj = new PayRequestDC();
                payRequestDCObj.CustomerDC = customerDCObj;
                payRequestDCObj.lstProductDC = lst.ToArray();
                payRequestDCObj.MerchantDC = merchantDCObj;

                //PayGateway
                PayGatewayServiceSoapClient cli = new PayGatewayServiceSoapClient();

                //pay Response
                PayResponseDC payResponseDC = new PayResponseDC();
                payResponseDC = cli.PaymentRequest(payRequestDCObj);

                if (payResponseDC.ResponseCode == "0")
                {
                    paymentTransactionsRepository = new PaymentTransactionsRepository();
                    var ptObj = new PaymentTransaction();

                    ptObj.CreditId = creditObj.Id;
                    ptObj.PaymentId = payResponseDC.referenceID;
                    ptObj.Amount = creditObj.Amount;
                    ptObj.PaidOn = DateTime.Now;
                    paymentTransactionsRepository.Add(ptObj);

                    Response.Redirect(payResponseDC.paymentURL, false);
                }
                else
                {
                    LogHelper.LogPayment(TransNo, payResponseDC.ResponseCode);
                    Response.Redirect("Error?ref=" + payResponseDC.referenceID);
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
                if (Request.QueryString["trn"] != null && !string.IsNullOrEmpty(Request.QueryString["trn"].Trim()))
                {
                    TransNo = Request.QueryString["trn"].Trim();
                    SendRequest();
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