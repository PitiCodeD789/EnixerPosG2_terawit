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
    public partial class CashManagePage : ContentPage
    {
        public CashManagePage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await PopupNavigation.PushAsync(new Popup.OpenShiftButtonPopup());
            // await PopupNavigation.PushAsync(new Popup.OpenTicketsPopup());

        }
    }
}