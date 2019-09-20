﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pos_EntryView : ContentView
    {
        public Pos_EntryView()
        {
            InitializeComponent();
            mEntry.BindingContext = this;
        }

        #region IsPassword
        private bool isPassword;

        public bool IsPassword
        {
            get { return isPassword; }
            set
            {
                isPassword = value;
                mEntry.IsPassword = isPassword;
            }
        }
        #endregion

        #region Placeholder
        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;
                mEntry.Placeholder = placeholder;
            }
        }

        #endregion

        #region TextEntry
        public string TextEntry
        {
            get { return (string)GetValue(TextEntryProperty); }
            set { SetValue(TextEntryProperty, value); }
        }

        public static readonly BindableProperty TextEntryProperty =
            BindableProperty.Create(
                propertyName: "TextEntry",
                returnType: typeof(string),
                declaringType: typeof(Pos_EntryView),
                defaultBindingMode: BindingMode.TwoWay); //คนที่ประกาศ
        #endregion

        #region KeyboardType
        private Keyboard keyboardType;

        public Keyboard KeyboardType
        {
            get { return keyboardType; }
            set
            {
                keyboardType = value;
                mEntry.Keyboard = keyboardType;
            }
        }
        #endregion

        #region Length
        private int textLength;

        public int TextLength
        {
            get { return textLength; }
            set
            {
                textLength = value;
                mEntry.MaxLength = textLength;
            }
        }

        #endregion

        #region Placeholder
        private TextAlignment horizontalTextAlignment;

        public TextAlignment HorizontalTextAlignment
        {
            get { return horizontalTextAlignment; }
            set
            {
                horizontalTextAlignment = value;
                mEntry.HorizontalTextAlignment = horizontalTextAlignment;
            }
        }

        #endregion
    }
}