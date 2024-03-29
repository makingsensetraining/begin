﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using BeginMobile.Services.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace BeginMobile.Services.Interfaces
{
    public class GenericBaseClient<T> where T : class
    {
        private readonly string _serviceBaseAddress;
        private readonly string _subAddress;
        private const int PostAsyncTimeout = 2000; //2 seconds for requests to web services
        private readonly ILoggingService _log = Logger.Current;

        public GenericBaseClient(string baseAddress, string subAddress)
        {
            _serviceBaseAddress = baseAddress;
            _subAddress = subAddress;
        }

        /// <summary>
        /// Gets.
        /// </summary>
        public T Get(string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                T profileInformationGroups = null;
                var response = httpClient.GetAsync(_subAddress + addressSuffix).Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationGroups = JsonConvert.DeserializeObject<T>(userJson);
                }

                return profileInformationGroups;
            }
        }

        /// <summary>
        /// Gets.
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1'</param>
        /// <param name="authToken">The authentication token.</param>
        public T Get(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                T profileInformationGroups = null;
                var response = httpClient.GetAsync(_subAddress + identifier + urlParams).Result;

                if (response.IsSuccessStatusCode)
                {
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    profileInformationGroups = JsonConvert.DeserializeObject<T>(userJson);
                }

                return profileInformationGroups;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        /// <returns></returns>
        public T Post(FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                T result = null;
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<T>(userJson);
                }

                return result;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        public T Post(string authToken, FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<T>(userJson);
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        /// <returns></returns>
        public async Task<string> PostContentResultAsync(FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                string result = "";
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                var response = await httpClient.PostAsync(_subAddress + addressSuffix, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = userJson;
                }

                return result;
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        public async Task<string> PostContentResultAsync(string authToken, FormUrlEncodedContent content,
            string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                string result = "";
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = await httpClient.PostAsync(_subAddress + addressSuffix, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result = userJson;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the list asynchronous.
        /// </summary>
        /// <param name="identifier">The identifier examples user, groups, etc</param>
        /// <param name="urlParams">The URL parameters examples '?user=user1'</param>
        /// <param name="authToken">The authentication token.</param>
        public IEnumerable<T> GetList(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.GetAsync(_subAddress + identifier + urlParams).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<T>>(userJson);
            }
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="authToken">The authentication token.</param>
        /// <param name="content">The content example object of this type FormUrlEncodedContent.</param>
        /// <param name="addressSuffix">The address suffix is complement url example 'me/change_password'.</param>
        public IEnumerable<T> PostList(string authToken, FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = httpClient.PostAsync(_subAddress + addressSuffix, content).Result;
                var userJson = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<List<T>>(userJson);
            }
        }

        /**
         * Methods async in services
         */

        public async Task<List<T>> GetListAsync(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = await httpClient.GetAsync(_subAddress + identifier + urlParams).ConfigureAwait(false);
                var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var resultList = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<T>>(userJson)
                    ).ConfigureAwait(false);

                return resultList;
            }
        }

        public async Task<List<T>> PostListAsync(string authToken, FormUrlEncodedContent content, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                var response = await httpClient.PostAsync(_subAddress + addressSuffix, content).ConfigureAwait(false);
                var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var resultList = await Task.Run(() =>
                    JsonConvert.DeserializeObject<List<T>>(userJson)).ConfigureAwait(false);

                return resultList;
            }
        }

        public async Task<T> GetAsync(string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);

                T profileInformationGroups = null;
                var response = await httpClient.GetAsync(_subAddress + addressSuffix).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    profileInformationGroups = await Task.Run(() =>
                        JsonConvert.DeserializeObject<T>(userJson)).ConfigureAwait(false);
                }

                return profileInformationGroups;
            }
        }

        public async Task<T> GetAsync(string authToken, string addressSuffix)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                T profileInformationGroups = null;
                var response = await httpClient.GetAsync(_subAddress + addressSuffix).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    profileInformationGroups = await Task.Run(() =>
                        JsonConvert.DeserializeObject<T>(userJson)).ConfigureAwait(false);
                }

                return profileInformationGroups;
            }
        }

        public async Task<T> GetAsync(string authToken, string identifier, string urlParams)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);

                T resultModel = null;
                var response = await httpClient.GetAsync(_subAddress + identifier + urlParams).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    resultModel = await Task.Run(() =>
                        JsonConvert.DeserializeObject<T>(userJson)
                        ).ConfigureAwait(false);
                }

                return resultModel;
            }
        }

        //use PostAsync with FormUrlEncodedContent object
        public async Task<T> PostAsync(FormUrlEncodedContent content, string addressSuffix,
            int timeout = PostAsyncTimeout)
        {
            return await PostAsync(null, content, addressSuffix, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="content"></param>
        /// <param name="addressSuffix"></param>
        /// <param name="timeout">in milliseconds</param>
        /// <returns></returns>
        public async Task<T> PostAsync(string authToken, FormUrlEncodedContent content, string addressSuffix,
            int timeout = PostAsyncTimeout)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_serviceBaseAddress);
                httpClient.Timeout = new TimeSpan(0, 0, 0, timeout);

                if (authToken != null)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authtoken", authToken);
                }

                var url = _subAddress + addressSuffix;

                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);

                var userJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = await Task.Run(() =>
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(userJson);
                    }
                    catch (Exception exception)
                    {
                        _log.ErrorFormat("Exception at '{0}' message: '{1}'", url, exception);
                        Debug.WriteLine("Exception at '{0}' message: '{1}'", url, exception);
                        return null;
                    }
                }).ConfigureAwait(false);

                if (response.IsSuccessStatusCode) return result;

                //log status code error
                _log.ErrorFormat("Url '{0}' not working: '{1}' : '{2}'", url, response.StatusCode,
                    response.ReasonPhrase);
                Debug.WriteLine("Url '{0}' not working: '{1}' : '{2}'", url, response.StatusCode,
                    response.ReasonPhrase);

                return result;
            }
        }

        public ObservableCollection<T> ListToObservableCollection(List<T> listItems)
        {
            ObservableCollection<T> resultCollection = null;
            if (listItems != null && listItems.Any())
            {
                resultCollection = new ObservableCollection<T>(listItems);
            }
            return resultCollection;
        }
    }
}