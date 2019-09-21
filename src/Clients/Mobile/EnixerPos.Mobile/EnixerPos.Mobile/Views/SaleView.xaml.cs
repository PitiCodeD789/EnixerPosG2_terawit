using DLToolkit.Forms.Controls;
using EnixerPos.Mobile.ViewModels;
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
    public partial class SaleView : ContentPage
    {
        public SaleView()
        {
            SaleViewModel vm = new SaleViewModel();
            InitializeComponent();
            BindingContext = vm;
            foreach (var itemSource in vm.TabChildren)
            {
                TabView.AddTab(itemSource);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }
    }
}