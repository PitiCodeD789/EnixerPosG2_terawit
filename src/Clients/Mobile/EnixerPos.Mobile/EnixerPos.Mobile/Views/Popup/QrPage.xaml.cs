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
    public partial class QrPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        ChargeViewModel _vm;
        public QrPage(ChargeViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = vm;
        }

        private void Cancel_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        private void Confirm_Tapped(object sender, EventArgs e)
        {
            _vm.QrPaymentComplete();
        }
    }
}