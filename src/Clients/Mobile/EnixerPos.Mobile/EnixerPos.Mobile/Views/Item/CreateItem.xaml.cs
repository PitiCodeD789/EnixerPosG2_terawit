using EnixerPos.Mobile.ViewModels;
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
    public partial class CreateItem : ContentPage
    {
        CreateItemPageViewModel _vM;
        public CreateItem(CreateItemPageViewModel vM)
        {
            InitializeComponent();
            BindingContext = _vM = vM;
        }
    }
}