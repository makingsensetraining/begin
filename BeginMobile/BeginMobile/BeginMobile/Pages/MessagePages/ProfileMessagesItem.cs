﻿using System;
using BeginMobile.LocalizeResources.Resources;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        public ProfileMessagesItem()
        {
            var circleShopImage = new CircleImage
                                  {
                                      BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                      BorderThickness = Device.OnPlatform(2, 3, 3),
                                      HeightRequest = Device.OnPlatform(50, 100, 100),
                                      WidthRequest = Device.OnPlatform(50, 100, 100),
                                      Aspect = Aspect.AspectFit,
                                      HorizontalOptions = LayoutOptions.Start,
                                      Source = BeginApplication.Styles.MessageIcon
                                  };

            var labelSender = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelSender.SetBinding(Label.TextProperty, "SenderName", stringFormat: "From: {0}");

            var labelSubject = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemTextStyle,
                                   FontAttributes = FontAttributes.Bold,
                                   HorizontalOptions = LayoutOptions.Start
                               };
            labelSubject.SetBinding(Label.TextProperty, "Subject");

            var labelCreate = new Label
                              {
                                  YAlign = TextAlignment.Center,
                                  Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                  HorizontalOptions = LayoutOptions.StartAndExpand
                              };

            labelCreate.SetBinding(Label.TextProperty, "DateSent", stringFormat: "Date: {0}");

            var labelContent = new Label
                               {
                                   YAlign = TextAlignment.Center,
                                   Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                   HorizontalOptions = LayoutOptions.StartAndExpand
                               };
            labelContent.SetBinding(Label.TextProperty, "MessageContent");

            var labelMarkedAs = new Label
                                {                                    
                                    YAlign = TextAlignment.Center,                                                                        
                                    Style = BeginApplication.Styles.ListItemDetailTextStyle,
                                    TextColor = Color.FromHex("000000"),
                                    BackgroundColor = Color.FromHex("F6B94D"),                                    
                                    XAlign = TextAlignment.Center,
                                    FontAttributes = FontAttributes.Bold,
                                    //WidthRequest = 40,
                                    HorizontalOptions = LayoutOptions.StartAndExpand                                 
                                };

            labelMarkedAs.SetBinding(Label.TextProperty, "ThreadUnRead");


            var buttonRemove = new Button
                               {
                                   Text = AppResources.ButtonRemoveFriend,
                                   Style = BeginApplication.Styles.ListViewItemButton,
                                   HorizontalOptions = LayoutOptions.Start,
                                   HeightRequest = Device.OnPlatform(15, 35, 35),
                                   WidthRequest = Device.OnPlatform(70, 70, 70)
                               };
            buttonRemove.Clicked += RemoveEventHandler;

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(labelSender, 0, 0);
            gridDetails.Children.Add(labelMarkedAs, 1, 1);
            gridDetails.Children.Add(labelSubject, 0, 1);
            gridDetails.Children.Add(labelContent, 0, 2);
            gridDetails.Children.Add(labelCreate, 0, 3);
            gridDetails.Children.Add(buttonRemove, 0, 4);

            var stackLayoutView = new StackLayout
                                  {
                                      Spacing = 2,
                                      Padding = BeginApplication.Styles.LayoutThickness,
                                      Orientation = StackOrientation.Horizontal,
                                      HorizontalOptions = LayoutOptions.FillAndExpand,
                                      VerticalOptions = LayoutOptions.FillAndExpand,
                                      Children =
                                      {
                                          circleShopImage,
                                          gridDetails
                                      }
                                  };

            View = stackLayoutView;
            View.SetBinding(ClassIdProperty, "ThreadId");
        }


        public void RemoveEventHandler(object sender, EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var threadId = current.Parent.Parent.ClassId;
            if (InboxMessage.IsInbox)
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.RemoveInboxMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this,
                    MessageSuscriptionNames.RemoveInboxMessage);
            }
            else
            {
                MessagingCenter.Send(this, MessageSuscriptionNames.RemoveSentMessage, threadId);
                MessagingCenter.Unsubscribe<ProfileMessagesItem, string>(this, MessageSuscriptionNames.RemoveSentMessage);
            }
        }
    }
}