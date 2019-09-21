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
        public ItemPopup()
        {
            InitializeComponent();
            selectionView.ItemsSource  = new[]
            {
                "Hot               0.00","Ice              5.00","Blended               10.00"
            };
        }
    }
}