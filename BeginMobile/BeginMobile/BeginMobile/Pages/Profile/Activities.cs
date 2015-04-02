﻿using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class Activities : ViewCell
    {
        public Activities()
        {
            var icon = new CircleImage
                       {
                           HeightRequest = Device.OnPlatform(50, 100, 100),
                           WidthRequest = Device.OnPlatform(50, 100, 100),
                           Aspect = Aspect.AspectFill,
                           HorizontalOptions = LayoutOptions.Start,
                           BorderThickness = Device.OnPlatform(2, 3, 3)
                       };

            icon.SetBinding(Image.SourceProperty, new Binding("Icon"));

            var optionLayout = CreateOptionLayout();
            View = new StackLayout
                   {
                       Orientation = StackOrientation.Horizontal,
                       Children =
                       {
                           icon,
                           optionLayout
                       }
                   };
        }

        private static Grid CreateOptionLayout()
        {
            var labelUserName = new Label
                                {
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    YAlign = TextAlignment.Center,
                                    Style = App.Styles.ListItemTextStyle
                                };

            var labelActivityDescription = new Label
                                           {
                                               HorizontalOptions = LayoutOptions.FillAndExpand,
                                               YAlign = TextAlignment.Center,
                                               Style = App.Styles.ListItemDetailTextStyle
                                           };

            var labelActivityType = new Label
                                    {
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        YAlign = TextAlignment.Center,
                                        Style = App.Styles.ListItemDetailTextStyle
                                    };

            var dateAndTime = new Label
                              {
                                  HorizontalOptions = LayoutOptions.FillAndExpand,
                                  YAlign = TextAlignment.Center,
                                  Style = App.Styles.ListItemDetailTextStyle
                              };

            labelUserName.SetBinding(Label.TextProperty, "NameSurname");
            labelActivityDescription.SetBinding(Label.TextProperty, "ActivityDescription");
            dateAndTime.SetBinding(Label.TextProperty, "DateAndTime");
            labelActivityType.SetBinding(Label.TextProperty, "ActivityType");

            var gridDetails = new Grid
                              {
                                  Padding = App.Styles.ListDetailThickness,
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

            gridDetails.Children.Add(labelUserName, 0, 0);
            gridDetails.Children.Add(labelActivityDescription, 1, 0);
            gridDetails.Children.Add(labelActivityType, 0, 1);
            gridDetails.Children.Add(dateAndTime, 1, 1);

            return gridDetails;
        }
    }

    public class ActivityViewModel
    {
        public string Icon { get; set; }
        public string NameSurname { get; set; }
        public string DateAndTime { get; set; }
        public string ActivityDescription { get; set; }
        public string ActivityType { get; set; }
    }
}