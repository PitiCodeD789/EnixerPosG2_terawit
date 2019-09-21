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
        Label label = new Label()
        {
            FontSize = 28
        };
        Picker picker = new Picker()
        {
            Title = "All Items",
            FontSize = 28,
            TextColor = Color.Black
        };
        public ItemPage()
        {
            InitializeComponent();
            FirstItem();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }

        private void Item_Tapped(object sender, EventArgs e)
        {
            FirstItem();
        }

        private void FirstItem()
        {
            theItem.BackgroundColor = Color.FromHex("#ededed");
            theCategories.BackgroundColor = Color.White;
            theDiscount.BackgroundColor = Color.White;
            ListItem.Children.Remove(new ItemItem());
            ListItem.Children.Add(new ItemItem());
            theTitle.Children.Remove(picker);
            theTitle.Children.Remove(label);
            theTitle.Children.Add(picker);
        }

        private void Categories_Tapped(object sender, EventArgs e)
        {
            theItem.BackgroundColor = Color.White;
            theCategories.BackgroundColor = Color.FromHex("#ededed");
            theDiscount.BackgroundColor = Color.White;
            ListItem.Children.Remove(new ItemItem());
            ListItem.Children.Add(new ItemItem());
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
            ListItem.Children.Remove(new ItemItem());
            ListItem.Children.Add(new ItemItem());
            theTitle.Children.Remove(picker);
            theTitle.Children.Remove(label);
            label.Text = "Discount";
            theTitle.Children.Add(label);
        }
    }
}