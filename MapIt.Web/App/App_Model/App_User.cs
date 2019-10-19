using System;
using System.Globalization;
using System.Linq;
using MapIt.Data;
using MapIt.Lib;
using MapIt.Repository;

namespace MapIt.Web.App.App_Model
{
    public class App_User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public int Sex { get; set; }

        public String BirthDateDate
        {
            get
            {
                if (BirthDate.HasValue)
                {
                    return this.BirthDate.Value.ToString("dd/MM/yyyy");
                }
                return string.Empty;
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime? BirthDate { get; set; }

        public int CountryId { get; set; }
        public string CountryEN { get; set; }
        public string CountryAR { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string ActivationCode { get; set; }

        public string OtherPhones { get; set; }

        public string FreeCreditAmount { get; set; }
        public string ActualCreditAmount { get; set; }

        public string Photo { get; set; }

        public string Lang { get; set; }

        public int? UserTypeID { get; set; }

        public bool IsActive { get; set; }
        public bool? IsVerified { get; set; }

        public int WatchListNotifs { get; set; }
        public int GeneralNotifs { get; set; }

        public int UnreadCommentsCount { get; set; }
        public int UnreadTechMessagesCount { get; set; }

        public string LastProCommentText { get; set; }

        public bool LastProCommentRead { get; set; }

        public String LastProCommentOnEN { get; set; }

        public String LastProCommentOnAR { get; set; }

        public string LastSrvCommentText { get; set; }

        public bool LastSrvCommentRead { get; set; }

        public String LastSrvCommentOnEN { get; set; }

        public String LastSrvCommentOnAR { get; set; }

        public String AddedOnDate
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt");
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_User(User user)
        {
            Id = user.Id;
            FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : string.Empty;
            LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : string.Empty;
            FullName = !string.IsNullOrEmpty(user.FullName) ? user.FullName : string.Empty;
            Sex = user.Sex ?? 0;
            BirthDate = user.BirthDate;
            IsActive = user.IsActive;
            IsVerified = user.IsVerified;
            CountryId = user.CountryId;
            UserTypeID = user.UserTypeID.HasValue && user.UserTypeID.Value > 0 ?
                user.UserTypeID.Value : 0;

            CountryEN = user.Country.TitleEN;
            CountryAR = user.Country.TitleAR;
            Phone = user.Phone;
            Email = !string.IsNullOrEmpty(user.Email) ? user.Email : string.Empty;
            UserName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : string.Empty;
            ActivationCode = user.ActivationCode;
            OtherPhones = user.OtherPhones;
            FreeCreditAmount = user.AvFreeCredit.ToString();
            ActualCreditAmount = user.AvActualCredit.ToString();
            Photo = AppSettings.WebsiteURL + user.UserPhoto;
            WatchListNotifs = user.Notifications.Where(n => n.TypeId == (int)AppEnums.NotifTypes.Alert).Count();
            GeneralNotifs = user.Notifications.Where(n => n.TypeId == (int)AppEnums.NotifTypes.General).Count();
            UnreadCommentsCount = (user.ReceiverPropertyComments.Count(c => !c.IsRead)) + (user.ReceiverServiceComments.Count(c => !c.IsRead));
            UnreadTechMessagesCount = user.TechMessages.Count(c => !c.IsRead);
            AddedOn = user.AddedOn;
            Lang = user.Lang;

            LastProCommentText = string.Empty;
            LastProCommentRead = false;
            LastProCommentOnEN = string.Empty;
            LastProCommentOnAR = string.Empty;

            LastSrvCommentText = string.Empty;
            LastSrvCommentRead = false;
            LastSrvCommentOnEN = string.Empty;
            LastSrvCommentOnAR = string.Empty;
        }

        public App_User(User user, long propertyId, long serviceId)
        {
            Id = user.Id;
            FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : string.Empty;
            LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : string.Empty;
            FullName = !string.IsNullOrEmpty(user.FullName) ? user.FullName : string.Empty;
            Sex = user.Sex.HasValue ? user.Sex.Value : 0;
            BirthDate = user.BirthDate;
            IsActive = user.IsActive;
            IsVerified = user.IsVerified;
            CountryId = user.CountryId;
            UserTypeID = user.UserTypeID.HasValue && user.UserTypeID.Value > 0 ?
                user.UserTypeID.Value : 0;
            CountryEN = user.Country.TitleEN;
            CountryAR = user.Country.TitleAR;
            Phone = user.Phone;
            Lang = user.Lang;
            Email = !string.IsNullOrEmpty(user.Email) ? user.Email : string.Empty;
            UserName = !string.IsNullOrEmpty(user.UserName) ? user.UserName : string.Empty;
            OtherPhones = user.OtherPhones;
            FreeCreditAmount = user.AvFreeCredit.ToString();
            ActualCreditAmount = user.AvActualCredit.ToString();
            Photo = AppSettings.WebsiteURL + user.UserPhoto;
            WatchListNotifs = user.Notifications.Where(n => n.TypeId == (int)AppEnums.NotifTypes.Alert).Count();
            GeneralNotifs = user.Notifications.Where(n => n.TypeId == (int)AppEnums.NotifTypes.General).Count();
            UnreadCommentsCount = (user.ReceiverPropertyComments.Count(c => !c.IsRead)) + (user.ReceiverServiceComments.Count(c => !c.IsRead));
            UnreadTechMessagesCount = user.TechMessages.Count(c => !c.IsRead);

            AddedOn = user.AddedOn;

            var proCommentsRepository = new PropertyCommentsRepository();
            var lastProCommentList = proCommentsRepository.Find(pc => pc.PropertyId == propertyId && (pc.SenderId == user.Id || pc.ReceiverId == user.Id));
            if (lastProCommentList.Count() > 0)
            {
                var lastProComment = lastProCommentList.OrderByDescending(pc => pc.Id).FirstOrDefault();

                LastProCommentText = lastProComment != null ? lastProComment.TextMessage : string.Empty;
                LastProCommentRead = lastProComment != null ? lastProComment.IsRead : false;
                LastProCommentOnEN = lastProComment != null ? lastProComment.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")) : string.Empty;
                LastProCommentOnAR = lastProComment != null ? lastProComment.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("ar-KW")) : string.Empty;
            }
            else
            {
                LastProCommentText = string.Empty;
                LastProCommentRead = false;
                LastProCommentOnEN = string.Empty;
                LastProCommentOnAR = string.Empty;
            }

            var srvCommentsRepository = new ServiceCommentsRepository();
            var lastSrvCommentList = srvCommentsRepository.Find(sc => sc.ServiceId == serviceId && (sc.SenderId == user.Id || sc.ReceiverId == user.Id));
            if (lastSrvCommentList.Count() > 0)
            {
                var lastSrvComment = lastSrvCommentList.OrderByDescending(sc => sc.Id).FirstOrDefault();

                LastSrvCommentText = lastSrvComment != null ? lastSrvComment.TextMessage : string.Empty;
                LastSrvCommentRead = lastSrvComment != null ? lastSrvComment.IsRead : false;
                LastSrvCommentOnEN = lastSrvComment != null ? lastSrvComment.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US")) : string.Empty;
                LastSrvCommentOnAR = lastSrvComment != null ? lastSrvComment.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("ar-KW")) : string.Empty;
            }
            else
            {
                LastSrvCommentText = string.Empty;
                LastSrvCommentRead = false;
                LastSrvCommentOnEN = string.Empty;
                LastSrvCommentOnAR = string.Empty;
            }
        }
    }
}