using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class SettingPageViewModel : BaseViewModel
    {
        public SettingPageViewModel()
        {
            LogOutClickCommand = new Command(LogOutClick);
        }

        private async void LogOutClick(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new Views.Popup.LoadingPopup());
            NomalLogout();
        }

        public ICommand LogOutClickCommand { get; set; }
        public string UserEmail { get; set; } = App.Email;

        
    }
}
