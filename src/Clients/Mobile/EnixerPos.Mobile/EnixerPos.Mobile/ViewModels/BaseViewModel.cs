using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
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
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual ICommand BackButton { get; set; }
        public async virtual void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async virtual void ForceLogout()
        {
            
            var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
            if (isClose)
            {
                App.CheckShift = false;
                var logoutData = await _authService.Logout(App.Email);
                if (logoutData == null)
                {
                    CloseApp();
                }
                else if (logoutData.IsError != System.Net.HttpStatusCode.OK)
                {
                    CloseApp();
                }
                else
                {
                    SecureStorage.RemoveAll();
                    await PopupNavigation.Instance.PopAllAsync();
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
            }
            else
            {
                CloseApp();
            }
        }
            
        public async virtual void CloseApp()
        {
            var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
            var logoutData = await _authService.Logout(App.Email);
            SecureStorage.RemoveAll();
            Environment.Exit(0);
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
