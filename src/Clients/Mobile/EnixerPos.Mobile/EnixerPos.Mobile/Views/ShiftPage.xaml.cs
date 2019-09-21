using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Mobile.ViewModels;
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
        ShiftPageViewModel _vM;
        public ShiftPage(ShiftPageViewModel vM)
        {
            InitializeComponent();
           BindingContext = _vM = vM;
            Shiftopened.RightTexLable = vM.getShiftView.CreateDateTime.ToString("dd MMM yyyy HH:mm:ss");
            Startingcash.RightTexLable = vM.getShiftView.StartingCash.ToString("#,#.00");
            Cashpayment.RightTexLable = vM.getShiftView.CashPayment.ToString("#,#.00");
            Cashrefunds.RightTexLable = vM.getShiftView.CashRefunds.ToString("#,#.00");
            Paidin.RightTexLable = vM.getShiftView.Paidin.ToString("#,#.00");
            Paidout.RightTexLable = vM.getShiftView.Paidout.ToString("#,#.00");
            Expectedcashamount.RightTexLable = vM.getShiftView.ExpectedCashAmount.ToString("#,#.00");
            Grosssales.RightTexLable = vM.getShiftView.Grosssales.ToString("#,#.00");
            Refunds.RightTexLable = vM.getShiftView.Refunds.ToString("#,#.00");
            Discount.RightTexLable = vM.getShiftView.Discount.ToString("#,#.00");
            Netsales.RightTexLable = vM.getShiftView.NetSales.ToString("#,#.00");
            Cash.RightTexLable = vM.getShiftView.Cash.ToString("#,#.00");
            DebitCard.RightTexLable = vM.getShiftView.DebitCard.ToString("#,#.00");
            CreditCard.RightTexLable = vM.getShiftView.CreditCard.ToString("#,#.00");
            QRCODE.RightTexLable = vM.getShiftView.QRCode.ToString("#,#.00");
            Taxes.RightTexLable = vM.getShiftView.Taxes.ToString("#,#.00");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
              await PopupNavigation.PushAsync(new Popup.ShiftPopUpPage());
           // await PopupNavigation.PushAsync(new Popup.OpenTicketsPopup());

        }


    }
}