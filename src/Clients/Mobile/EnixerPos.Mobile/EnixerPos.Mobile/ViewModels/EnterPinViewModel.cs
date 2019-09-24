using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class EnterPinViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IAuthService _authService = new AuthService();

        public EnterPinViewModel()
        {
            pin = "";
            countLogout = 0;
            InputPin = new Command<string>(InputPinMethod);
        }

        private int countLogout;

        private string pin;

        public ICommand InputPin { get; set; }
        public async void InputPinMethod(string value)
        {
            if (value == "Delete")
            {
                if (pin.Length > 0)
                {
                    pin = pin.Remove(pin.Length - 1);
                    int countPin = pin.Length;
                    HintColorChange(countPin);
                }
            }
            else
            {
                bool isExistvalue = Helper.CheckDigitaAndLength(value, 1);
                if (!isExistvalue)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("ค่าที่ใส่ไม่ใช่ตัวเลข", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }
                pin += value;
                int countPin = pin.Length;
                HintColorChange(countPin);
                if (countPin == 4)
                {
                    var refreshToken = await SecureStorage.GetAsync("RefreshToken");
                    var loginData = await _authService.LoginByPin(pin, refreshToken, App.Email);
                    if (loginData == null)
                    {
                        countLogout++;
                        ResetPin();
                        ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่ระบบได้", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                    else if (loginData.IsError != System.Net.HttpStatusCode.OK || loginData.Model == null)
                    {
                        countLogout++;
                        ResetPin();
                        ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่ระบบได้", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                    //else if(countLogout >= 3)
                    //{
                    //    ForceLogout();
                    //}
                    else
                    {
                        await SecureStorage.SetAsync("RefreshToken", loginData.Model.RefreshToken);
                        await SecureStorage.SetAsync("Token", loginData.Model.Token);
                        string testToken = await SecureStorage.GetAsync("Token");
                        App.Email = await SecureStorage.GetAsync("Email");
                        App.StoreName = await SecureStorage.GetAsync("StoreName");
                        App.User = loginData.Model.User;
                        App.UserId = loginData.Model.UserId;
                        int shiftId = loginData.Model.ShiftId;
                        if(shiftId > 0)
                        {
                            App.CheckShift = true;
                            App.OpenShiftId = shiftId;
                        }
                        Application.Current.MainPage = new NavigationPage(new SaleView())
                        {
                            BackgroundColor = Color.White
                        };
                    }
                }
                if (countPin > 4)
                {
                    pin = pin.Substring(0, 3);
                }
            }
        }

        private void ResetPin()
        {
            pin = "";
            int countPin = pin.Length;
            HintColorChange(countPin);
        }

        private void HintColorChange(int length)
        {
            var hasKeyColor = "Gray";
            var hasNoKeyColor = "White";
            if (length == 0)
            {
                PwHint0 = hasNoKeyColor;
                PwHint1 = hasNoKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 1)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasNoKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 2)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 3)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 4)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 5)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 6)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasKeyColor;
                PwHint5 = hasKeyColor;
            }
        }

        // ------------------------------ Propfull ------------------------------

        private string[] _pwHint = new string[] { "White", "White", "White", "White", "White", "White" };
        public string PwHint0
        {
            get { return _pwHint[0]; }
            set { _pwHint[0] = value; OnPropertyChanged(nameof(PwHint0)); }
        }
        public string PwHint1
        {
            get { return _pwHint[1]; }
            set { _pwHint[1] = value; OnPropertyChanged(nameof(PwHint1)); }
        }
        public string PwHint2
        {
            get { return _pwHint[2]; }
            set { _pwHint[2] = value; OnPropertyChanged(nameof(PwHint2)); }
        }
        public string PwHint3
        {
            get { return _pwHint[3]; }
            set { _pwHint[3] = value; OnPropertyChanged(nameof(PwHint3)); }
        }
        public string PwHint4
        {
            get { return _pwHint[4]; }
            set { _pwHint[4] = value; OnPropertyChanged(nameof(PwHint4)); }
        }
        public string PwHint5
        {
            get { return _pwHint[5]; }
            set { _pwHint[5] = value; OnPropertyChanged(nameof(PwHint5)); }
        }
    }
}
