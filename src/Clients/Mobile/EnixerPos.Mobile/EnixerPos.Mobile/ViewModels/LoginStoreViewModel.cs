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
    public class LoginStoreViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IAuthService _authService = new AuthService();

        public LoginStoreViewModel()
        {
            email = "";
            password = "";
            GotoLogin = new Command(LoginByStore);
            GotoForgotPass = new Command(ForgotPasswordMethod);
            GotoRegis = new Command(RegisterMethod);
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return
                    password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand GotoLogin { get; set; }
        public async void LoginByStore()
        {
            bool isEmail = Helper.CheckEmailFormat(email);
            if (!isEmail)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("E-Mail ไม่ถูกต้อง", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
            else
            {
                var loginData = await _authService.Login(email, password);
                if (loginData == null)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่ระบบได้", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }
                else
                {
                    if (loginData.IsError != System.Net.HttpStatusCode.OK || loginData.Model == null)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่ระบบได้", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                    else
                    {
                        await SecureStorage.SetAsync("RefreshToken", loginData.Model.RefreshToken);
                        await SecureStorage.SetAsync("Email", email);
                        await SecureStorage.SetAsync("StoreName", loginData.Model.StoreName);
                        await Application.Current.MainPage.Navigation.PushAsync(new EnterPin());
                    }
                }
            }
        }

        public ICommand GotoForgotPass { get; set; }
        public async void ForgotPasswordMethod()
        {

        }

        public ICommand GotoRegis { get; set; }
        public async void RegisterMethod()
        {

        }

    }
}
