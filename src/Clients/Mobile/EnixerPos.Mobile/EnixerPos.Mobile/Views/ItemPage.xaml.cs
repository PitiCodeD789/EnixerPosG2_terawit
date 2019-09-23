using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.ViewModels.ItemPage;
using EnixerPos.Mobile.Views.Item;
using EnixerPos.Mobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
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
    public partial class ItemPage : ContentPage
    {
        public ItemPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            TheBinding.InputDataToBinding();
        }
    }
}