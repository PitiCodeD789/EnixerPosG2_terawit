using AiForms.Renderers;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class SaleViewModel
    {
        public SaleViewModel()
        {
            Drinks = new ObservableCollection<ItemModel>() {
                new ItemModel() { ItemName = "1", CategoryName = "Cat"}, new ItemModel() { ItemName = "2" }, new ItemModel() { ItemName = "3" },
                new ItemModel() { ItemName = "1"}, new ItemModel() { ItemName = "2" }, new ItemModel() { ItemName = "3" },
                new ItemModel() { ItemName = "1"}, new ItemModel() { ItemName = "2" }, new ItemModel() { ItemName = "3" }
            };

            //getCategory
            var categoriesList = new List<CategoryModel>() { new CategoryModel() { Name = "Drinks"}, new CategoryModel() { Name = "Soup"} };
            foreach (var category in categoriesList)
            {
                var grid = new Grid();
                int row = 0;
                int col = 0;
                if (Drinks!=null)
                {
                    foreach (var item in Drinks)
                    {
                        if (col%4 == 0 && col != 0)
                        {
                            row++;
                            col = 0;
                        }
                        
                        var button = new Button();
                        button.Text = item.ItemName;
                        button.HeightRequest = 150;
                        button.WidthRequest = 150;
                        button.HorizontalOptions = LayoutOptions.Center;
                        button.VerticalOptions = LayoutOptions.Center;
                        button.Command = new Command<string>((text) => Application.Current.MainPage.DisplayAlert(item.ItemName,text,"Ok"));
                        button.CommandParameter = item.ItemName;
                        grid.Children.Add(button,col,row);
                        col++;
                    }
                }
                TabChildren.Add(new TabItem(category.Name, grid));
            };
        }


        private ObservableCollection<ItemModel> items;

        public ObservableCollection<ItemModel> Drinks
        {
            get { return items; }
            set { items = value; }
        }


        public List<TabItem> TabChildren = new List<TabItem>();


    }
}
