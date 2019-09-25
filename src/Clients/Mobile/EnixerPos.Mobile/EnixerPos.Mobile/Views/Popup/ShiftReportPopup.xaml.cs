using EnixerPos.Api.ViewModels.Shifts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShiftReportPopup : PopupPage
    {
        GetShiftViewModel _vM;
        public ShiftReportPopup(GetShiftViewModel vM)
        {
            InitializeComponent();
            BindingContext = _vM = vM;
            decimal expectedcashamount = _vM.StartingCash + _vM.CashPayment + _vM.Paidin - _vM.Refunds - _vM.Paidout;
            decimal netsell = _vM.CashPayment + _vM.QRCode + _vM.DebitCard + _vM.CreditCard;
            decimal grosssales = netsell + _vM.Refunds + _vM.Discount;         
            Shiftopened.RightTexLable = vM.CreateDateTime.ToLocalTime().ToString("dd MMM yyyy HH:mm:ss");
            Startingcash.RightTexLable = vM.StartingCash.ToString("#,0.00");
            Cashpayment.RightTexLable = vM.CashPayment.ToString("#,0.00");
            Cashrefunds.RightTexLable = vM.CashRefunds.ToString("#,0.00");
            Paidin.RightTexLable = vM.Paidin.ToString("#,0.00");
            Paidout.RightTexLable = vM.Paidout.ToString("#,0.00");
            Expectedcashamount.RightTexLable = expectedcashamount.ToString("#,0.00");
            Grosssales.RightTexLable = grosssales.ToString("#,0.00");
            Refunds.RightTexLable = vM.Refunds.ToString("#,0.00");
            Discount.RightTexLable = vM.Discount.ToString("#,0.00");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          await  PopupNavigation.Instance.PopAsync();
        }
    }
}