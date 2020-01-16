using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MapIt.Lib
{
    public class MyfatoorahPayment
    {
        public  static string PostRequest(string apiName,string jsonBody)
        {
            string url = AppSettings.MyF_merchant_url + apiName;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppSettings.MyF_merchant_token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var messge = client.PostAsync(url, content).Result;
            string result = messge.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return result;
        }
        
    }


    public class CustomerAddress
    {
        public string Block { get; set; }
        public string Street { get; set; }
        public string HouseBuildingNo { get; set; }
        public string Address { get; set; }
        public string AddressInstructions { get; set; }
    }

    public class InvoiceItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    public class PaymentRequest
    {
        public string PaymentMethodId { get; set; }
        public string CustomerName { get; set; }
        public string DisplayCurrencyIso { get; set; }
        public string MobileCountryCode { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public double InvoiceValue { get; set; }
        public string CallBackUrl { get; set; }
        public string ErrorUrl { get; set; }
        public string Language { get; set; }
        public string CustomerReference { get; set; }
        public int CustomerCivilId { get; set; }
        public string UserDefinedField { get; set; }
        public string ExpireDate { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }


    public class ExecutePaymentData
    {
        public int InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
    }

    public class ValidationError
    {
        public string Name { get; set; }
        public string Error { get; set; }
    }

    

    public class InvoiceTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string PaymentGateway { get; set; }
        public string ReferenceId { get; set; }
        public string TrackId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentId { get; set; }
        public string AuthorizationId { get; set; }
        public string TransactionStatus { get; set; }
        public string TransationValue { get; set; }
        public string CustomerServiceCharge { get; set; }
        public string DueValue { get; set; }
        public string PaidCurrency { get; set; }
        public string PaidCurrencyValue { get; set; }
        public string Currency { get; set; }
        public string Error { get; set; }
        public string CardNumber { get; set; }
    }

    public class EnquiryRequest
    {
        public string Key { get; set; }
        public string KeyType { get; set; }
    }

        public class EnquiryPaymentData
    {
        public int InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public int InvoiceValue { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string UserDefinedField { get; set; }
        public string InvoiceDisplayValue { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
        public List<InvoiceTransaction> InvoiceTransactions { get; set; }
    }

    public class PaymentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public object Data { get; set; }
    }


}
