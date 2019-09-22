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
    public partial class OpenTicketsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public OpenTicketsPopup(object vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}