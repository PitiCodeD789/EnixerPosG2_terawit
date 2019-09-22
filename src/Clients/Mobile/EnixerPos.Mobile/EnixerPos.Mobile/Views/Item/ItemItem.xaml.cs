using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemItem : ContentView
    {
        public ItemItem(object vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}