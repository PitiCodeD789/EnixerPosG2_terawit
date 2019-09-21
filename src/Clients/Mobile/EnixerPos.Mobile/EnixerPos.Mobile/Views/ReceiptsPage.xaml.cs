using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptsPage : ContentPage
    {
        PopupMenu PopupMenu { get; set; }
        public ReceiptsPage()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupMenu = new PopupMenu();
            PopupMenu.ItemsSource =
                 new[] {"Save"};
            PopupMenu.OnItemSelected += PopupMenu_OnItemSelected;
            PopupMenu.ShowPopup(sender as View);
        }
        void PopupMenu_OnItemSelected(string item)
        {
            DisplayAlert("แจ้ง", "Save แล้ว", "ตกลง");
        }
    }
}