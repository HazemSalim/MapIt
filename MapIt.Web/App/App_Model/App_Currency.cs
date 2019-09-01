using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_Currency
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string SymbolEN { get; set; }
        public string SymbolAR { get; set; }

        public App_Currency(Currency currency)
        {
            Id = currency.Id;
            TitleEN = currency.TitleEN;
            TitleAR = currency.TitleAR;
            SymbolEN = currency.SymbolEN;
            SymbolAR = currency.SymbolAR;
        }
    }
}