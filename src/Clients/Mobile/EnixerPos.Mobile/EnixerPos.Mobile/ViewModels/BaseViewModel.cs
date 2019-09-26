using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly IShiftService _shiftService = new ShiftService();
        private readonly IAuthService _authService = new AuthService();
        public BaseViewModel()
        {
            BackButton = new Command(BackPageMethod);
            ClosePopupCommand = new Command(ClosePopup);
        }

        private void ClosePopup(object obj)
        {
            PopupNavigation.Instance.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual ICommand BackButton { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public async virtual void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
      
        public async virtual void NomalLogout()
        {
            //var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
            //if (isClose)
            //{
                App.CheckShift = false;
                var logoutData = await _authService.Logout(App.Email);
                if (logoutData == null)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถออกจากระบบได้", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }
                else if (logoutData.IsError != System.Net.HttpStatusCode.OK)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถออกจากระบบได้", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }
                else
                {
                    SecureStorage.RemoveAll();
                    await PopupNavigation.Instance.PopAllAsync();
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
            //}
            //else
            //{
            //    ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถปิดกะได้", 1);
            //    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            //}
        }

        public async virtual void ForceLogout()
        {
            try
            {
                var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
                if (isClose)
                {
                    App.CheckShift = false;
                    var logoutData = await _authService.Logout(App.Email);
                    if (logoutData == null)
                    {
                        SecureStorage.RemoveAll();
                        Application.Current.MainPage = new NavigationPage(new Login());
                    }
                    else if (logoutData.IsError != System.Net.HttpStatusCode.OK)
                    {
                        SecureStorage.RemoveAll();
                        Application.Current.MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        SecureStorage.RemoveAll();
                        await PopupNavigation.Instance.PopAllAsync();
                        ErrorViewModel errorViewModel = new ErrorViewModel("กรุณาเข้าสู่ระบบใหม่อีกครั้ง", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                        Application.Current.MainPage = new NavigationPage(new Login());
                    }
                }
                else
                {
                    SecureStorage.RemoveAll();
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
            }
            catch(Exception e)
            {
                SecureStorage.RemoveAll();
                Application.Current.MainPage = new NavigationPage(new Login());
            }
        }

        public void CheckUnauthorized(System.Net.HttpStatusCode httpStatus)
        {
            if (httpStatus == System.Net.HttpStatusCode.Unauthorized)
            {
                ForceLogout();
            }
        }

        public async virtual void CloseApp()
        {
            try
            {
                var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
                var logoutData = await _authService.Logout(App.Email);
                SecureStorage.RemoveAll();
                Environment.Exit(0);
            }
            catch(Exception e)
            {
                SecureStorage.RemoveAll();
                Environment.Exit(0);
            }
            
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
