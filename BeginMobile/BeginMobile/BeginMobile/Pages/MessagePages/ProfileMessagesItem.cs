﻿using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessagesItem : ViewCell
    {
        private static string GroupImage
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "userdefault.png" : "userdefault3.png";
            }
        }

        public ProfileMessagesItem()
        {

            var shopImage = new CircleImage
                            {
                                BorderColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                                BorderThickness = Device.OnPlatform(2, 3, 3),
                                HeightRequest = Device.OnPlatform(50, 100, 100),
                                WidthRequest = Device.OnPlatform(50, 100, 100),
                                Aspect = Aspect.AspectFill,
                                HorizontalOptions = LayoutOptions.Start,
                                Source = GroupImage
                            };

            var lblGrTitle = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemTextStyle,
                                 FontAttributes = FontAttributes.Bold,
                                 HorizontalOptions = LayoutOptions.Start
                             };

            lblGrTitle.SetBinding(Label.TextProperty, "Title");

            var lblCreate = new Label
                            {
                                YAlign = TextAlignment.Center,
                                Style = App.Styles.ListItemDetailTextStyle,
                                HorizontalOptions = LayoutOptions.End
                            };

            lblCreate.SetBinding(Label.TextProperty, "CreateDate", stringFormat: "Date: {0}");

            var lblContent = new Label
                             {
                                 YAlign = TextAlignment.Center,
                                 Style = App.Styles.ListItemDetailTextStyle,
                                 HorizontalOptions = LayoutOptions.StartAndExpand
                             };

            lblContent.SetBinding(Label.TextProperty, "Content");

            var gridDetails = new Grid
                              {
                                  Padding = new Thickness(10, 5, 10, 5),
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  VerticalOptions = LayoutOptions.FillAndExpand,
                                  RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
                                  ColumnDefinitions =
                                  {
                                      new ColumnDefinition {Width = GridLength.Auto},
                                      new ColumnDefinition {Width = GridLength.Auto}
                                  }
                              };

            gridDetails.Children.Add(lblGrTitle, 0, 0);
            gridDetails.Children.Add(lblContent, 0, 1);

            gridDetails.Children.Add(lblCreate, 1, 0);

            var sLayout = new StackLayout
                          {
                              Orientation = StackOrientation.Horizontal,
                              HorizontalOptions = LayoutOptions.FillAndExpand,
                              Children =
                              {
                                  shopImage,
                                  gridDetails
                              }
                          };

            View = sLayout;
        }
    }
}