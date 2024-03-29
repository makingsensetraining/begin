﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.ManagerServices
{
    public class NotificationManager
    {
        private static readonly string BaseAddress = ConfigBaseAddress.BaseAddress;
        private static readonly string SubAddress = ConfigBaseAddress.SubAddress;
        private static readonly string Identifier = ConfigBaseAddress.IdentifierNotifications;

        private readonly GenericBaseClient<ProfileNotification> _notificationClient =
            new GenericBaseClient<ProfileNotification>(BaseAddress, SubAddress);
        private static readonly string ThisClassName = typeof(NotificationManager).Name;

        public async Task<ProfileNotification> GetProfileNotificationByParams(
            string authToken,
            string limit = null,
            string status = null
            )
        {
            try
            {
                var urlGetParams = "?limit=" + limit + "&status=" + status;
                return await _notificationClient.GetAsync(authToken, Identifier, urlGetParams);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetProfileNotificationByParams", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileNotification> MarkAsRead(string authToken, string notificationId = null)
        {
            try
            {
                var notId = notificationId ?? "";
                string addressSuffix = Identifier + "/mark_as_read/" + notId;
                return await _notificationClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileNotification()
                {
                    Error = exception.Message
                };

                AppContextError.Send(ThisClassName, "MarkAsRead", exception, error, ExceptionLevel.Application);
                return error;
            }
        }

        public async Task<ProfileNotification> MarkAsUnread(string authToken, string notificationId = null)
        {
            try
            {
                var notId = notificationId ?? "";
                string addressSuffix = Identifier + "/mark_as_unread/" + notId;
                return await _notificationClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileNotification()
                {
                    Error = exception.Message
                };

                AppContextError.Send(ThisClassName, "MarkAsUnread", exception, error, ExceptionLevel.Application);
                return error;
            }
        }
    }
}
