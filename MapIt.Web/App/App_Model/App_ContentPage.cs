using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_ContentPage
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public bool HasLink { get; set; }
        public string Link { get; set; }
        public string ContentEN { get; set; }
        public string ContentAR { get; set; }

        public App_ContentPage(ContentPage cPage, int pageId)
        {
            Id = cPage.Id;
            TitleEN = cPage.TitleEN;
            TitleAR = cPage.TitleAR;
            HasLink = cPage.HasLink;
            Link = cPage.Link;
            ContentEN = pageId > 0 ? cPage.ContentEN : string.Empty;
            ContentAR = pageId > 0 ? cPage.ContentAR : string.Empty;
        }
    }
}