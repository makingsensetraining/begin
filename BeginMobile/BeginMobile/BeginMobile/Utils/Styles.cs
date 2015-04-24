﻿using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    public class Styles
    {
        private readonly double _fontSizeButtonSmall;
        private readonly double _textfontSizeMedium;
        private readonly double _textfontSizeLarge;
        private readonly double _textFontSizeSmall;
        private readonly Setter _buttonFontSize;
        private readonly Setter _titleFontSize;

        public Styles()
        {
            FontFamily = Device.OnPlatform("Helvetica", "Droid Sans Mono", "Comic Sans MS");

            var fontSizeForButtonMedium = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Button)));

            var fontSizeButtonLarge = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Large, typeof (Button)),
                Device.GetNamedSize(NamedSize.Large, typeof (Button)),
                Device.GetNamedSize(NamedSize.Large, typeof (Button)));

            _fontSizeButtonSmall = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Small, typeof (Button)),
                Device.GetNamedSize(NamedSize.Small, typeof (Button)),
                Device.GetNamedSize(NamedSize.Small, typeof (Button)));

            _textfontSizeMedium = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                Device.GetNamedSize(NamedSize.Medium, typeof (Label)));

            _textfontSizeLarge = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Large, typeof (Label)),
                Device.GetNamedSize(NamedSize.Large, typeof (Label)),
                Device.GetNamedSize(NamedSize.Large, typeof (Label)));

            _textFontSizeSmall = Device.OnPlatform(
                Device.GetNamedSize(NamedSize.Small, typeof (Label)),
                Device.GetNamedSize(NamedSize.Small, typeof (Label)),
                Device.GetNamedSize(NamedSize.Small, typeof (Label)));

            _buttonFontSize = new Setter();
            _titleFontSize = new Setter();
            var subTitleFontSize = new Setter();
            var textBodyFontSize = new Setter();


            if (Device.Idiom == TargetIdiom.Tablet)
            {
                _buttonFontSize.Property = Button.FontSizeProperty;
                _buttonFontSize.Value = fontSizeButtonLarge;
                _titleFontSize.Property = Label.FontSizeProperty;
                _titleFontSize.Value = _textfontSizeLarge;
                subTitleFontSize.Property = Label.FontSizeProperty;
                subTitleFontSize.Value = _textfontSizeMedium;
                textBodyFontSize.Property = Label.FontSizeProperty;
                textBodyFontSize.Value = _textFontSizeSmall;
            }
            if (Device.Idiom == TargetIdiom.Phone)
            {
                _buttonFontSize.Property = Button.FontSizeProperty;
                _buttonFontSize.Value = fontSizeForButtonMedium;
                _titleFontSize.Property = Label.FontSizeProperty;
                _titleFontSize.Value = _textfontSizeMedium;
                subTitleFontSize.Property = Label.FontSizeProperty;
                subTitleFontSize.Value = _textFontSizeSmall;
                textBodyFontSize.Property = Label.FontSizeProperty;
                textBodyFontSize.Value = _textFontSizeSmall;
            }
        }

        public string FontFamily { get; set; }

        public Style LinkButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = _fontSizeButtonSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("354B60"), Color.White, Color.White)
                                    },
                                }
                            };

                return style;
            }
        }

        public Style DefaultButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.FromHex("77D065")
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 8},
                                    new Setter {Property = Button.TextColorProperty, Value = Color.White},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                }
                            };
                style.Setters.Add(_buttonFontSize);

                return style;
            }
        }

        public Style TitleStyle
        {
            get
            {
                Device.Styles.TitleStyle.Setters.Add(_titleFontSize);
                Device.Styles.TitleStyle.Setters.Add(new Setter
                                                     {
                                                         Property = Label.FontFamilyProperty,
                                                         Value = FontFamily
                                                     });
                return Device.Styles.TitleStyle;
            }
        }

        public Style SubtitleStyle
        {
            get
            {
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.TextColorProperty,
                                                            Value =
                                                                Device.OnPlatform(Color.FromHex("354B60"),
                                                                    Color.FromHex("EDEEF2"), Color.FromHex("77D065"))
                                                        });

                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontFamilyProperty,
                                                            Value = FontFamily
                                                        });
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontSizeProperty,
                                                            Value = _textfontSizeMedium
                                                        });
                Device.Styles.SubtitleStyle.Setters.Add(new Setter
                                                        {
                                                            Property = Label.FontAttributesProperty,
                                                            Value = FontAttributes.Bold
                                                        });
                return Device.Styles.SubtitleStyle;
            }
        }

        public Style BodyStyle
        {
            get
            {
                Device.Styles.BodyStyle.Setters.Add(new Setter {Property = Label.FontFamilyProperty, Value = FontFamily});
                Device.Styles.BodyStyle.Setters.Add(new Setter
                                                    {
                                                        Property = Label.FontSizeProperty,
                                                        Value =
                                                            Device.OnPlatform<double>(7, 12, 12)
                                                    });
                return Device.Styles.BodyStyle;
            }
        }

        public Style CaptionStyle
        {
            get
            {
                Device.Styles.CaptionStyle.Setters.Add(new Setter
                                                       {
                                                           Property = Label.FontFamilyProperty,
                                                           Value = FontFamily
                                                       });
                return Device.Styles.CaptionStyle;
            }
        }


        public Style ListItemDetailTextStyle
        {
            get
            {
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter
                                                                  {
                                                                      Property = Label.FontFamilyProperty,
                                                                      Value = FontFamily
                                                                  });
                Device.Styles.ListItemDetailTextStyle.Setters.Add(new Setter
                                                                  {
                                                                      Property = Label.FontSizeProperty,
                                                                      Value =
                                                                          Device.OnPlatform<double>(7, 12, 12)
                                                                  });
                return Device.Styles.ListItemDetailTextStyle;
            }
        }

        public Style ListItemTextStyle
        {
            get
            {
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.FontFamilyProperty,
                                                                Value = FontFamily
                                                            });
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.FontSizeProperty,
                                                                Value =
                                                                    Device.OnPlatform<double>(8, 15, 15)
                                                            });
                Device.Styles.ListItemTextStyle.Setters.Add(new Setter
                                                            {
                                                                Property = Label.TextColorProperty,
                                                                Value =
                                                                    Device.OnPlatform(Color.FromHex("354B60"),
                                                                        Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"))
                                                            });
                return Device.Styles.ListItemTextStyle;
            }
        }

        public Color BlackBackground
        {
            get { return Color.Black; }
        }

        public Color BrownBackground
        {
            get { return Color.Black; }
        }

        public Color LabelTextColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("354B60"), Color.FromHex("77D065"), Color.FromHex("77D065"));
            }
        }

        public Color MenuBackground
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("ECECEC"),
                        Color.FromHex("1E2225"), Color.FromHex("323232"));
            }
        }

        public Color SearchBackground
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("C6C6CB"), Color.FromHex("1E2225"), Color.FromHex("425961"));
            }
        }

        public Style StyleNavigationTitle
        {
            get
            {
                return Device.Idiom == TargetIdiom.Tablet
                    ? new Style(typeof (Label))
                      {
                          Setters =
                          {
                              new Setter
                              {
                                  Property = Label.FontSizeProperty,
                                  Value =
                                      Device.OnPlatform<double>(12, 12, 12)
                              }
                          }
                      }
                    : new Style(typeof (Label))
                      {
                          Setters =
                          {
                              new Setter
                              {
                                  Property = Label.FontSizeProperty,
                                  Value =
                                      Device.OnPlatform<double>(8, 10, 10)
                              }
                          }
                      };
            }
        }

        public Thickness LayoutThickness
        {
            get
            {
                return Device.OnPlatform(new Thickness(20, 0, 20, 0), new Thickness(20, 0, 20, 0),
                    new Thickness(20, 0, 20, 0));
            }
        }

        public Thickness ListDetailThickness
        {
            get
            {
                return Device.OnPlatform(new Thickness(10, 5, 10, 5), new Thickness(10, 5, 10, 5),
                    new Thickness(10, 5, 10, 5));
            }
        }

        public Style LabelTextDate
        {
            get
            {
                return new Style(typeof (Label))
                       {
                           Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(7, 12, 12)
                               }
                           }
                       };
            }
        }

        public Style LabelLargeTextTitle
        {
            get
            {
                return new Style(typeof (Label))
                       {
                           Setters =
                           {
                               new Setter {Property = Label.FontFamilyProperty, Value = FontFamily},
                               new Setter
                               {
                                   Property = Label.FontSizeProperty,
                                   Value = Device.OnPlatform<double>(10, 16, 16)
                               }
                           }
                       };
            }
        }

        public Color MenuOptionsColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("020202"), Color.FromHex("DDDDDD"), Color.FromHex("77D065"));
            }
        }

        public Color PageBackgroundColor
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("DDDDDD"), Color.FromHex("292929"), Color.FromHex("77D065"));
            }
        }

        public Thickness GridPadding
        {
            get
            {
                return Device.OnPlatform(new Thickness(10, 5, 10, 5), new Thickness(10, 5, 10, 5),
                    new Thickness(10, 5, 10, 5));
            }
        }

        public double TextFontSizeMedium
        {
            get { return _textfontSizeMedium; }
        }

        public double TextFontSizeSmall
        {
            get { return _textFontSizeSmall; }
        }

        public double TextFontSizeLarge
        {
            get { return _textfontSizeLarge; }
        }

        public Color ColorGreenDroidBlueSapphireIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("126180"), Color.FromHex("77D065"), Color.FromHex("77D065"));
            }
        }

        public Color ColorWhiteDroidBlueIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("354B60"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Style ListViewItemButton
        {
            get
            {
                var style = new Style(typeof (Button))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.FromHex("425d78")
                                    },
                                    new Setter {Property = Button.TextColorProperty, Value = Color.White},
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 2},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily}
                                }
                            };
                style.Setters.Add(new Setter
                                  {
                                      Property = Button.FontSizeProperty,
                                      Value = Device.OnPlatform<double>(7, 12, 12)
                                  });
                return style;
            }
        }

        public Color ColorLine
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("354B60"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Color ColorGrayDroidDSkyIos
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("DBE1ED"), Color.FromHex("6D7075"), Color.FromHex("6D7075"));
            }
        }

        public Color ColorWhiteBackground
        {
            get
            {
                return Device.OnPlatform
                    (Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"), Color.FromHex("FFFFFF"));
            }
        }

        public Style MessageContentStyle
        {
            get
            {
                var style = new Style(typeof (InputView))
                            {
                                Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value =
                                            Device.OnPlatform(Color.FromHex("DBE1ED"), Color.FromHex("333333"),
                                                Color.FromHex("333333")),
                                    }
                                }
                            };
                return style;
            }
        }


        public Style MessageNavigationButton
        {
            get
            {
                var style = new Style(typeof(Button))
                {
                    Setters =
                                {
                                    new Setter
                                    {
                                        Property = VisualElement.BackgroundColorProperty,
                                        Value = Color.Transparent
                                    },
                                    new Setter {Property = Button.BorderRadiusProperty, Value = 0},
                                    new Setter {Property = Button.FontFamilyProperty, Value = FontFamily},
                                    new Setter {Property = Button.FontSizeProperty, Value = _fontSizeButtonSmall},
                                    new Setter
                                    {
                                        Property = Button.TextColorProperty,
                                        Value = Device.OnPlatform
                                            (Color.FromHex("354B60"), Color.White, Color.White)
                                    },
                                }
                };

                return style;
            }
        }
