using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.ViewModels;
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
    public partial class ItemPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ItemPopup(object vm)
        {
            InitializeComponent();
            
            BindingContext = vm;
        }
    }
}