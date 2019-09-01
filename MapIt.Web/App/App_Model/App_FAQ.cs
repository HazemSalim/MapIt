using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_FAQ
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string ContentEN { get; set; }
        public string ContentAR { get; set; }

        public App_FAQ(FAQ faq)
        {
            Id = faq.Id;
            TitleEN = faq.TitleEN;
            TitleAR = faq.TitleAR;
            ContentEN = faq.ContentEN;
            ContentAR = faq.ContentAR;
        }
    }
}