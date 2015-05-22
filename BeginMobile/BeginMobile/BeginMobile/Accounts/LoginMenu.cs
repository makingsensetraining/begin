﻿using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using BeginMobile.Interfaces;
using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Services.ManagerServices;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Accounts
{
    public class LoginMenu : BaseContentPage
	{

        private readonly ScrollView _mainScrollView;

        public LoginMenu()
        {
            Style = BeginApplication.Styles.InitialPageStyle;

            var logo = new CircleImage
                       {
                           Source = BeginApplication.Styles.LogoIcon,
                           Style = BeginApplication.Styles.CircleImageLogo,
                           HorizontalOptions = LayoutOptions.CenterAndExpand
                       };

            var buttonForgotPassword = new Button
                                       {
                                           Text = AppResources.ButtonForgotPassword,
                                           Style = BeginApplication.Styles.LinkButton,
                                           FontSize = BeginApplication.Styles.TextFontSizeLarge
                                       };
            var buttonLoginWithFacebook = new Button
                                          {
                                              Text = "Login with Facebook", //AppResources.ButtonLoginWithFacebook,
                                              Style = BeginApplication.Styles.DefaultButton
                                          };
            var buttonLogin = new Button
                              {
                                  Text = "Log in with User Name", //AppResources.ButtonLogin,
                                  Style = BeginApplication.Styles.DefaultButton
                              };

            var buttonRegister = new Button
                                 {
                                     Text = "Create Account", //AppResources.ButtonRegister,
                                     Style = BeginApplication.Styles.DefaultButton,
                                     BackgroundColor = BeginApplication.Styles.ColorBrown,
                                     TextColor = BeginApplication.Styles.ColorWhiteBackground

                                 };

            buttonForgotPassword.Clicked +=
                (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "ForgotPassword");


            buttonLogin.Clicked +=
                (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "LoginWithUserName");


            buttonRegister.Clicked += (sender, eventArgs) => MessagingCenter.Send<ContentPage>(this, "Register");

            _mainScrollView = new ScrollView();
        

        var mainGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Padding = BeginApplication.Styles.InitialPagesThickness,
                RowDefinitions = new RowDefinitionCollection
                                 {
                                     new RowDefinition{ Height = new GridLength(8,GridUnitType.Star)},
                                     new RowDefinition{ Height = new GridLength(4,GridUnitType.Star)},
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = GridLength.Auto},
                                     new RowDefinition{ Height = GridLength.Auto},
                                 }                                 
            };
            mainGrid.Children.Add(logo,0,0);
            mainGrid.Children.Add(buttonLogin,0,2);
            mainGrid.Children.Add(buttonForgotPassword,0,3);
            mainGrid.Children.Add(buttonRegister,0,4);
                            
            _mainScrollView.Content = mainGrid;
            Content = _mainScrollView;
            SizeChanged += (sender, e) => SetOrientation(this);
		}
        public void SetOrientation(Page page)
        {
            if (_mainScrollView != null)
                _mainScrollView.Padding = page.Width > page.Height //width > Height landscape else portrait
                    ? new Thickness(page.Width * 0.02, 0, page.Width * 0.02, 0)
                    : new Thickness(page.Width * 0.02, page.Height * 0.20, page.Width * 0.02, 0);
        }
	}
}