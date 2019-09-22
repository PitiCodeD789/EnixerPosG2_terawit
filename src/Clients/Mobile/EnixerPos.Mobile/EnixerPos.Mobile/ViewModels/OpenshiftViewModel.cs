using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
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
        private readonly IShiftService _shiftService = new ShiftService();
        public OpenshiftViewModel()
        {
            OpenShiftButtonClickCommand = new Command(OpenShiftButtonClick);
            OpenShiftsubmitClickCommand = new Command(OpenShiftsubmitClick);
            countLogout = 0;
        }

        private int countLogout;

        private async void OpenShiftsubmitClick(object obj)
        {
            OpenShiftCommand command = new OpenShiftCommand()
            {
                StartingCash = Decimal.Parse(startingCash),
                UserId = App.UserId,
                StoreEmail = App.Email,
                PosImei = App.DeviceId,
                PosUserId = App.UserId
            };
            var openShiftId = await _shiftService.OpenShift(command);
            if(openShiftId == null)
            {
                countLogout++;
                ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่กะนี้ได้", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                CheckLogout();
            }
            else if (openShiftId.IsError != System.Net.HttpStatusCode.OK || openShiftId.Model == null)
            {
                countLogout++;
                ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถเข้าสู่กะนี้ได้", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                CheckLogout();
            }
            else
            {
                App.OpenShiftId = openShiftId.Model.OpenShiftId;
                App.CheckShift = true;
                await PopupNavigation.Instance.PopAsync(false);
            }
        }

        private void CheckLogout()
        {
            if(countLogout >= 3)
            {
                ForceLogout();
            }
        }

        private async void OpenShiftButtonClick(object obj)
        {

            await PopupNavigation.Instance.PopAsync(false);
            await PopupNavigation.Instance.PushAsync(new Views.Popup.StartCashPopup());
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
