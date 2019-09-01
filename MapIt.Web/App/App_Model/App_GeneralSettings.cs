using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapIt.Data;

namespace MapIt.Web.App.App_Model
{
    public class App_GeneralSettings
    {
        public string TitleEN { get; set; }
        public string TitleAR { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string YouTube { get; set; }
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
        public string WorkingHours { get; set; }
        public double NormalAdCost { get; set; }
        public double SpecAdCost { get; set; }
        public double AdVideoCost { get; set; }
        public int NormalAdDuration { get; set; }
        public int SpecAdDuration { get; set; }
        public int AvPhotos { get; set; }
        public int AvVideos { get; set; }
        public double Version { get; set; }
        public bool AutoActiveUser { get; set; }

        public App_GeneralSettings(GeneralSetting gSetting)
        {
            TitleEN = gSetting.TitleEN;
            TitleAR = gSetting.TitleAR;
            Website = !string.IsNullOrEmpty(gSetting.Website) ? gSetting.Website : string.Empty;
            Facebook = !string.IsNullOrEmpty(gSetting.Facebook) ? gSetting.Facebook : string.Empty;
            Twitter = !string.IsNullOrEmpty(gSetting.Twitter) ? gSetting.Twitter : string.Empty;
            Instagram = !string.IsNullOrEmpty(gSetting.Instagram) ? gSetting.Instagram : string.Empty;
            YouTube = !string.IsNullOrEmpty(gSetting.Youtube) ? gSetting.Youtube : string.Empty;
            Linkedin = !string.IsNullOrEmpty(gSetting.Linkedin) ? gSetting.Linkedin : string.Empty;
            Snapchat = !string.IsNullOrEmpty(gSetting.Snapchat) ? gSetting.Snapchat : string.Empty;
            Tumblr = !string.IsNullOrEmpty(gSetting.Tumblr) ? gSetting.Tumblr : string.Empty;
            AppStore = !string.IsNullOrEmpty(gSetting.AppStore) ? gSetting.AppStore : string.Empty;
            GooglePlay = !string.IsNullOrEmpty(gSetting.GooglePlay) ? gSetting.GooglePlay : string.Empty;
            Longitude = !string.IsNullOrEmpty(gSetting.Longitude) ? gSetting.Longitude : string.Empty;
            Latitude = !string.IsNullOrEmpty(gSetting.Latitude) ? gSetting.Latitude : string.Empty;
            Address = !string.IsNullOrEmpty(gSetting.Address) ? gSetting.Address : string.Empty;
            Email = !string.IsNullOrEmpty(gSetting.Email) ? gSetting.Email : string.Empty;
            Phone = !string.IsNullOrEmpty(gSetting.Phone) ? gSetting.Phone : string.Empty;
            Fax = !string.IsNullOrEmpty(gSetting.Fax) ? gSetting.Fax : string.Empty;
            WorkingHours = !string.IsNullOrEmpty(gSetting.WorkingHours) ? gSetting.WorkingHours : string.Empty;
            NormalAdCost = gSetting.NormalAdCost;
            SpecAdCost = gSetting.SpecAdCost;
            AdVideoCost = gSetting.AdVideoCost;
            NormalAdDuration = gSetting.NormalAdDuration;
            SpecAdDuration = gSetting.SpecAdDuration;
            AvPhotos = gSetting.AvPhotos;
            AvVideos = gSetting.AvVideos;
            Version = gSetting.Version;
            AutoActiveUser = gSetting.AutoActiveUser;
        }
    }
}