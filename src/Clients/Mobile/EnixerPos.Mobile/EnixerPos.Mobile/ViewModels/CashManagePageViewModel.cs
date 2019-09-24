using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CashManagePageViewModel : BaseViewModel
    {
        private readonly IShiftService _shiftService;
        public CashManagePageViewModel()
        {
            _shiftService = new ShiftService();
            PayinClickCommand = new Command(PayinClick);
            PayoutClickCommand = new Command(PayoutClick);

        }

        private async void PayoutClick(object obj)
        {
            ManageCashCommand manage = new ManageCashCommand
            {
                Amount = decimal.Parse(Amount),
                Comment = Comment,
                ManageCashStatus = Api.ViewModels.Enixer_Enumerations.ManageCashStatus.PayOut,
                PosUserId = App.UserId,
                ShiftId = App.OpenShiftId
            };
            bool isPayOut = await _shiftService.ManageCashPay(manage);
            if(isPayOut)
            {
                App.Current.MainPage.DisplayAlert("ok", "ok", "ok");
            }else
            {
                App.Current.MainPage.DisplayAlert("ok", "ok", "Error");
            }


        }

        private async void PayinClick(object obj)
        {
            ManageCashCommand manage = new ManageCashCommand
            {
                Amount = decimal.Parse(Amount),
                Comment = Comment,
                ManageCashStatus = Api.ViewModels.Enixer_Enumerations.ManageCashStatus.PayIn,
                PosUserId = App.UserId,
                ShiftId = App.OpenShiftId
            };
            bool isPayOut = await _shiftService.ManageCashPay(manage);
            if (isPayOut)
            {
                App.Current.MainPage.DisplayAlert("ok", "ok", "ok");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("ok", "ok", "Error");
            }

        }

        public ICommand PayinClickCommand { get; set; }
        public ICommand PayoutClickCommand { get; set; }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged("Comment"); }
        }
    }
}
