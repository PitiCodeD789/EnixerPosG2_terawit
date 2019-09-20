using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
                {
                    new MainPageMasterMenuItem { Id = 0, Title = "Sale", ImageSource = "icon_cart" },
                    new MainPageMasterMenuItem { Id = 1, Title = "Receipts", ImageSource = "icon_receipt" },
                    new MainPageMasterMenuItem { Id = 2, Title = "Shift", ImageSource = "icon_clock" },
                    new MainPageMasterMenuItem { Id = 3, Title = "Item", ImageSource = "icon_list" },
                    new MainPageMasterMenuItem { Id = 4, Title = "Setting", ImageSource = "icon_gear" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}