#region Images Icons
        public Style CircleImageCommon
        {
            get
            {
                var style = new Style(typeof (CircleImage))
                            {
                                Setters =
                                {
                                    new Setter {Property = Image.AspectProperty, Value = Aspect.AspectFit},
                                    new Setter {Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter {Property = View.VerticalOptionsProperty, Value = LayoutOptions.Center},
                                    new Setter
                                    {
                                        Property = VisualElement.HeightRequestProperty,
                                        Value = Device.Idiom == TargetIdiom.Phone? Device.OnPlatform(40, 80, 70):Device.OnPlatform(100, 110, 70)
                                    },
                                    new Setter
                                    {
                                        Property = VisualElement.WidthRequestProperty,
                                        Value = Device.Idiom == TargetIdiom.Phone? Device.OnPlatform(40, 80, 70):Device.OnPlatform(100, 110, 70)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderColorProperty,
                                        Value = Device.OnPlatform( Color.Silver, Color.Silver,  Color.Silver)
                                    },
                                    new Setter
                                    {
                                        Property = CircleImage.BorderThicknessProperty,
                                        Value = Device.OnPlatform(2, 3, 3)
                                    }
            
                                }
                            };

                return style;
            }
        }
        public string DefaultProfileUserIconName
        {
            get
            {
                return "userdefault3.png"; 
            }            
        }
        public string ProfileMenuItemIcon
        {
            get
            {
                return Device.OS == TargetPlatform.iOS ? "Contact.png" : "userprofile.png";
            }
        }

        public string KnocksMenuITemIcon
        {
            get
            {
                return "padlock.png";
            }
        }
        public string MessageIcon
        {
            get
            {
                return "userprofile.png";
            }
        }

        public string DefaultGroupIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultWallIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultEventIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultNotificationIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultContactIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultActivityIcon
        {
            get
            {
                return "userprofile.png";
            }
        }
        public string DefaultShopIcon
        {
            get
            {
                return "userprofile.png";
            }
        }

        public string RatinGoffIcon
        {
            get
            {
                return "ratingoff.png";
            }
        }


#endregion
    }
}