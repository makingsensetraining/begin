﻿using System.Collections.Generic;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BeginMobile.Services
{
    public class ProfileServices
    {
        private readonly ProfileManager _profileManager;
        private readonly GroupManager _groupManager;
        private readonly EventManager _eventManager;
        private readonly ContactManager _contactManager;
        private readonly NotificationManager _notificationManager;
        private readonly MessageManager _messageManager;

        public ProfileServices()
        {
            _profileManager = new ProfileManager();
            _eventManager = new EventManager();
            _groupManager = new GroupManager();
            _contactManager = new ContactManager();
            _notificationManager = new NotificationManager();
            _messageManager = new MessageManager();
        }

        public async Task<ProfileInformationGroups> GetGroups(
            string authToken,
            string userName = null,
            string limit = null,
            string offset = null)
        {
            return await _profileManager.GetGroupsInformation(authToken, userName, limit, offset);
        }

        public ProfileInfo GetInformation(string userName, string authToken)
        {
            return _profileManager.GetProfileInformation(userName, authToken);
        }

        public async Task<ProfileInformationActivities> GetActivities(
            string authToken, 
            string userName = null,
            string limit = null,
            string offset = null)
        {
            return await _profileManager.GetActivitiesInformation(authToken, userName, limit, offset);
        }

        public async Task<ProfileInformationEvents> GetEvents(
            string authToken,
            string userName = null,
            string limit = null,
            string offset = null
            )
        {
            return await _profileManager.GetEventsInformation(authToken, userName, limit, offset);
        }

        public async Task<ProfileContacts> GetMyContacts(
            string authToken,
            string limit = null,
            string offset = null
            )
        {
            return await _profileManager.GetContactsInformation(authToken, limit: limit, offset: offset);
        }

        public async Task<ProfileContacts> GetUserContacts(
            string authToken,
            string userName,
            string limit = null,
            string offset = null
            )
        {
            return await _profileManager.GetContactsInformation(authToken,  username: userName, limit: limit, offset: offset);
        }

        public async Task<ProfileInformationShop> GetShopInfo(
            string authToken,
            string userName = null,
            string limit = null,
            string offset = null 
            )
        {
            return await _profileManager.GetShopInformation(authToken, userName, limit, offset);
        }

        public ProfileInformationMessages GetMessagesInfo(string userName, string authToken)
        {
            return _profileManager.GetMessagesInformation(userName, authToken);
        }

        public async Task<ProfileMeWall> GetWall(
            string authToken, 
            string filter = null, 
            string type = null,
            string limit = null,
            string offset= null
            )
        {
            return await _profileManager.GetWall(authToken, filter, type, limit, offset);
        }

        public async Task<ProfileInfo> GetInformationDetail(string authToken, string userName)
        {
            return await _profileManager.GetProfileInformationDetail(authToken, userName);
        }

        //Groups, events and contacts
        public async Task<ObservableCollection<Group>> GetGroupsByParams(string authToken, string name = null, string cat = null,
            string limit = null, string sections = null)
        {
            return await _groupManager.GetGroupsByParams(authToken, name, cat, limit, sections);
        }

        public async Task<Group> GetGroup(string authToken, string groupId, string sections = null)
        {
            return await _groupManager.GetGroupById(authToken, groupId, sections);
        }

        public async Task<ObservableCollection<ProfileEvent>> GetEventsByParams(string authToken, string name = null,
            string cat = null, string limit = null)
        {
            return await _eventManager.GetEventsByParams(authToken, name, cat, limit);
        }

        public ProfileEvent GetEvent(string authToken, string eventId)
        {
            return _eventManager.GetEventById(authToken, eventId);
        }

        public async Task<List<User>> GetContacts(
            string authToken, 
            string name = null, 
            string sort = null, 
            string limit = null,
            string offset = null)
        {
            return await _contactManager.GetContacts(authToken,name, sort, limit, offset);
        }

        public User GetContact(string authToken, string contactId)
        {
            return _contactManager.GetContactById(authToken, contactId);
        }

        //Contact options 
        public List<ServiceError> SendRequest(string authToken, string userName)
        {
            return _contactManager.SendRequest(authToken, userName);
        }

        public List<ServiceError> AcceptRequest(string authToken, string userName)
        {
            return _contactManager.AcceptRequest(authToken, userName);
        }

        public List<ServiceError> RejectRequest(string authToken, string userName)
        {
            return _contactManager.RejectRequest(authToken, userName);
        }

        public List<ServiceError> RemoveFriendship(string authToken, string userName)
        {
            return _contactManager.RemoveFriendship(authToken, userName);
        }

        public async Task<ProfileContacts> CancelRequest(string authToken, string userName)
        {
            return await _contactManager.CancelRequest(authToken, userName);
        }

        //Notifications
        public async Task<ProfileNotification> GetProfileNotification(string authToken, string limit = null, string status = null)
        {
            return await _notificationManager.GetProfileNotificationByParams(authToken, limit, status);
        }

        public async Task<ProfileNotification> MarkAsRead(string authToken, string notificationId = null)
        {
            return await _notificationManager.MarkAsRead(authToken, notificationId);
        }

        public async Task<ProfileNotification> MarkAsUnread(string authToken, string notificationId = null)
        {
            return await _notificationManager.MarkAsUnread(authToken, notificationId);
        }

        //Messages
        public async Task<ProfileThreadMessages> GetProfileThreadMessagesInbox(
            string authToken, 
            string q = null, 
            string limit = null,
            string offset = null)
        {
            return await _messageManager.GetProfileThreadMessagesInbox(authToken, q, limit, offset);
        }

        public async Task<ProfileThreadMessages> GetProfileThreadMessagesSent(
            string authToken, 
            string q = null, 
            string limit = null,
            string offset = null)
        {
            return await _messageManager.GetProfileThreadMessagesSent(authToken, q, limit, offset);
        }

        public async Task<ProfileThreadMessages> SendMessage(string authToken, string to, string subject, string message, string threadId = null)
        {
            return await _messageManager.SendMessage(authToken, to, subject, message, threadId);
        }

        public async Task<List<Message>> GetMessagesByThread(string authToken, string threadId)
        {
            return await _messageManager.GetThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> MarkAsReadByThread(string authToken, string threadId)
        {
            return await _messageManager.MarkAsReadThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> MarkAsUnreadByThread(string authToken, string threadId)
        {
            return await _messageManager.MarkAsUnreadThreadMessages(authToken, threadId);
        }

        public async Task<ProfileThreadMessages> DeleteByThread(string authToken, string threadId)
        {
            return await _messageManager.DeleteThreadMessages(authToken, threadId);
        }

        public User GetUser(string authToken, string userId)
        {
            return _contactManager.GetUserById(authToken, userId);
        }

        public async Task<List<ShopCategory>> GetShopCategories(
            string authToken,
            string limit = null,
            string offset = null,
            string catId = null)
        {
            return await _profileManager.GetCategories(authToken, limit, offset, catId);
        }
    }
}