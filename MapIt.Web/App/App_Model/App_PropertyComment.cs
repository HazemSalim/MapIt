using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MapIt.Data;
using MapIt.Repository;
using MapIt.Lib;

namespace MapIt.Web.App.App_Model
{
    public class App_PropertyComment
    {
        public long Id { get; set; }

        public long PropertyId { get; set; }
        public long PropertyUserId { get; set; }

        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderPhone { get; set; }
        public string SenderPhoto { get; set; }

        public long ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverPhoto { get; set; }

        public string TextMessage { get; set; }
        public bool IsRead { get; set; }
        public int UnreadMessageCount { get; set; }

        public String AddedOnDateEN
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("en-US"));
            }
        }

        public String AddedOnDateAR
        {
            get
            {
                return this.AddedOn.ToString("dd/MM/yyyy hh:mm tt", new CultureInfo("ar-KW"));
            }
        }

        [System.Web.Script.Serialization.ScriptIgnore]
        public DateTime AddedOn { get; set; }

        public App_PropertyComment()
        {

        }

        public App_PropertyComment(PropertyComment comment, long propertyId, long receiverId)
        {
            var propertyCommentsRepository = new PropertyCommentsRepository();

            Id = comment.Id;

            PropertyId = comment.PropertyId;
            PropertyUserId = comment.Property.UserId;

            SenderId = comment.SenderId;
            SenderName = !String.IsNullOrEmpty(comment.Sender.FullName) ? comment.Sender.FullName : string.Empty;
            SenderPhone = comment.Sender.Phone;
            SenderPhoto = AppSettings.WebsiteURL + comment.Sender.UserPhoto;

            ReceiverId = comment.ReceiverId;
            ReceiverName = !String.IsNullOrEmpty(comment.Receiver.FullName) ? comment.Receiver.FullName : string.Empty;
            ReceiverPhone = comment.Receiver.Phone;
            ReceiverPhoto = AppSettings.WebsiteURL + comment.Receiver.UserPhoto;

            TextMessage = comment.TextMessage;
            IsRead = comment.IsRead;
            UnreadMessageCount = propertyCommentsRepository.Count(c => !c.IsRead && c.PropertyId == propertyId && c.ReceiverId == receiverId &&
                (receiverId == comment.SenderId ? c.SenderId == comment.ReceiverId : c.SenderId == comment.SenderId));
            AddedOn = comment.AddedOn;
        }

        public App_PropertyComment(PropertyComment comment, long receiverId)
        {
            var propertyCommentsRepository = new PropertyCommentsRepository();

            Id = comment.Id;

            PropertyId = comment.PropertyId;
            PropertyUserId = comment.Property.UserId;

            SenderId = comment.SenderId;
            SenderName = !String.IsNullOrEmpty(comment.Sender.FullName) ? comment.Sender.FullName : string.Empty;
            SenderPhone = comment.Sender.Phone;
            SenderPhoto = AppSettings.WebsiteURL + comment.Sender.UserPhoto;

            ReceiverId = comment.ReceiverId;
            ReceiverName = !String.IsNullOrEmpty(comment.Receiver.FullName) ? comment.Receiver.FullName : string.Empty;
            ReceiverPhone = comment.Receiver.Phone;
            ReceiverPhoto = AppSettings.WebsiteURL + comment.Receiver.UserPhoto;

            TextMessage = comment.TextMessage;
            IsRead = comment.IsRead;
            UnreadMessageCount = propertyCommentsRepository.Count(c => !c.IsRead && c.ReceiverId == receiverId &&
                (receiverId == comment.SenderId ? c.SenderId == comment.ReceiverId : c.SenderId == comment.SenderId));
            AddedOn = comment.AddedOn;
        }
    }
}