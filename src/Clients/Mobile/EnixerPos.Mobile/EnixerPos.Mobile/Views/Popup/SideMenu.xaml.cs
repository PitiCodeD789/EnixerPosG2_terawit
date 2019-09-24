using EnixerPos.Mobile.ViewModels;
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
    public partial class SideMenu : PopupPage
    {
        public SideMenu()
        {
            InitializeComponent();
        }

        private void Sale_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SaleView());
            PopupNavigation.PopAllAsync();
        }
        private void Receipt_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReceiptsPage());
            PopupNavigation.PopAllAsync();
        }
        private void Shift_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShiftPage(new ViewModels.ShiftPageViewModel()));
            PopupNavigation.PopAllAsync();
        }
        private void Item_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ItemPage());
            PopupNavigation.PopAllAsync();
        }
        private void Logout_Tapped(object sender, EventArgs e)
        {

            BaseViewModel baseViewModel = new BaseViewModel();
            baseViewModel.ForceLogout();
        }

        private void LockButton_Clicked(object sender, EventArgs e)
        {
            BaseViewModel baseViewModel = new BaseViewModel();
            baseViewModel.ForceLogout();
        }
    }
}