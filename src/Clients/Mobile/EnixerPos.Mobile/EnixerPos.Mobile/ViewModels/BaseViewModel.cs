using EnixerPos.Mobile.Views;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
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
            SecureStorage.RemoveAll();
            var logoutData = await _authService.Logout();
            if (logoutData == null)
            {
                CloseApp();
            }
            if (logoutData.IsError != System.Net.HttpStatusCode.OK || logoutData.Model == null)
            {
                CloseApp();
            }
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        public async virtual void CloseApp()
        {
            SecureStorage.RemoveAll();
            var logoutData = await _authService.Logout();
            Environment.Exit(0);
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
