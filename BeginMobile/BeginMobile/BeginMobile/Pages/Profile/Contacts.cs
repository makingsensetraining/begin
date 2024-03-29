﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using BeginMobile.Services.Utils;
using BeginMobile.Utils;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Contacts : ContentPage
    {
        private ListView _listViewContacts;
        private Label _labelNoContactsMessage;
        private readonly List<Contact> _defaultList = new List<Contact>();
        private readonly SearchView _searchView;
        private readonly LoginUser _currentUser;
        //private ProfileContacts _profileInformationContacts;
        private ObservableCollection<Contact> _profileContacts;
        private const string Aroba = "@";

        //Paginator
        private readonly ActivityIndicator _activityIndicatorLoading;
        private readonly StackLayout _stackLayoutLoadingIndicator;
        private bool _isLoading;
        private int _offset = 0;
        private int _limit = DefaultLimit;
        private string _name;
        private string _sort;
        private const int DefaultLimit = 10;
        private bool _areLastItems;
        private Grid _gridMainComponents;
        private Dictionary<string, string> _sortOptionsDictionary = new Dictionary<string, string>
                                                                    {
                                                                        {"last_active", "Last Active"},
                                                                        {"newest_registered", "Newest Registered"},
                                                                        {"alpha", "Alphabetical"},
                                                                        {string.Empty, "None"}
                                                                    };

        private Picker _sortPicker;

        public Contacts()
        {
            Style = BeginApplication.Styles.PageStyle;
            Title = "Contacts";
            _searchView = new SearchView();
            _currentUser = (LoginUser) Application.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            var profileInformationContacts =
                await BeginApplication.ProfileServices.GetMyContacts(_currentUser.AuthToken, limit: _limit.ToString(), offset: _offset.ToString());

            LoadSortOptionsPicker();

            var userContacts = profileInformationContacts != null
                ? profileInformationContacts.Contacts as IEnumerable<User>
                : new List<User>();

            _profileContacts = new ObservableCollection<Contact>(RetrieveContacts(userContacts));

            //profileInfoContacts.AddRange();

            var contactListViewTemplate = new DataTemplate(() => new CustomViewCell(_currentUser));
            MessagingSubscriptions();

            _listViewContacts = new ListView
                                {
                                    ItemsSource = _profileContacts,
                                    ItemTemplate = contactListViewTemplate,
                                    HasUnevenRows = true
                                };

            _listViewContacts.ItemSelected += async (sender, eventArgs) =>
                                                    {
                                                        if (eventArgs.SelectedItem == null)
                                                        {
                                                            return;
                                                        }

                                                        var contactItem = (Contact) eventArgs.SelectedItem;

                                                        var contentPageContactDetail = new ContactDetail(contactItem)
                                                                                       {
                                                                                           BindingContext = contactItem
                                                                                       };

                                                        await Navigation.PushAsync(contentPageContactDetail);
                                                        ((ListView) sender).SelectedItem = null;
                                                    };

            _listViewContacts.ItemTapped += async (sender, eventArgs) =>
                                                  {
                                                      if (eventArgs.Item == null)
                                                      {
                                                          return;
                                                      }

                                                      var contactItem = (Contact) eventArgs.Item;

                                                      var contentPageContactDetail = new ContactDetail(contactItem)
                                                                                     {
                                                                                         BindingContext = contactItem
                                                                                     };

                                                      await Navigation.PushAsync(contentPageContactDetail);
                                                      ((ListView) sender).SelectedItem = null;
                                                  };

            _searchView.SearchBar.TextChanged += SearchItemEventHandler;
            _searchView.Limit.SelectedIndexChanged += SearchItemEventHandler;
            _sortPicker.SelectedIndexChanged += SearchItemEventHandler;

            _labelNoContactsMessage = new Label();

            var stackLayoutContactsList = new StackLayout
                                          {
                                              Padding = BeginApplication.Styles.ThicknessMainLayout,
                                              VerticalOptions = LayoutOptions.FillAndExpand,
                                              Orientation = StackOrientation.Vertical,
                                              Children =
                                              {
                                                  _listViewContacts
                                              }
                                          };

            ToolbarItem = new ToolbarItem("Filter", BeginApplication.Styles.FilterIcon, () =>
            {
                _searchView
                    .Container
                    .IsVisible
                    = true;
            });
#if __ANDROID__ || __IOS__
            ToolbarItems.Add(ToolbarItem);
#endif

            _gridMainComponents = new Grid
                      {
                          Padding = BeginApplication.Styles.ThicknessMainLayout,
                          VerticalOptions = LayoutOptions.FillAndExpand,
                          HorizontalOptions = LayoutOptions.FillAndExpand,
                          RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      
                                  }
                          
                      };

            _gridMainComponents.Children.Add(_searchView.Container, 0, 0);
            _gridMainComponents.Children.Add(_labelNoContactsMessage, 0, 1);
            _gridMainComponents.Children.Add(stackLayoutContactsList, 0, 2);
            Content = _gridMainComponents;
        }

        public ToolbarItem ToolbarItem { get; set; }
        public Grid GetGrid()
        {
            return _gridMainComponents;
        }
        #region Events

        /// <summary>
        /// Common handler when an searchBar item has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void SearchItemEventHandler(object sender, EventArgs args)
        {
            string limit;
            string sort;

            var q = sender.GetType() == typeof (SearchBar) ? ((SearchBar) sender).Text : _searchView.SearchBar.Text;

            RetrieveLimitSelected(out limit);
            RetrieveSortOptionSelected(out sort);

            var list = await BeginApplication.ProfileServices.GetContacts(_currentUser.AuthToken, q, sort, limit) ??
                       new List<User>();

            if (list.Any())
            {
                _listViewContacts.ItemsSource = new ObservableCollection<Contact>(RetrieveContacts(list));
                _labelNoContactsMessage.Text = string.Empty;
            }

            else
            {
                _listViewContacts.ItemsSource = new ObservableCollection<Contact>(_defaultList);
            }
        }

        #endregion

        #region Private methods

        private void LoadSortOptionsPicker()
        {
            _sortPicker = new Picker
                          {
                              Title = "Sort by",
                              VerticalOptions = LayoutOptions.CenterAndExpand,
                              BackgroundColor = BeginApplication.Styles.ColorWhite
                          };

            if (_sortOptionsDictionary != null)
            {
                foreach (var op in _sortOptionsDictionary.Values)
                {
                    _sortPicker.Items.Add(op);
                }
            }

            else
            {
                _sortOptionsDictionary = new Dictionary<string, string> {{"last_active", "Last Active"}};
            }

            _searchView.Container.Children.Add(_sortPicker);
        }

        private void RetrieveSortOptionSelected(out string sort)
        {
            var catSelectedIndex = _sortPicker.SelectedIndex;
            var catLastIndex = _sortPicker.Items.Count - 1;

            var selected = catSelectedIndex == -1 || catSelectedIndex == catLastIndex
                ? null
                : _sortPicker.Items[catSelectedIndex];

            sort = selected == null ? null : _sortOptionsDictionary.FirstOrDefault(s => s.Value == selected).Key;
        }

        private void RetrieveLimitSelected(out string limit)
        {
            var limitSelectedIndex = _searchView.Limit.SelectedIndex;
            var limitLastIndex = _searchView.Limit.Items.Count - 1;

            limit = limitSelectedIndex == -1 || limitSelectedIndex == limitLastIndex
                ? null
                : _searchView.Limit.Items[limitSelectedIndex];
        }

        private static IEnumerable<Contact> RetrieveContacts(IEnumerable<User> profileInformationContacts)
        {
            if (profileInformationContacts != null)
            {
                return profileInformationContacts.Select(contact => new Contact
                                                                    {
                                                                        Icon =
                                                                            BeginApplication.Styles.DefaultContactIcon,
                                                                        //TODO:change for contac avatar
                                                                        NameSurname = contact.NameSurname,
                                                                        Email = contact.Email,
                                                                        Url = contact.Url,
                                                                        UserName =
                                                                            string.Format("{0}{1}", Aroba,
                                                                                contact.UserName),
                                                                        Registered =
                                                                            DateConverter.GetTimeSpan(
                                                                                DateTime.Parse(contact.Registered)),
                                                                        Id = contact.Id.ToString(),
                                                                        Relationship = contact.Relationship,
                                                                        IsOnline = contact.IsOnline,
                                                                        Profession = contact.Profession
                                                                    });
            }
            else
            {
                return new List<Contact>();
            }
        }

        private void MessagingSubscriptions()
        {
            MessagingCenter.Subscribe(this, FriendshipMessages.DisplayAlert, DisplayAlertCallBack());
            MessagingCenter.Subscribe(this, FriendshipMessages.RemoveContact, RemoveContactCallback());
        }

        private Action<CustomViewCell, string> DisplayAlertCallBack()
        {
            return async (sender, arg) => { await DisplayAlert("Error", arg, "Ok"); };
        }

        private Action<CustomViewCell, string> RemoveContactCallback()
        {
            return async (sender, arg) =>
                         {
                             var removeUsername = arg;

                             if (!string.IsNullOrEmpty(removeUsername))
                             {
                                 var confirm = await DisplayAlert("Confirm",
                                     string.Format("Are you sure you want to remove '{0}' from contacts?",
                                         removeUsername), "Yes", "No");

                                 if (confirm)
                                 {
                                     var responseErrors = FriendshipActions.Request(FriendshipOption.Send,
                                         _currentUser.AuthToken, removeUsername);

                                     if (responseErrors.Any())
                                     {
                                         var message = responseErrors.Aggregate(string.Empty,
                                             (current, contactServiceError) =>
                                                 current + (contactServiceError.ErrorMessage + "\n"));
                                         await
                                             DisplayAlert("Error", message, "Ok");
                                     }

                                     else
                                     {
                                         var contacts = ((ObservableCollection<Contact>) _listViewContacts.ItemsSource);
                                         var toRemove =
                                             contacts.FirstOrDefault(contact => contact.UserName == removeUsername);

                                         if (toRemove != null && contacts.Remove(toRemove))
                                         {
                                             _listViewContacts.ItemsSource = contacts;
                                             await
                                                 DisplayAlert("Info",
                                                     string.Format("'{0}' has been deleted successfully.",
                                                         removeUsername), "Ok");
                                         }
                                     }
                                 }
                             }
                         };
        }

        #endregion

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //Content = null;
            _profileContacts = null;
        }
    }
}