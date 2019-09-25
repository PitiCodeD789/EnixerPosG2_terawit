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
    public class CashManagePageViewModel : BaseViewModel
    {
        private readonly IShiftService _shiftService;
        public CashManagePageViewModel()
        {
            _shiftService = new ShiftService();
            PayinClickCommand = new Command(PayinClick);
            PayoutClickCommand = new Command(PayoutClick);
            PopupNavigation.Instance.PopAsync();
        }

        private async void PayoutClick(object obj)
        {
            if (Amount != null)
            {
                decimal amount_check = decimal.Parse(Amount);
                if (amount_check > 0)
                {

                    await PopupNavigation.Instance.PushAsync(new Views.Popup.LoadingPopup());
                    ManageCashCommand manage = new ManageCashCommand
                    {
                        Amount = decimal.Parse(Amount),
                        Comment = Comment,
                        ManageCashStatus = Api.ViewModels.Enixer_Enumerations.ManageCashStatus.PayOut,
                        PosUserId = App.UserId,
                        ShiftId = App.OpenShiftId
                    };


                    bool isPayOut = await _shiftService.ManageCashPay(manage);
                    await PopupNavigation.Instance.PopAsync(false);
                    if (isPayOut)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("PayOut Complete", 2);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                        Amount = "";
                        Comment = "";
                    }
                    else
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("PayOut Error", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                }else
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("Input Amount Morethan 0 ", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }


                
            }else
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("Input Amount", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }



        }

        private async void PayinClick(object obj)
        {
            if (Amount != null)
            {
                decimal amount_check = decimal.Parse(Amount);
                if (amount_check > 0)
                {

                    await PopupNavigation.Instance.PushAsync(new Views.Popup.LoadingPopup());
                    ManageCashCommand manage = new ManageCashCommand
                    {
                        Amount = decimal.Parse(Amount),
                        Comment = Comment,
                        ManageCashStatus = Api.ViewModels.Enixer_Enumerations.ManageCashStatus.PayIn,
                        PosUserId = App.UserId,
                        ShiftId = App.OpenShiftId
                    };

                    bool isPayOut = await _shiftService.ManageCashPay(manage);
                    await PopupNavigation.Instance.PopAsync(false);

                    if (isPayOut)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("PayIn Complete", 2);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                        Amount = "";
                        Comment = "";
                    }
                    else
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("PayIn Error", 1);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                }else
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("Input Amount Morethan 0 ", 1);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }

            }else
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("Input Amount", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
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
