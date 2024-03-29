﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Interfaces;
using BeginMobile.Services.Utils;

namespace BeginMobile.Services.ManagerServices
{
    public class GroupManager
    {
        private static readonly string BaseAddress = ConfigBaseAddress.BaseAddress;
        private static readonly string SubAddress = ConfigBaseAddress.SubAddress;
        private static readonly string Identifier = ConfigBaseAddress.IdentifierGroups;


        private readonly GenericBaseClient<Group> _groupClient =
            new GenericBaseClient<Group>(BaseAddress, SubAddress);
        private static readonly string ThisClassName = typeof(GroupManager).Name;
        public GroupManager()
        {
        }

        public async Task<ObservableCollection<Group>> GetGroupsByParams(
            string authToken,
            string name = null,
            string cat = null,
            string limit = null,
            string sections = null
            )
        {
            try
            {
                var urlGetParams = "?q=" + name + "&cat=" + cat + "&limit=" + limit + "&sections=" + sections;

                var resultGroups = await _groupClient.GetListAsync(authToken, Identifier, urlGetParams);
                var groups = Task.Run(() => _groupClient.ListToObservableCollection(resultGroups));

                return await groups;
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetGroupsByParams", exception, null, ExceptionLevel.Application);
                return null;
            }
        }

        public async Task<Group> GetGroupById(string authToken, string groupId, string sections = null)
        {
            try
            {
                var urlId = "/" + groupId;

                if (!string.IsNullOrEmpty(sections))
                {
                    urlId += "?sections=" + sections;
                }

                return await _groupClient.GetAsync(authToken, Identifier, urlId);
            }
            catch (Exception exception)
            {
                AppContextError.Send(ThisClassName, "GetGroupById", exception, null, ExceptionLevel.Application);
                return null;
            }
        }
    }
}