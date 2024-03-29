﻿using EnixerPos.Mobile.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShiftPopUpPage : PopupPage
    {
        ShiftPopUpPageViewModel _vM;
        public ShiftPopUpPage(ShiftPopUpPageViewModel vM)
        {
            InitializeComponent();
            BindingContext = _vM = vM;
        }
    }
}