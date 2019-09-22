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
        public QrPage(object vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}