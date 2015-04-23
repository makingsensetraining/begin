﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.ManagerServices
{
    public class MessageManager
    {
        private const string BaseAddress = "http://186.109.86.251:5432/";
        private const string SubAddress = "begin/api/v1/";
        private const string Identifier = "messages";

        private readonly GenericBaseClient<ProfileThreadMessages> _profileThreadMessagesClient =
            new GenericBaseClient<ProfileThreadMessages>(BaseAddress, SubAddress);

        private readonly GenericBaseClient<Message> _threadMessagesClient =
            new GenericBaseClient<Message>(BaseAddress, SubAddress);

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesInbox(
            string authToken,
            string q = null,
            string limit = null)
        {
            try
            {
                const string addressSuffix = Identifier + "/inbox";
                var urlGetParams = "?q=" + q + "&limit=" + limit;

                return await _profileThreadMessagesClient.GetAsync(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                var profileThreadMessages = new ProfileThreadMessages()
                {
                    Error = exception.Message
                };

                AppContextError.Send(exception, profileThreadMessages, ExceptionLevel.Application);
                return profileThreadMessages;
            }
        }

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesSent(
            string authToken,
            string q = null,
            string limit = null)
        {
            try
            {
                const string addressSuffix = Identifier + "/sentbox";
                var urlGetParams = "?q=" + q + "&limit=" + limit;

                return await _profileThreadMessagesClient.GetAsync(authToken, addressSuffix, urlGetParams);
            }
            catch (Exception exception)
            {
                var profileThreadMessages = new ProfileThreadMessages()
                {
                    Error = exception.Message
                };

                AppContextError.Send(exception, profileThreadMessages, ExceptionLevel.Application);
                return profileThreadMessages;
            }
        }

        public async Task<ProfileThreadMessages> SendMessage(string authToken, string to, string subject, string message, string threadId = null)
        {
            try
            {
                var contentValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("to", to),
                    new KeyValuePair<string, string>("subject", subject),
                    new KeyValuePair<string, string>("message", message)
                };

                if (!string.IsNullOrEmpty(threadId))
                {
                    contentValues.Add(new KeyValuePair<string, string>("thread_id", threadId));
                }

                var content = new FormUrlEncodedContent(contentValues.ToArray());

                const string addressSuffix = Identifier + "/send";
                return await _profileThreadMessagesClient.PostAsync(authToken, content, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileThreadMessages()
                {
                    Error = exception.Message,
                };

                AppContextError.Send(exception, error, ExceptionLevel.Application);
                return error;
            }
        }

        public async Task<List<Message>> GetThreadMessages(string authToken, string threadId)
        {
            try
            {
                string addressSuffix = "/thread/" + threadId;
                return await _threadMessagesClient.GetListAsync(authToken, Identifier, addressSuffix);

            }
            catch (Exception exception)
            {
                AppContextError.Send(exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<ProfileThreadMessages> MarkAsReadThreadMessages(string authToken, string threadId)
        {
            try
            {
                string addressSuffix = Identifier + "/mark_as_read/" + threadId;
                return await _profileThreadMessagesClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileThreadMessages
                {
                    Error =  exception.Message
                };

                AppContextError.Send(exception, error, ExceptionLevel.Application);
                return error;
            }
        }

        public async Task<ProfileThreadMessages> MarkAsUnreadThreadMessages(string authToken, string threadId)
        {
            try
            {
                string addressSuffix = Identifier + "/mark_as_unread/" + threadId;
                return await _profileThreadMessagesClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileThreadMessages
                {
                    Error = exception.Message
                };

                AppContextError.Send(exception, error, ExceptionLevel.Application);
                return error;
            }
        }

        public async Task<ProfileThreadMessages> DeleteThreadMessages(string authToken, string threadId)
        {
            try
            {
                string addressSuffix = Identifier + "/delete/" + threadId;
                return await _profileThreadMessagesClient.PostAsync(authToken, null, addressSuffix);
            }
            catch (Exception exception)
            {
                var error = new ProfileThreadMessages
                {
                    Error = exception.Message
                };

                AppContextError.Send(exception, error, ExceptionLevel.Application);
                return error;
            }
        }
    }
}
