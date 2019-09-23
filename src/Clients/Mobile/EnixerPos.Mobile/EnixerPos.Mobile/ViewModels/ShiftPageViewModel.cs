using EnixerPos.Api.ViewModels.Shifts;
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
    public class ShiftPageViewModel : BaseViewModel
    {
        private IShiftService _shiftService = new ShiftService();
        public GetShiftViewModel getShiftView { get; set; }
        public ShiftPageViewModel()
        {
            getShiftView = new GetShiftViewModel
            {
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now.AddHours(12),
                StartingCash = 5005,
                CashPayment = 500,
                Cash = 1500,
                CashRefunds = 0,
                CreditCard = 500,
                DebitCard = 1500,
                Discount = 250,
                Grosssales = 550,
                Paidin = 200,
                Taxes = 145.60m
            };

            ShowShiftListCommand = new Command(ShowShiftList);
            CloseShiftListCommand = new Command(CloseShiftList);


        }

        private void CloseShiftList(object obj)
        {
            var isClose = _shiftService.CloseListShift(App.OpenShiftId);
            if(isClose)
            {
                //App.Current.MainPage.DisplayAlert("ok", "Close Ok", "ok");
                App.CheckShift = false;
                App.Current.MainPage = new NavigationPage(new Views.EnterPin());

            }else
            {
                App.Current.MainPage.DisplayAlert("ok", "Can not close", "ok");
            }
        }

        private async void ShowShiftList(object obj)
        {
            List<GetShiftViewModel> data = _shiftService.GetListShift();
            await PopupNavigation.PushAsync(new Views.Popup.ShiftPopUpPage());
        }

        public ICommand ShowShiftListCommand { get; set; }
        public ICommand CloseShiftListCommand { get; set; }

    }
}
