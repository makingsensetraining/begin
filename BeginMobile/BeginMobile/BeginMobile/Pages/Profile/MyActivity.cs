﻿using System;
using System.Collections.Generic;
using System.Linq;
using BeginMobile.Services.DTO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace BeginMobile.Pages.Profile
{
    public class MyActivity : ContentPage
    {
        private const string UserDefault = "userdefault3.png";
        private LoginUser _currentUser;
        private ProfileInformationActivities _profileActivity;
        public MyActivity()
        {
            Title = "My activity";
            _currentUser = (LoginUser)App.Current.Properties["LoginUser"];

            Init();
        }

        private async Task Init()
        {
            _profileActivity = await App.ProfileServices.GetActivities(_currentUser.User.UserName, _currentUser.AuthToken);
            var listDataSource = new List<ActivityViewModel>();

            if (_profileActivity != null)
            {
                listDataSource.AddRange(from activity in _profileActivity.Activities
                                        where activity.Component.Equals("activity", StringComparison.InvariantCultureIgnoreCase)
                                        select new ActivityViewModel
                                        {
                                            Icon = UserDefault,
                                            NameSurname = _profileActivity.NameSurname,
                                            ActivityDescription = activity.Content,
                                            ActivityType = activity.Type,
                                            DateAndTime = activity.Date
                                        });
            }

            var listViewTemplate = new DataTemplate(typeof(Activities));
            var listViewActivities = new ListView
            {
                ItemsSource = listDataSource,
                ItemTemplate = listViewTemplate
            };

            listViewActivities.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                ((ListView)s).SelectedItem = null;
            };

            listViewActivities.HasUnevenRows = true;

            var stackLayout = new StackLayout
            {
                Spacing = 2,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical
            };

            stackLayout.Children.Add(listViewActivities);

            var mainStackLayout = new StackLayout
            {
                Spacing = 2,
                Padding = App.Styles.LayoutThickness
            };

            mainStackLayout.Children.Add(stackLayout);
            Content = mainStackLayout;
        }
    }
}