﻿using BeginMobile.LocalizeResources.Resources;
using BeginMobile.Utils;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Pages.Notifications
{
    public class TemplateListViewNotification : ViewCell
    {
        public TemplateListViewNotification(bool isUnread)
        {
            var circleIconImage = new CircleImage
            {
                Style = BeginApplication.Styles.CircleImageCommon
            };

            circleIconImage.SetBinding(Image.SourceProperty, new Binding("Icon"));
            var optionLayout = CreateOptionLayout();


            var gridComponents = new Grid
            {
                Padding = BeginApplication.Styles.ThicknessInsideListView,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                                     {
                                         new RowDefinition {Height = GridLength.Auto},
                                     },
                ColumnDefinitions =
                                     {
                                         new ColumnDefinition {Width = GridLength.Auto},
                                         new ColumnDefinition {Width = GridLength.Auto}
                                     }
            };
            gridComponents.Children.Add(circleIconImage, 0, 0);
            gridComponents.Children.Add(optionLayout, 1, 0);
            //View.SetBinding(ClassIdProperty, "Id");
            View = gridComponents;        
           // gridDetails.Children.Add(isUnread ? buttonMarkAsRead : buttonMarkAsUnread, 2, 0);                    
        }

        private Grid CreateOptionLayout()
        {
            var labelnotificationDesc = new Label
            {               
                WidthRequest = 350,
                YAlign = TextAlignment.Center,
                Style = BeginApplication.Styles.ListItemTextStyle
            };

            labelnotificationDesc.SetBinding(Label.TextProperty, "NotificationDescription");


            var labelintervalDate = new Label
            {
                Style = BeginApplication.Styles.ListItemDetailTextStyle,
                YAlign = TextAlignment.Center
            };

            labelintervalDate.SetBinding(Label.TextProperty, "IntervalDate");

            var buttonMarkAsRead = new Button
            {
                Text = AppResources.ButtonReadNotification,
                Style = BeginApplication.Styles.ListViewItemButton
            };


            buttonMarkAsRead.Clicked += OnMarkAsReadEventHandler;

            var buttonMarkAsUnread = new Button
            {
                Text = AppResources.ButtonUnReadNotification,
                Style = BeginApplication.Styles.ListViewItemButton
            };

            buttonMarkAsUnread.Clicked += OnMarkAsUnreadEventHandler;
        
            var gridDetails = new Grid
            {
                Padding = BeginApplication.Styles.ThicknessBetweenImageAndDetails,
                HorizontalOptions =  LayoutOptions.FillAndExpand,
                VerticalOptions =  LayoutOptions.FillAndExpand,
                RowDefinitions =
                                  {
                                      new RowDefinition {Height = GridLength.Auto},
                                      new RowDefinition {Height = GridLength.Auto}
                                  },
            };
            gridDetails.Children.Add(labelnotificationDesc, 0, 0);
            gridDetails.Children.Add(labelintervalDate, 0, 1);
           
            return gridDetails;
        }

        #region Events

        private void OnMarkAsUnreadEventHandler(object sender, System.EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var notificationId = current.Parent.ClassId;
            SubscribeMarkUnread(notificationId);
        }

        private void OnMarkAsReadEventHandler(object sender, System.EventArgs e)
        {
            var current = sender as Button;
            if (current == null) return;

            var notificationId = current.Parent.ClassId;
            SubscribeMarkRead(notificationId);
        }

        private void SubscribeMarkRead(string notificationId)
        {
            MessagingCenter.Send(this, NotificationMessages.MarkAsRead, notificationId);
            MessagingCenter.Unsubscribe<TemplateListViewNotification, string>(this, NotificationMessages.MarkAsRead);
        }

        private void SubscribeMarkUnread(string notificationId)
        {
            MessagingCenter.Send(this, NotificationMessages.MarkAsUnread, notificationId);
            MessagingCenter.Unsubscribe<TemplateListViewNotification, string>(this, NotificationMessages.MarkAsUnread);
        }

        #endregion
    }
}