using System;
using System.Collections.Generic;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MapIt.Web.Payment
{
    public partial class Buy : BasePage
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

                var phoneParts = creditObj.User.Phone.Split(' ');
                string currCode = "+965";
                string mobile = "00000000";
                if (phoneParts.Length > 1)
                {
                    currCode = phoneParts[0];
                    mobile = phoneParts[1];
                }



                PaymentRequest paymentRequest = new PaymentRequest
                {
                    PaymentMethodId = "1",
                    CustomerName = creditObj.User.FullName,
                    DisplayCurrencyIso = "KWD",
                    MobileCountryCode = currCode,
                    CustomerMobile = mobile,
                    CustomerEmail = creditObj.User.Email,
                    InvoiceValue = creditObj.Amount,
                    CallBackUrl = AppSettings.MyF_merchant_return_url,
                    ErrorUrl = AppSettings.MyF_merchant_error_url,
                    Language = "en",
                    CustomerReference = "",
                    CustomerCivilId = 0,
                    UserDefinedField = "Custom field",
                    ExpireDate = "",
                    CustomerAddress = new CustomerAddress
                    {
                        Block = "",
                        Street = "",
                        HouseBuildingNo = "",
                        Address = "",
                        AddressInstructions = ""
                    },
                    InvoiceItems = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        ItemName = product,
                        Quantity = 1,
                        UnitPrice = creditObj.Amount
                    }
                }
                };


                string json = JsonConvert.SerializeObject(paymentRequest);
                string result = MyfatoorahPayment.PostRequest("ExecutePayment", json);

                var paymentResult = JsonConvert.DeserializeObject<PaymentResponse>(result);

                JObject tmpObj = paymentResult.Data as JObject;
                ExecutePaymentData data = tmpObj.ToObject<ExecutePaymentData>();

                paymentTransactionsRepository = new PaymentTransactionsRepository();
                var ptObj = new PaymentTransaction
                {
                    CreditId = creditObj.Id,
                    Amount = creditObj.Amount,
                    RefId = data.InvoiceId.ToString(),
                    PaidOn = DateTime.Now
                };

                paymentTransactionsRepository.Add(ptObj);

                if (paymentResult.IsSuccess)
                {
                    Response.Redirect(data.PaymentURL, false);
                }
                else
                {
                    string error = "";
                    foreach (var err in paymentResult.ValidationErrors)
                    {
                        error += "Name:" + err.Name;
                        error += "Error:" + err.Error;
                    }
                    LogHelper.LogPayment(TransNo, error);

                    Response.Redirect("Error?ref=" + TransNo);
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