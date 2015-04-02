﻿using Xamarin.Forms;

namespace BeginMobile.Pages.MessagePages
{
    public class ProfileMessageHeader: ViewCell
    {
        public ProfileMessageHeader()
        {
            Height = 30;

            var labelHeader = new Label
                              {
                                  FontSize = 18,
                                  FontAttributes = FontAttributes.Bold
                              };

            labelHeader.SetBinding(Label.TextProperty, "Key");

            var stLayout = new StackLayout
                           {
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               Orientation = StackOrientation.Horizontal,
                               Children =
                               {
                                   labelHeader
                               }
                           };

            View = stLayout;
        }
    }
}