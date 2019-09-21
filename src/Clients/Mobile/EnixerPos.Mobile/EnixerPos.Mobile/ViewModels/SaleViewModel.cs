using AiForms.Renderers;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Components;
using EnixerPos.Service.Models;
using EnixerPos.Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class SaleViewModel
    {
        MockupService _service = new MockupService();
        public SaleViewModel()
        {
            AllMenu = new List<MenuModel>();
            SetItemMenu();
            foreach (var category in AllMenu)
            {
                var grid = new Grid()
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                    },
                    RowDefinitions = new RowDefinitionCollection()
                    {
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                    }
                };

                int row = 0;
                int col = 0;
                if (category != null || category.Items.Count > 0)
                {
                    foreach (var item in category.Items)
                    {
                        if (col%4 == 0 && col != 0)
                        {
                            row++;
                            col = 0;
                        }
                        
                        var button = new Button();
                        button.Text = item.Name;
                        button.HeightRequest = 150;
                        button.WidthRequest = 150;
                        button.HorizontalOptions = LayoutOptions.Center;
                        button.VerticalOptions = LayoutOptions.Center;
                        button.Command = new Command<string>((text) => Application.Current.MainPage.DisplayAlert(item.Name,text,"Ok"));
                        button.CommandParameter = item.Name;
                        grid.Children.Add(button,col,row);
                        col++;
                    }
                }
                TabChildren.Add(new TabItem(category.CategoryName, grid));
            };
        }


        private ObservableCollection<ItemModel> items;
        public ObservableCollection<ItemModel> Drinks
        {
            get { return items; }
            set { items = value; }
        }

        private List<MenuModel> _allMenu;
        public List<MenuModel> AllMenu
        {
            get { return _allMenu; }
            set { _allMenu = value; }
        }

        private List<CategoryModel> _categoriesList;
        public List<CategoryModel> CategoriesList
        {
            get { return _categoriesList; }
            set { _categoriesList = value; }
        }


        void SetItemMenu()
        {
            CategoriesList = _service.GetCategories();
            foreach (var category in CategoriesList)
            {
                AllMenu.Add(new MenuModel()
                {
                    CategoryName = category.Name,
                    Items = new ObservableCollection<ItemModel>()
                });
            }

            ItemsViewModel items = _service.GetItems();
            if (items == null || items.Items.Count == 0)
            {
                Application.Current.MainPage.DisplayAlert("","ไม่พบรายการอาหารในระบบ","OK");
                return;
            }

            foreach (var item in items.Items)
            {
                var isCatExist = AllMenu.Where(a => a.CategoryName == item.CategoryName).FirstOrDefault();
                if (isCatExist == null)
                {
                    AllMenu.Add(new MenuModel()
                    {
                        CategoryName = item.CategoryName
                    });
                }
                AllMenu.Where(a => a.CategoryName == item.CategoryName).FirstOrDefault().Items.Add(item);
            }

        }


        public List<TabItem> TabChildren = new List<TabItem>();


    }
}
