﻿using System.Collections.Generic;
using Xamarin.Forms;

namespace BeginMobile.MenuProfile
{
    public class MenuPage: ContentPage
    {
        public ListView Menu { get; set; }
        public MenuPage()
        {
            //Icon = "settings.png";
            Title = "Menu";
            BackgroundColor = Color.FromHex("333333");

            var listTool1 = new ToolbarItem()
            {
                Icon = "",
                Text = "Option 1",
                Order = ToolbarItemOrder.Secondary
            };

            var listTool2 = new ToolbarItem()
            {
                Icon = "",
                Text = "Option 2",
                Order = ToolbarItemOrder.Secondary
            };

            var listTool3 = new ToolbarItem()
            {
                Icon = "",
                Text = "Option 3",
                Order = ToolbarItemOrder.Secondary
            };

            var listTool4 = new ToolbarItem()
            {
                Icon = "",
                Text = "Option 4",
                Order = ToolbarItemOrder.Secondary,
                //Command = new Command()
            };
            
            ToolbarItems.Add(listTool1);
            ToolbarItems.Add(listTool2);
            ToolbarItems.Add(listTool3);
            ToolbarItems.Add(listTool4);

            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };

            //Layout
            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            //Content
            Content = layout;

        }
    }
}
