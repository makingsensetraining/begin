﻿using BeginMobile.Services.DTO;
using BeginMobile.Services.Models;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Profile
{
    public class EventDetailInformation : ContentPage
    {
        private const string AllCategories = "No Categories";
        private const string TextCategories = "Categories:";
        private const string TextDateAndHour = "Date/Hour:";
        private const string TextContent = "Event Description:";
        private const string TextAvailableSpaces = "Available spaces:";

        public EventDetailInformation(ProfileEvent eventInfo)
        {
            Style = BeginApplication.Styles.PageStyle;
            this.SetBinding(TitleProperty, "Name", stringFormat: "Event - {0}");

            var profileEvent = eventInfo;

            if (profileEvent == null)
            {
                return;
            }

            var profileEventViewModel = GetEvenViewModel(profileEvent);

            var imageEvent = new CircleImage
            {
                Style = BeginApplication.Styles.CircleImageForDetails,
                Source = BeginApplication.Styles.DefaultEventIcon//TODO:change for event avatar if this exist
            };



            var gridImage = new Grid
            {
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stackLayoutLinesRight = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   BoxViewLine(),
                   BoxViewLine()
                }
            };
            var stackLayoutLinesLeft = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                   BoxViewLine(),
                   BoxViewLine()
                }
            };

            gridImage.Children.Add(stackLayoutLinesLeft, 0, 0);
            gridImage.Children.Add(imageEvent, 1, 0);
            gridImage.Children.Add(stackLayoutLinesRight, 2, 0);

            var labelEventName = new Label
            {
                Text = profileEventViewModel.Name,
                Style = BeginApplication.Styles.SubtitleStyle,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var labelTextDateAndHour = new Label
            {
                YAlign = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                Style = BeginApplication.Styles.ListItemTextStyle,
                Text = TextDateAndHour
            };

            var labelDates = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                Text = profileEventViewModel.TextDates
            };

            var labelTimes = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                Text = profileEventViewModel.TextTimes
            };

            var labelTextCategories = new Label
            {
                YAlign = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
                Style = BeginApplication.Styles.ListItemTextStyle,
                Text = TextCategories
            };

            var labelCategories = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                Text = AllCategories
            };

            var labelTextContent = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.LabelLargeTextTitle,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                Text = TextContent
            };

            var labelTextAvailableSpaces = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.LabelLargeTextTitle,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                Text = TextAvailableSpaces
            };

            var labelAvailableSpaces = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                Text = profileEventViewModel.EventAvailableSpaces
            };

            var labelEventDescription = new Label
            {
                YAlign = TextAlignment.End,
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                Text = profileEventViewModel.EventDescription
            };


            var gridMainContent = new Grid
            {
                Padding = BeginApplication.Styles.GridPadding,
                //VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                RowDefinitions =
                                      {
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto}
                                          
                                      }
            };

            var stackLayoutSectionCategories = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children =
                                                   {
                                                       labelTextCategories,
                                                       labelCategories
                                                   }
            };

            var gridBooking = new Grid
            {
                //VerticalOptions = LayoutOptions.FillAndExpand,
                //HorizontalOptions = LayoutOptions.CenterAndExpand,
                RowDefinitions =
                                      {
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},
                                          new RowDefinition {Height = GridLength.Auto},                                                                               
                                      }
            };
            var labelBookingTitle = new Label
            {
                Text = "Bookings",//TODO Add to resources
                Style = BeginApplication.Styles.TitleStyle,
                FontAttributes = FontAttributes.Bold

            };
            gridBooking.Children.Add(labelBookingTitle, 0, 0);

            var stackLayoutDescriptionAndBooking = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { labelTextContent,labelEventDescription, gridBooking }
            };

           // gridMainContent.Children.Add(gridImage, 0, 0);
            gridMainContent.Children.Add(labelEventName, 0, 1);
            gridMainContent.Children.Add(labelTextDateAndHour, 0, 2);
            gridMainContent.Children.Add(labelDates, 0, 3);
            gridMainContent.Children.Add(labelTimes, 0, 4);
            gridMainContent.Children.Add(stackLayoutSectionCategories, 0, 5);
            gridMainContent.Children.Add(labelTextAvailableSpaces, 0, 6);
            gridMainContent.Children.Add(labelAvailableSpaces, 0, 7);
            gridMainContent.Children.Add(stackLayoutDescriptionAndBooking, 0, 8);

            Content = new ScrollView
                      {
                          Content = new StackLayout
                                    {
                                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                                        Orientation = StackOrientation.Vertical,
                                        Children =
                                        {
                                            gridImage,
                                            gridMainContent
                                            //gridButtons
                                        }
                                    }
                      };
        }

        private static EventViewModel GetEvenViewModel(ProfileEvent profileEvent)
        {
            {
                var modelView = new EventViewModel
                {
                    UserFullName = profileEvent.Owner.NameSurname,
                    TextDates = profileEvent.StartDate + " to " + profileEvent.EndDate,
                    TextTimes = profileEvent.StartTime + " - " + profileEvent.EndTime,
                    Name = profileEvent.Name,
                    Categories = AllCategories,
                    EventDescription = profileEvent.Content,
                    EventAvailableSpaces = profileEvent.Spaces
                };

                return modelView;
            }
        }
        private static BoxView BoxViewLine()
        {
            return new BoxView { Color = BeginApplication.Styles.ColorLine, WidthRequest = 100, HeightRequest = 2 };
        }
    }
}