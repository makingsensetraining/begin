﻿using BeginMobile.Pages.GroupPages;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeginMobile.Utils.Extensions;

namespace BeginMobile.Pages.Profile
{
    public class Groups : ContentPage
    {
        private ListView _lViewGroup;
        private RelativeLayout _rLayout;
        private ProfileInformationGroups groupInformation;
        private Label noGroupsMessage;

        public Groups()
        {
            Title = "Groups";
            var currentUser = (LoginUser)App.Current.Properties["LoginUser"];
            groupInformation = App.ProfileServices.GetGroups(currentUser.User.UserName, currentUser.AuthToken);

            _lViewGroup = new ListView() { };

            _lViewGroup.ItemTemplate = new DataTemplate(typeof(ProfileGroupItemCell));
            _lViewGroup.ItemsSource = groupInformation.Groups;

            _lViewGroup.HasUnevenRows = true;

            _lViewGroup.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                {
                    return;
                }

                var groupItem = (Group)e.SelectedItem;
                var groupPage = new GroupItemPage();
                groupPage.BindingContext = groupItem;
                await Navigation.PushAsync(groupPage);

                // clears the 'selected' background
                ((ListView)sender).SelectedItem = null;
            };

            noGroupsMessage = new Label();

            SearchBar searchBar = new SearchBar
            {
                Placeholder = "Search by name",
            };

            searchBar.TextChanged += OnSearchBarButtonPressed;

            _rLayout = new RelativeLayout();
            
            _rLayout.Children.Add(searchBar,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            _rLayout.Children.Add(_lViewGroup,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            Content = new ScrollView() { Content = _rLayout };

           
        }

        

        private void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            var groupsList = groupInformation.Groups;

            SearchBar searchBar = (SearchBar)sender;
            string searchText = searchBar.Text; // recovery the text of search bar

            if (!string.IsNullOrEmpty(searchText) || !string.IsNullOrWhiteSpace(searchText))
            {

                if (groupsList.Count == 0)
                {
                    noGroupsMessage.Text = "There is no groups";
                }

                else
                {
                    List<Group> list =
                        (from g in groupsList 
                            where g.Name.Contains(searchText, StringComparison.InvariantCultureIgnoreCase)
                            select g).ToList<Group>();

                    if (list.Any())
                    {
                         _lViewGroup.ItemsSource = list;
                        noGroupsMessage.Text = "";
                    }

                    else
                    {
                         _lViewGroup.ItemsSource = groupInformation.Groups;
                    }
                }
            }
            else
            {
                _lViewGroup.ItemsSource = groupInformation.Groups;
            }

        }
    }
}