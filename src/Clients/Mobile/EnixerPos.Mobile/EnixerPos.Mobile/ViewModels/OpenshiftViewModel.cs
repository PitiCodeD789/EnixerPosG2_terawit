using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class OpenshiftViewModel : BaseViewModel
    {
        public OpenshiftViewModel()
        {
            OpenShiftButtonClickCommand = new Command(OpenShiftButtonClick);
            OpenShiftsubmitClickCommand = new Command(OpenShiftsubmitClick);
        }

        private async void OpenShiftsubmitClick(object obj)
        {
           await PopupNavigation.PopAsync();
        }

        private async void OpenShiftButtonClick(object obj)
        {
             PopupNavigation.PopAsync(false);
            await PopupNavigation.PushAsync(new Views.Popup.StartCashPopup());
        }

        public ICommand OpenShiftButtonClickCommand { get; set; }
        public ICommand OpenShiftsubmitClickCommand { get; set; }

        private string startingCash;

        public string StartingCash
        {
            get { return startingCash; }
            set { startingCash = value; }
        }


    }
}
