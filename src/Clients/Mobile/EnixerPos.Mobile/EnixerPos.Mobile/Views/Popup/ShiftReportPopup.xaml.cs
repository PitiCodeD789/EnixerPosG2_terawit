using EnixerPos.Api.ViewModels.Shifts;
using Rg.Plugins.Popup.Pages;
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
           
            Shiftopened.RightTexLable = vM.CreateDateTime.ToString("dd MMM YYYY HH:mm:ss");
            Startingcash.RightTexLable = vM.StartingCash.ToString("#,#.00");
            Cashpayment.RightTexLable = vM.CashPayment.ToString("#,#.00");
            Cashrefunds.RightTexLable = vM.CashRefunds.ToString("#,#.00");
            Paidin.RightTexLable = vM.Paidin.ToString("#,#.00");
            Paidout.RightTexLable = vM.Paidout.ToString("#,#.00");
            Expectedcashamount.RightTexLable = vM.ExpectedCashAmount.ToString("#,#.00");
            Grosssales.RightTexLable = vM.Grosssales.ToString("#,#.00");
            Refunds.RightTexLable = vM.Refunds.ToString("#,#.00");
            Discount.RightTexLable = vM.Discount.ToString("#,#.00");
        }
    }
}