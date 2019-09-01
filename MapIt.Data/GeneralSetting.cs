//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapIt.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class GeneralSetting
    {
        public int Id { get; set; }
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKW { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Linkedin { get; set; }
        public string Snapchat { get; set; }
        public string Tumblr { get; set; }
        public string AppStore { get; set; }
        public string GooglePlay { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int DefaultCountryId { get; set; }
        public int DefaultCurrencyId { get; set; }
        public string InvoiceTerms { get; set; }
        public string WorkingHours { get; set; }
        public double DefFreeAmount { get; set; }
        public double NormalAdCost { get; set; }
        public double SpecAdCost { get; set; }
        public double AdVideoCost { get; set; }
        public int NormalAdDuration { get; set; }
        public int SpecAdDuration { get; set; }
        public int AvPhotos { get; set; }
        public int AvVideos { get; set; }
        public int SimilarAdCount { get; set; }
        public int PageSize { get; set; }
        public int PageSizeMob { get; set; }
        public bool AutoActiveUser { get; set; }
        public bool AutoActiveAd { get; set; }
        public double Version { get; set; }
        public System.DateTime AddedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> AdminUserId { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Currency DefaultCurrency { get; set; }
    }
}
