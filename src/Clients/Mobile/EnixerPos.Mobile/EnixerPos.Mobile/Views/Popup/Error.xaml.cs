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
    public partial class Error : Rg.Plugins.Popup.Pages.PopupPage
    {
        public Error(ErrorViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}