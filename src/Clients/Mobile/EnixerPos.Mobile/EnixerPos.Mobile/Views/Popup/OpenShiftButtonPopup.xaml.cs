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
    public partial class OpenShiftButtonPopup : PopupPage
    {
        public OpenShiftButtonPopup()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
               return false;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}