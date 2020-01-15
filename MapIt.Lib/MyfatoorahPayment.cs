using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapIt.Lib
{
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


    public class PaymentData
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

    public class PaymentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public PaymentData Data { get; set; }
    }




}
