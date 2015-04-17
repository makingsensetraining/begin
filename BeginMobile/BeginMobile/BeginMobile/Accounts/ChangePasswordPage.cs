﻿using System.Linq;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using Xamarin.Forms;
using BeginMobile.Services.Utils;
using BeginMobile.LocalizeResources.Resources;

namespace BeginMobile.Accounts
{
    public class ChangePasswordPage : ContentPage
    {
        private readonly Entry _entryCurrentPassword;
        private readonly Entry _entryNewPassword;
        private readonly Entry _entryRepeatNewPassword;

        public ChangePasswordPage()
        {
            var currentUser = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            var loginUserManager = new LoginUserManager();

            Title = AppResources.ChangePasswordTitle;

            _entryCurrentPassword = new Entry
            {
                Placeholder = AppResources.PasswordPlaceholder,
                IsPassword = true,
            };

            _entryNewPassword = new Entry
            {
                Placeholder = AppResources.PlaceholderChangePasswordNewPass,
                IsPassword = true,
            };

            _entryRepeatNewPassword = new Entry
            {
                Placeholder = AppResources.PlaceholderChangePasswordConfirmNewPass,
                IsPassword = true,
            };

            var buttonChangePassword = new Button
                                           {
                                               Text = AppResources.ButtonChangePasswordSend,
                                               HorizontalOptions = LayoutOptions.FillAndExpand,
                                               Style = BeginApplication.Styles.DefaultButton
                                           };

            buttonChangePassword.Clicked += async (sender, eventArgs) =>
            {
                var changePasswordResponse =
                    await loginUserManager.ChangeYourPassword(_entryCurrentPassword.Text, _entryNewPassword.Text, _entryRepeatNewPassword.Text, currentUser.AuthToken);

                if (changePasswordResponse != null)
                {
                    if (changePasswordResponse.Errors != null)
                    {
                        var messageErrors = changePasswordResponse.Errors.Aggregate("", (current, error) => current + "\n");
                        await DisplayAlert(AppResources.ErrorMessageTitle, messageErrors, "Re-try");
                        _entryCurrentPassword.Text = "";
                        _entryNewPassword.Text = "";
                        _entryRepeatNewPassword.Text = "";
                    }
                    else
                    {
                        await DisplayAlert(AppResources.ServerErrorMessageName, AppResources.ServerErrorMessage, "Ok");
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await DisplayAlert(AppResources.ServerMessageSuccess, AppResources.ServerMessageChangePassword, "Ok");
                    await Navigation.PopToRootAsync();
                }
            };

            var mainLayout = new StackLayout
            {
                Spacing = 20,
                Padding = BeginApplication.Styles.LayoutThickness,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    _entryCurrentPassword, _entryNewPassword, _entryRepeatNewPassword, buttonChangePassword
                }
            };

            Content = new ScrollView
            {
                Content = mainLayout
            };

            MessagingCenter.Subscribe<AppContextError>(this, AppContextError.NamedMessage, OnAppContextErrorOccurred);
        }

        private async void OnAppContextErrorOccurred(AppContextError appContextError)
        {
            await DisplayAlert(appContextError.Title, appContextError.Message, appContextError.Accept);
            await Navigation.PopToRootAsync();
        }
    }
}
