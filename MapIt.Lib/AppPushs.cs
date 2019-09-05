﻿using System;
using System.Linq;
using MapIt.Data;
using MapIt.Repository;
using MapIt.Helpers;

namespace MapIt.Lib
{
    public class AppPushs
    {
        #region Methods

        public static void Push(int typeId, long? userId, int? gNotifId, long? propertyId, long? serviceId, int? offerId, string msgEN, string msgAR)
        {
            try
            {
                var notifTypesRepository = new NotifTypesRepository();
                var nTypeObj = notifTypesRepository.GetByKey(typeId);

                if (nTypeObj == null)
                {
                    return;
                }

                var notificationsRepository = new NotificationsRepository();
                var notifObj = new Notification();

                notifObj.UserId = userId;
                notifObj.GenNotifId = gNotifId;
                notifObj.PropertyId = propertyId;
                notifObj.ServiceId = serviceId;
                notifObj.OfferId = offerId;
                notifObj.TypeId = nTypeObj.Id;
                notifObj.TitleEN = msgEN;
                notifObj.TitleAR = msgAR;
                notifObj.IsRead = false;
                notifObj.AddedOn = DateTime.Now;

                notificationsRepository.Add(notifObj);

                if (notifObj == null)
                {
                    return;
                }

                notificationsRepository = new NotificationsRepository();
                notifObj = notificationsRepository.GetByKey(notifObj.Id);


                #region Get message by Language
                string message = notifObj.TitleAR;

                if (userId.HasValue && userId > 0)
                {
                    UsersRepository usersRepository = new UsersRepository();
                    var usr = usersRepository.First(c => c.Id == userId);
                    if (usr != null && usr.Lang == "en")
                        message = notifObj.TitleEN;
                }
                #endregion

                if (notifObj.UserId.HasValue)
                {
                    if (notifObj.OfferId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.OfferId.Value.ToString(), nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                    else if (notifObj.PropertyId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.PropertyId.Value.ToString(), nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                    else if (notifObj.ServiceId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.ServiceId.Value.ToString(), nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                    else if (notifObj.GenNotifId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), string.Empty, nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                    else
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), string.Empty, nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                }
                else
                {
                    if (notifObj.OfferId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.OfferId.Value.ToString(), nTypeObj.Title.ToString(), message);
                    }
                    else if (notifObj.PropertyId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.PropertyId.Value.ToString(), nTypeObj.Title.ToString(), message);
                    }
                    else if (notifObj.ServiceId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), notifObj.ServiceId.Value.ToString(), nTypeObj.Title.ToString(), notifObj.User.DevicesTokens.ToList(), message);
                    }
                    else if (notifObj.GenNotifId.HasValue)
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), string.Empty, nTypeObj.Title.ToString(), message);
                    }
                    else
                    {
                        SendPushNotification.Send(notifObj.Id.ToString(), string.Empty, nTypeObj.Title.ToString(), message);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return;
            }
        }

        #endregion
    }
}