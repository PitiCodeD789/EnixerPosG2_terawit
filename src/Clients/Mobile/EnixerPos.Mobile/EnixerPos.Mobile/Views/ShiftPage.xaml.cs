using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShiftPage : ContentPage
    {
        private readonly IShiftService _shiftService;
        ShiftPageViewModel _vM;
        public ShiftPage(ShiftPageViewModel vM)
        {
            InitializeComponent();
            _shiftService = new ShiftService();
           BindingContext = _vM = vM;
         
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }

        protected async override void OnAppearing()
        {
           
            base.OnAppearing();

           // GetShiftViewModel data = _shiftService.GetShiftDetail(App.OpenShiftId, App.UserId);
            var shiftData = _shiftService.GetShiftDetail(App.OpenShiftId, App.UserId);
            if(shiftData == null)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถดูข้อมูลได้", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
            else if(shiftData.IsError == System.Net.HttpStatusCode.Unauthorized)
            {
                BaseViewModel baseViewModel = new BaseViewModel();
                baseViewModel.ForceLogout();
            }
            else if(shiftData.IsError != System.Net.HttpStatusCode.OK)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("ไม่สามารถดูข้อมูลได้", 1);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
            else
            {
                _vM.GetShiftView = shiftData.Model;

                decimal expectedcashamount = _vM.GetShiftView.StartingCash + _vM.GetShiftView.CashPayment + _vM.GetShiftView.Paidin - _vM.GetShiftView.Refunds - _vM.GetShiftView.Paidout;
                decimal netsell = _vM.GetShiftView.CashPayment + _vM.GetShiftView.QRCode + _vM.GetShiftView.DebitCard + _vM.GetShiftView.CreditCard;
                decimal grosssales = netsell + _vM.GetShiftView.Refunds + _vM.GetShiftView.Discount;
                decimal taxes = netsell - (netsell * 100m / 107m);
                Shiftopened.RightTexLable = _vM.GetShiftView.CreateDateTime.ToLocalTime().ToString("dd MMM yyyy HH:mm:ss");
                Startingcash.RightTexLable = _vM.GetShiftView.StartingCash.ToString("#,0.00");
                Cashpayment.RightTexLable = _vM.GetShiftView.CashPayment.ToString("#,0.00");
                Cashrefunds.RightTexLable = _vM.GetShiftView.CashRefunds.ToString("#,0.00");
                Paidin.RightTexLable = _vM.GetShiftView.Paidin.ToString("#,0.00");
                Paidout.RightTexLable = _vM.GetShiftView.Paidout.ToString("#,0.00");
                Expectedcashamount.RightTexLable = expectedcashamount.ToString("#,0.00");
                Grosssales.RightTexLable = grosssales.ToString("#,0.00");
                Refunds.RightTexLable = _vM.GetShiftView.Refunds.ToString("#,0.00");
                Discount.RightTexLable = _vM.GetShiftView.Discount.ToString("#,0.00");
                Netsales.RightTexLable = netsell.ToString("#,0.00");
                Cash.RightTexLable = _vM.GetShiftView.CashPayment.ToString("#,0.00");
                DebitCard.RightTexLable = _vM.GetShiftView.DebitCard.ToString("#,0.00");
                CreditCard.RightTexLable = _vM.GetShiftView.CreditCard.ToString("#,0.00");
                QRCODE.RightTexLable = _vM.GetShiftView.QRCode.ToString("#,0.00");
                Taxes.RightTexLable = taxes.ToString("#,0.00");

            }
        }
    }
}
