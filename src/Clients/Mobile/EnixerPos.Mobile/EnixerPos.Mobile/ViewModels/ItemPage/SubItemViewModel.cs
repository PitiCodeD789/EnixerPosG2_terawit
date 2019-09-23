using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views.Item;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels.ItemPage
{
    public class SubItemViewModel : BaseViewModel, INotifyPropertyChanged
    {
        ProductService service = new ProductService();

        public SubItemViewModel()
        {
            typePage = Status.InItemPage.Item;
            categoryData = GetCategoryData().Result;
            itemDate = GetItemData().Result;
            InputDataToBinding();
            EditData = new Command<string>(EditDataMethod);
            CreateData = new Command(CreateDataMethod);
            GotoItem = new Command(ItemMethod);
            GotoCategory = new Command(CategoryMethod);
            GotoDiscount = new Command(DiscountMethod);
        }

        private string itemColor;
        public string ItemColor
        {
            get
            {
                return itemColor;
            }
            set
            {
                itemColor = value;
                OnPropertyChanged();
            }
        }

        private string categoryColor;
        public string CategoryColor
        {
            get
            {
                return categoryColor;
            }
            set
            {
                categoryColor = value;
                OnPropertyChanged();
            }
        }

        private string discountColor;
        public string DiscountColor
        {
            get
            {
                return discountColor;
            }
            set
            {
                discountColor = value;
                OnPropertyChanged();
            }
        }

        private bool picker;
        public bool Picker
        {
            get
            {
                return picker;
            }
            set
            {
                picker = value;
                OnPropertyChanged();
            }
        }

        private bool laber;
        public bool Laber
        {
            get
            {
                return laber;
            }
            set
            {
                laber = value;
                OnPropertyChanged();
            }
        }

        private string changeCategory;
        public string ChangeCategory

        {
            get
            {
                return changeCategory;
            }
            set
            {
                changeCategory = value;
                ChangeItem();
                OnPropertyChanged();
            }
        }

        private string namePage;
        public string NamePage
        {
            get
            {
                return namePage;
            }
            set
            {
                namePage = value;
                OnPropertyChanged();
            }
        }

        private List<string> categoryName;
        public List<string> CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged();
            }
        }

        private List<ItemPageModel> theData;
        public List<ItemPageModel> TheData
        {
            get
            {
                return theData;
            }
            set
            {
                theData = value;
                OnPropertyChanged();
            }
        }

        private List<ItemModel> itemDate;

        private List<CategoryModel> categoryData;

        private List<DiscountModel> discountData;

        private Status.InItemPage typePage;

        private async Task<List<ItemModel>> GetItemData()
        {

            List<ItemModel> mock = service.GetItems().Items;

            var result = mock;
            return result;
        }

        private async Task<List<CategoryModel>> GetCategoryData()
        {
            List<CategoryModel> mock = service.GetCategories();

            var result = mock;
            return result;
        }

        private async Task<List<DiscountModel>> GetDiscountData()
        {
            List<DiscountModel> mock = service.GetDiscounts().Discounts;

            var result = mock;
            return result;
        }

        private void InputDataToBinding()
        {
            if(typePage == Status.InItemPage.Item)
            {
                TheData = itemDate.Select(x => new ItemPageModel()
                {
                    Color = x.Color,
                    DataName = x.Name,
                    CountItemVisible = false,
                    Number = x.Price.ToString("#,###.00"),
                    NumberVisible = true
                }).ToList();
                CategoryName = categoryData.Select(x => x.Name).ToList();
                CategoryName.Add("All Items");
                Picker = true;
                Laber = false;
                ItemColor = "#ededed";
                CategoryColor = "#ffffff";
                DiscountColor = "#ffffff";
            }
            else if(typePage == Status.InItemPage.Categories)
            {
                TheData = categoryData.Select(x => new ItemPageModel()
                {
                    Color = x.Color,
                    DataName = x.Name,
                    CountItem = x.CountItem.ToString() + " Items",
                    CountItemVisible = true,
                    NumberVisible = false
                }).ToList();
                NamePage = "Categories";
                Picker = false;
                Laber = true;
                ItemColor = "#ffffff";
                CategoryColor = "#ededed";
                DiscountColor = "#ffffff";
            }
            else
            {
                TheData = discountData.Select(x => new ItemPageModel()
                {
                    Color = "#d6d6d6",
                    DataName = x.DiscountName,
                    CountItemVisible = false,
                    Number = (!x.IsPercentage) ? x.Amount.ToString("#,###.00") : (x.Amount/100).ToString("#%"),
                    NumberVisible = true
                }).ToList();
                NamePage = "Discounts";
                Picker = false;
                Laber = true;
                ItemColor = "#ffffff";
                CategoryColor = "#ffffff";
                DiscountColor = "#ededed";
            }
        }

        public ICommand EditData { get; set; }
        public async void EditDataMethod(string dataName)
        {
            
        }

        public ICommand CreateData { get; set; }
        public async void CreateDataMethod()
        {
            if (typePage == Status.InItemPage.Item)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CreateItem(new CreateItemPageViewModel()));
            }
            else if (typePage == Status.InItemPage.Categories)
            {

                await Application.Current.MainPage.Navigation.PushAsync(new CreateCategoryPage(new CategoryPageViewModel()));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CreateDiscountPage(new CreateDiscountPageViewModel()));
            }
        }

        public ICommand GotoItem { get; set; }
        public void ItemMethod()
        {
            itemDate = GetItemData().Result;
            categoryData = GetCategoryData().Result;
            typePage = Status.InItemPage.Item;
            InputDataToBinding();
        }

        public ICommand GotoCategory { get; set; }
        public void CategoryMethod()
        {
            categoryData = GetCategoryData().Result;
            typePage = Status.InItemPage.Categories;
            InputDataToBinding();
        }

        public ICommand GotoDiscount { get; set; }
        public void DiscountMethod()
        {
            discountData = GetDiscountData().Result;
            typePage = Status.InItemPage.Discount;
            InputDataToBinding();
        }

        private void ChangeItem()
        {
            if(changeCategory == "All Item")
            {
                InputDataToBinding();
            }
            else
            {
                TheData = itemDate.Where(y => y.CategoryName == changeCategory).Select(x => new ItemPageModel()
                {
                    Color = x.Color,
                    DataName = x.Name,
                    CountItemVisible = false,
                    Number = x.Price.ToString("#,###.00"),
                    NumberVisible = true
                }).ToList();
            }
        }
    }
}
