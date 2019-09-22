using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.ViewModels;
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
    public partial class ChargePage : ContentPage
    {
        public ChargePage(ReceiptViewModel receipt)
        {
            InitializeComponent();
            BindingContext = new ChargeViewModel(receipt);
        }

        private void Back_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}