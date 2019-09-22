using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.ViewModels.ItemPage;
using EnixerPos.Mobile.Views.Item;
using EnixerPos.Mobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
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
    public partial class ItemPage : ContentPage
    {
        private ItemMainViewModel vm;
        private ContentView subItem;
        private ContentView subCategory;
        private ContentView subDiscount;
        Label label = new Label()
        {
            FontSize = 28
        };
        Picker picker = new Picker()
        {
            Title = "All Items",
            TitleColor = Color.Black,
            FontSize = 28,
            TextColor = Color.Black
        };
        public ItemPage()
        {
            InitializeComponent();
            vm = new ItemMainViewModel();
            subItem = new ItemItem(new SubItemViewModel());
            subCategory = new ItemItem(new SubCategoriesViewModel());
            subDiscount = new ItemItem(new SubDiscountViewModel());
            BindingContext = vm;
            theItem.BackgroundColor = Color.FromHex("#ededed");
            theCategories.BackgroundColor = Color.White;
            theDiscount.BackgroundColor = Color.White;
            //ListItem.Children.Add(subItem);
            picker.ItemsSource = vm.CategoryName;
            theTitle.Children.Add(picker);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            theItem.BackgroundColor = Color.FromHex("#ededed");
            theCategories.BackgroundColor = Color.White;
            theDiscount.BackgroundColor = Color.White;
            ListItem.Children.Remove(subItem);
            ListItem.Children.Remove(subCategory);
            ListItem.Children.Remove(subDiscount);
            ListItem.Children.Add(subItem);
            theTitle.Children.Remove(picker);
            theTitle.Children.Remove(label);
            picker.ItemsSource = vm.CategoryName;
            theTitle.Children.Add(picker);
        }

        private void Categories_Tapped(object sender, EventArgs e)
        {
            theItem.BackgroundColor = Color.White;
            theCategories.BackgroundColor = Color.FromHex("#ededed");
            theDiscount.BackgroundColor = Color.White;
            ListItem.Children.Remove(subItem);
            ListItem.Children.Remove(subCategory);
            ListItem.Children.Remove(subDiscount);
            ListItem.Children.Add(subCategory);
            theTitle.Children.Remove(picker);
            theTitle.Children.Remove(label);
            label.Text = "Categories";
            theTitle.Children.Add(label);
        }

        private void Discount_Tapped(object sender, EventArgs e)
        {
            theItem.BackgroundColor = Color.White;
            theCategories.BackgroundColor = Color.White;
            theDiscount.BackgroundColor = Color.FromHex("#ededed");
            ListItem.Children.Remove(subItem);
            ListItem.Children.Remove(subCategory);
            ListItem.Children.Remove(subDiscount);
            ListItem.Children.Add(subDiscount);
            theTitle.Children.Remove(picker);
            theTitle.Children.Remove(label);
            label.Text = "Discount";
            theTitle.Children.Add(label);
        }
    }
}