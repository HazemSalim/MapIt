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
using Newtonsoft.Json;
using System.Web;
using System.Linq;

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

                //string token = "8-vxhGm9mL0nrplGPquxHEwQZSJ62TQjWRj4Anh2VvkjUq8hYKcJv5vn8LdchMPk4l0z4l8c2icZXQKIxsqRH8CMHq3EXQsSg_vh5c4jJvzEqB1mU4lii9OgMIro0YwndohRP6PyWywvq5G5s6iScbLJ5ayo3IBbpk0E7PgwRAJK0zmEYAVdQ2zTUfEF4Ds_o2Nw4fKKzqBNuwogWhyTuxvt28iadLijW1xLU181zFG8fAGlLHip5TzALkOoEkLrWbBQ7OLoPfUSmWUysNSNcDtHptpucDn-muBaOx6IVhiexHYvgsExyLO4WP4_GkztY6JsISLG57l-nbvQAc1BF4-q0TjFH9Ol8ynYaCFJx-0nGnXLR_DmG9_-G_HqLGXPki6WmBroRaSrIZT176Uz2re72gmcUCT7NroKLX_gUuzkydYy1ehkUJ4Dr8CjoUOW1IOMmCMlELREl0DgjfXYiNhs8EY9tnczxIXRmmNd-xsIxxh4eHb9cbfAACYATd4spEgNRS9CH0ydZzZ0DxHX8ZXNzoZiAjUkof3vJ4NJ7d0F6q4dCNeJNOc8CV2tem3bW4Z62RN1FbY76S5Yh4-6XOlqDg7B59oCQH79k4od8HOSI4TT4FUyhpPk-3Ea6cQVAHLx22oQft23fyAn5OPmnhPLdudAlNytlUO1mVsm3OpOJ_35"; //token value to be placed here;
                //string baseURL = "https://apitest.myfatoorah.com";

                string baseURL = "https://apitest.myfatoorah.com";
                string token = "7Fs7eBv21F5xAocdPvvJ-sCqEyNHq4cygJrQUFvFiWEexBUPs4AkeLQxH4pzsUrY3Rays7GVA6SojFCz2DMLXSJVqk8NG-plK-cZJetwWjgwLPub_9tQQohWLgJ0q2invJ5C5Imt2ket_-JAlBYLLcnqp_WmOfZkBEWuURsBVirpNQecvpedgeCx4VaFae4qWDI_uKRV1829KCBEH84u6LYUxh8W_BYqkzXJYt99OlHTXHegd91PLT-tawBwuIly46nwbAs5Nt7HFOozxkyPp8BW9URlQW1fE4R_40BXzEuVkzK3WAOdpR92IkV94K_rDZCPltGSvWXtqJbnCpUB6iUIn1V-Ki15FAwh_nsfSmt_NQZ3rQuvyQ9B3yLCQ1ZO_MGSYDYVO26dyXbElspKxQwuNRot9hi3FIbXylV3iN40-nCPH4YQzKjo5p_fuaKhvRh7H8oFjRXtPtLQQUIDxk-jMbOp7gXIsdz02DrCfQIihT4evZuWA6YShl6g8fnAqCy8qRBf_eLDnA9w-nBh4Bq53b1kdhnExz0CMyUjQ43UO3uhMkBomJTXbmfAAHP8dZZao6W8a34OktNQmPTbOHXrtxf6DS-oKOu3l79uX_ihbL8ELT40VjIW3MJeZ_-auCPOjpE3Ax4dzUkSDLCljitmzMagH2X8jN8-AYLl46KcfkBV";



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


                string url = baseURL + "/v2/ExecutePayment";
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(paymentRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var messge = client.PostAsync(url, content).Result;
                string result =  messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var paymentResult = JsonConvert.DeserializeObject<PaymentResponse>(result);

                paymentTransactionsRepository = new PaymentTransactionsRepository();
                var ptObj = new PaymentTransaction
                {
                    CreditId = creditObj.Id,
                    Amount = creditObj.Amount,
                    PaymentId = TransNo,
                PaidOn = DateTime.Now
                };

                paymentTransactionsRepository.Add(ptObj);

                if (paymentResult.IsSuccess)
                {
                    Response.Redirect(paymentResult.Data.PaymentURL, false);
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