﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.DTO;
using BeginMobile.Services.ManagerServices;
using BeginMobile.Services.Utils;
using Xamarin.Forms;
using BeginMobile.Pages;

namespace BeginMobile.Accounts
{
    public class Register : BaseContentPage
    {
        private const string EmailRegex =
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private readonly Entry _entryUsername;
        private readonly Entry _entryFullName;
        private readonly Entry _entryEmail;
        private readonly Entry _entryPassword;
        private readonly Entry _entryConfirmPassword;
        private bool _switchStatus;
        private readonly ILoginManager _iLoginManager;

        readonly ContentPage _contentPageTermsAndConditions;

        public Register(ILoginManager iLoginManager)
        {
            Style = BeginApplication.Styles.InitialPageStyle;
            _iLoginManager = iLoginManager;
            if (AppResources.RegisterFormTitle != null) Title = AppResources.RegisterFormTitle;

            Title = "Create Account";

            _contentPageTermsAndConditions = new TermsAndConditions(true);

            /*var mainTitle = new Label
            {
                Text = "Create Account",//AppResources.LoginFormTitle,
                Style = BeginApplication.Styles.InitialPageTitleStyle,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };*/

            _entryUsername = new Entry
            {
                Placeholder = AppResources.EntryUsernamePlaceholder
            };

            _entryFullName = new Entry
            {
                Placeholder = AppResources.EntryFullNamePlaceholder
            };

            _entryEmail = new Entry
            {
                Placeholder = AppResources.EntryEmailPlaceholder
            };

            _entryPassword = new Entry
            {
                IsPassword = true,
                Placeholder = AppResources.EntryPasswordPlaceholder,
                
            };

            _entryConfirmPassword = new Entry
            {
                IsPassword = true,
                Placeholder = AppResources.EntryConfirmPasswordPlaceholder,
                
            };

            var buttonTermsAndConditions = new Button()
            {
                Text = AppResources.RegisterButtonTermsAndConditions,
                FontSize = 10,
                Style = BeginApplication.Styles.LinkButton,
                TextColor = Color.FromHex("77D065")
            };

            var switchTermsAndConditions = new Switch();
            switchTermsAndConditions.Toggled += (sender, eventArgs) => { _switchStatus = eventArgs != null && eventArgs.Value; };

            var buttonRegister = new Button
            {
                Text = "Create Account",//AppResources.ButtonRegister,
                Style = BeginApplication.Styles.DefaultButton

            };


            buttonTermsAndConditions.Clicked += async (sender, eventArgs) =>
            {
                await Navigation.PushAsync(_contentPageTermsAndConditions); 
                //MessagingCenter.Send<ContentPage>(this, "TermsAndConditions")
            };

            buttonRegister.Clicked += RegisterClickEventHandler;

            var stackLayoutSwitch = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
                Children = { buttonTermsAndConditions, switchTermsAndConditions }
            };

            var stackLayoutLoading = CreateStackLayoutWithLoadingIndicator();


            Content = new ScrollView
                      {
                          Content = new StackLayout
                                    {
                                        Padding = BeginApplication.Styles.InitialPagesThickness,
                                        VerticalOptions = LayoutOptions.Center,
                                        Children =
                                        {
                                            stackLayoutLoading,
                                            _entryUsername,
                                            _entryFullName,
                                            _entryEmail,
                                            _entryPassword,
                                            _entryConfirmPassword,
                                            _entryConfirmPassword,
                                            stackLayoutSwitch,
                                            buttonRegister
                                            
                                        }
                                    }
                      };
        }

        public async void RegisterClickEventHandler(object sender, EventArgs eventArgs)
        {
            var userName = _entryUsername.Text.Trim();
            var fullName = _entryFullName.Text.Trim();
            var email = _entryEmail.Text.Trim();
            var password = _entryPassword.Text.Trim();
            var confirmPassword = _entryConfirmPassword.Text.Trim();

            if (String.IsNullOrEmpty(fullName) ||
                String.IsNullOrEmpty(email)
                || String.IsNullOrEmpty(password)
                || String.IsNullOrEmpty(confirmPassword)
                || String.IsNullOrEmpty(userName)
                )
            {
                await DisplayAlert(AppResources.ApplicationValidationError,
                    AppResources.RegisterAlertFieldsAreRequired,
                    AppResources.AlertReTry);
            }
            else
            {
                var isEmailValid = Regex.IsMatch(email, EmailRegex);
                if (isEmailValid)
                {
                    if (password.Equals(confirmPassword))
                    {
                        if (_switchStatus)
                        {
                            ActivityIndicatorLoading.IsVisible = true;
                            ActivityIndicatorLoading.IsRunning = true;

                            var loginUserManager = new LoginUserManager();
                            var registerUser = await loginUserManager.Register(userName, email,
                                password, fullName);

                            if (registerUser != null)
                            {
                                if (registerUser.Errors != null)
                                {                                   
                                    var errorMessages = ErrorMessages.GetTranslatedErrors(registerUser.Errors).Aggregate("", (current, error) => current + (error + "\n"));
                                    await DisplayAlert(AppResources.ApplicationError, errorMessages, AppResources.AlertOk);
                                }
                                else
                                {
                                    await
                                        DisplayAlert(AppResources.ServerMessageSuccess,
                                            AppResources.RegisterAlertSuccessMessage, AppResources.AlertOk);

                                    var loginUser = new LoginUser
                                                    {
                                                        AuthToken = registerUser.AuthToken,
                                                        User = registerUser.User
                                                    };

                                    Application.Current.Properties["IsLoggedIn"] = true;
                                    Application.Current.Properties["LoginUser"] = loginUser;

                                    //iLoginManager.ShowMainPage(loginUser);
                                    _iLoginManager.ShowUploaderPage();
                                }
                            }

                            ActivityIndicatorLoading.IsVisible = false;
                            ActivityIndicatorLoading.IsRunning = false;
                        }

                        else
                        {
                            await DisplayAlert(AppResources.ApplicationValidationError,
                                AppResources.RegisterAlertValidationTermsAndConditions,
                                AppResources.AlertReTry);
                        }
                    }
                    else
                    {
                        await DisplayAlert(AppResources.ApplicationValidationError,
                            AppResources.RegisterAlertValidationPassAndConfirm,
                            AppResources.AlertReTry);
                    }
                }
                else
                {
                    await DisplayAlert(AppResources.ApplicationValidationError,
                        AppResources.RegisterAlertValidationEmail,
                        AppResources.AlertReTry);
                }
            }

        }
    }
}