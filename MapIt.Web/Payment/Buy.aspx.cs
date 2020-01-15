using System;
using System.Collections.Generic;
using MapIt.Data;
using MapIt.Helpers;
using MapIt.Lib;
using MapIt.Repository;
using MapIt.Web.MyFatoorahServiceReference;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

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

                string token = "8-vxhGm9mL0nrplGPquxHEwQZSJ62TQjWRj4Anh2VvkjUq8hYKcJv5vn8LdchMPk4l0z4l8c2icZXQKIxsqRH8CMHq3EXQsSg_vh5c4jJvzEqB1mU4lii9OgMIro0YwndohRP6PyWywvq5G5s6iScbLJ5ayo3IBbpk0E7PgwRAJK0zmEYAVdQ2zTUfEF4Ds_o2Nw4fKKzqBNuwogWhyTuxvt28iadLijW1xLU181zFG8fAGlLHip5TzALkOoEkLrWbBQ7OLoPfUSmWUysNSNcDtHptpucDn-muBaOx6IVhiexHYvgsExyLO4WP4_GkztY6JsISLG57l-nbvQAc1BF4-q0TjFH9Ol8ynYaCFJx-0nGnXLR_DmG9_-G_HqLGXPki6WmBroRaSrIZT176Uz2re72gmcUCT7NroKLX_gUuzkydYy1ehkUJ4Dr8CjoUOW1IOMmCMlELREl0DgjfXYiNhs8EY9tnczxIXRmmNd-xsIxxh4eHb9cbfAACYATd4spEgNRS9CH0ydZzZ0DxHX8ZXNzoZiAjUkof3vJ4NJ7d0F6q4dCNeJNOc8CV2tem3bW4Z62RN1FbY76S5Yh4-6XOlqDg7B59oCQH79k4od8HOSI4TT4FUyhpPk-3Ea6cQVAHLx22oQft23fyAn5OPmnhPLdudAlNytlUO1mVsm3OpOJ_35"; //token value to be placed here;
                string baseURL = "https://apitest.myfatoorah.com";
                string url = baseURL + "/v2/InitiatePayment";
                HttpClient client = new HttpClient();
                byte[] cred = Encoding.UTF8.GetBytes("Bearer " + token);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var parameters = new Dictionary<string, string> { { "InvoiceAmount", "100" }, { "CurrencyIso", "KWD" } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                HttpResponseMessage messge = client.PostAsync(url, encodedContent).Result;
                if (messge.IsSuccessStatusCode)
                {
                    string result = messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                else
                {
                    string result = messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }



                //####### Execute Payment ######

                //url = baseURL + "/v2/ExecutePayment";
                //client = new HttpClient();
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //string json = "{\"PaymentMethodId\":\"2\",\"CustomerName\": \"Ahmed\",\"DisplayCurrencyIso\": \"KWD\",\"MobileCountryCode\":\"+965\",\"CustomerMobile\": \"92249038\",\"CustomerEmail\": \"aramadan@myfatoorah.com\",\"InvoiceValue\": 100,\"CallBackUrl\": \"https://google.com\",\"ErrorUrl\": \"https://google.com\",\"Language\": \"en\",\"CustomerReference\" :\"ref 1\",\"CustomerCivilId\":12345678,\"UserDefinedField\": \"Custom field\",\"ExpireDate\": \"\",\"CustomerAddress\" :{\"Block\":\"\",\"Street\":\"\",\"HouseBuildingNo\":\"\",\"Address\":\"\",\"AddressInstructions\":\"\"},\"InvoiceItems\": [{\"ItemName\": \"Product 01\",\"Quantity\": 1,\"UnitPrice\": 100}]}";
                //var content = new StringContent(json, Encoding.UTF8, "application/json");
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //messge = client.PostAsync(url, content).Result;
                //if (messge.IsSuccessStatusCode)
                //{
                //    string result = messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //}
                //else
                //{
                //    string result = messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                //}

                //var lst = new List<ProductDC>();

                //// Merchnat
                //var merchantDCObj = new MerchantDC();
                //merchantDCObj.merchant_ReferenceID = creditObj.Id.ToString();
                //merchantDCObj.merchant_username = AppSettings.MyF_merchant_username;
                //merchantDCObj.merchant_password = AppSettings.MyF_merchant_password;
                //merchantDCObj.merchant_code = AppSettings.MyF_merchant_code;
                //merchantDCObj.ReturnURL = AppSettings.MyF_merchant_return_url;
                //merchantDCObj.merchant_error_url = AppSettings.MyF_merchant_error_url;
                //merchantDCObj.udf1 = creditObj.Id.ToString();

                ////Product
                //var productDCObj = new ProductDC();
                //productDCObj.product_name = product;
                //productDCObj.qty = 1;
                //productDCObj.unitPrice = creditObj.Amount;
                //lst.Add(productDCObj);

                ////Customer 
                //var customerDCObj = new CustomerDC();
                //customerDCObj.Name = creditObj.User.FullName;
                //customerDCObj.Email = creditObj.User.Email;
                //customerDCObj.Mobile = creditObj.User.Phone;

                ////payReq
                //var payRequestDCObj = new PayRequestDC();
                //payRequestDCObj.CustomerDC = customerDCObj;
                //payRequestDCObj.lstProductDC = lst.ToArray();
                //payRequestDCObj.MerchantDC = merchantDCObj;

                ////PayGateway
                //PayGatewayServiceSoapClient cli = new PayGatewayServiceSoapClient();

                ////pay Response
                //PayResponseDC payResponseDC = new PayResponseDC();
                //payResponseDC = cli.PaymentRequest(payRequestDCObj);

                //if (payResponseDC.ResponseCode == "0")
                //{
                //    paymentTransactionsRepository = new PaymentTransactionsRepository();
                //    var ptObj = new PaymentTransaction();

                //    ptObj.CreditId = creditObj.Id;
                //    ptObj.PaymentId = payResponseDC.referenceID;
                //    ptObj.Amount = creditObj.Amount;
                //    ptObj.PaidOn = DateTime.Now;
                //    paymentTransactionsRepository.Add(ptObj);

                //    Response.Redirect(payResponseDC.paymentURL, false);
                //}
                //else
                //{
                //    LogHelper.LogPayment(TransNo, payResponseDC.ResponseCode);
                //    Response.Redirect("Error?ref=" + payResponseDC.referenceID);
                //}
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