﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace BeginMobile.Utils
{
    public class CustomizedButtonStyle
    {
        public static Style GetControlButtonStyle()
        {
            var controlButtonStyle = new Style(typeof(Button))
            {
                Setters =
                                                 {
                                                     new Setter
                                                         {
                                                             Property = Button.BackgroundColorProperty,
                                                             Value = Color.Transparent
                                                         },
                                                     new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                                     new Setter {Property = Button.HeightRequestProperty, Value = 42},
                                                     new Setter {Property = Button.WidthRequestProperty, Value = 220},
                                                     new Setter {Property = Button.FontSizeProperty, Value = 12},
                                                 }
            };
            return controlButtonStyle;
        }

        public static Style GetButtonStyle()
        {
            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                                          {
                                              new Setter
                                                  {                                                      
                                                      Property = Button.BackgroundColorProperty,
                                                      Value = Color.FromHex("77D065")
                                                  },
                                              new Setter {Property = Button.FontProperty, Value = Color.White}
                                          }
            };
            return buttonStyle;
        }
    }
}
