﻿using System;
using Xamarin.Forms;

namespace BeginMobile.Utils
{
    /// <summary>
    /// The check box.
    /// </summary>
    public class CheckBox : View
    {
        /// <summary>
        /// The checked state property.
        /// </summary>
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create<CheckBox, bool>(
                p => p.Checked, false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

        /// <summary>
        /// The checked text property.
        /// </summary>
        public static readonly BindableProperty CheckedTextProperty =
            BindableProperty.Create<CheckBox, string>(
                p => p.CheckedText, string.Empty, BindingMode.TwoWay);

        /// <summary>
        /// The unchecked text property.
        /// </summary>
        public static readonly BindableProperty UncheckedTextProperty =
            BindableProperty.Create<CheckBox, string>(
                p => p.UncheckedText, string.Empty);

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty DefaultTextProperty =
            BindableProperty.Create<CheckBox, string>(
                p => p.Text, string.Empty);

        /// <summary>
        /// Identifies the TextColor bindable property.
        /// </summary>
        /// 
        /// <remarks/>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create<CheckBox, Color>(
                p => p.TextColor, Device.OnPlatform(Color.FromHex("354B60"),Color.FromHex("EDEEF2"), Color.FromHex("77D065")));

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create<CheckBox, double>(
                p => p.FontSize, -1);

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create<CheckBox, string>(
                p => p.FontName, string.Empty);


        /// <summary>
        /// The checked changed event.
        /// </summary>
        public event Action<bool> CheckedChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }

            set
            {
                if (Checked != value)
                {
                    SetValue(CheckedProperty, value);
                    if (CheckedChanged != null) CheckedChanged.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the checked text.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string CheckedText
        {
            get
            {
                return (string)GetValue(CheckedTextProperty);
            }

            set
            {
                this.SetValue(CheckedTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string UncheckedText
        {
            get
            {
                return (string)GetValue(UncheckedTextProperty);
            }

            set
            {
                SetValue(UncheckedTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string DefaultText
        {
            get
            {
                return (string)GetValue(DefaultTextProperty);
            }

            set
            {
                SetValue(DefaultTextProperty, value);
            }
        }

        public Color TextColor
        {
            get
            {
                return (Color)GetValue(TextColorProperty);
            }

            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get
            {
                return (double)GetValue(FontSizeProperty);
            }
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get
            {
                return (string)GetValue(FontNameProperty);
            }
            set
            {
                SetValue(FontNameProperty, value);
            }
        }
        public string Text
        {
            get
            {
                return this.Checked
                    ? (string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText)
                        : (string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText);
            }
        }

        /// <summary>
        /// Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnCheckedPropertyChanged(BindableObject bindable, bool oldvalue, bool newvalue)
        {
            var checkBox = (CheckBox)bindable;
            checkBox.Checked = newvalue;
        }
    }
}
