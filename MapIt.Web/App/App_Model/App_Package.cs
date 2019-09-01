using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_Package
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string DescriptionEN { get; set; }
        public string DescriptionAR { get; set; }

        public int CurrencyId { get; set; }
        public int CurrencyCode { get; set; }
        public string CurrencyEN { get; set; }
        public string CurrencyAR { get; set; }
        public string SymbolEN { get; set; }
        public string SymbolAR { get; set; }
        public string Price { get; set; }

        public App_Package(Package package, Currency currency)
        {
            Id = package.Id;
            TitleEN = package.TitleEN;
            TitleAR = package.TitleAR;
            DescriptionEN = package.DescriptionEN;
            DescriptionAR = package.DescriptionAR;
            CurrencyId = currency.Id;
            CurrencyCode = currency.Code;
            CurrencyEN = currency.TitleEN;
            CurrencyAR = currency.TitleAR;
            SymbolEN = currency.SymbolEN;
            SymbolAR = currency.SymbolAR;
            Price = Math.Round(package.Price * currency.ExchangeRate, currency.Digits, MidpointRounding.ToEven).ToString(currency.Format);
        }
    }
}
