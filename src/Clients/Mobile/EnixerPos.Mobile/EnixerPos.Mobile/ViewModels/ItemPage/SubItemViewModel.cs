using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views.Item;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
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
        IProductService _service = new ProductService();

        public SubItemViewModel()
        {
            typePage = Status.InItemPage.Item;
            InputDataToBinding();
            CreateData = new Command(CreateDataMethod);
            GotoItem = new Command(ItemMethod);
            GotoCategory = new Command(CategoryMethod);
            GotoDiscount = new Command(DiscountMethod);
            ShowSerachEntityCommand = new Command(ShowSerachEntity);
        }

        private void ShowSerachEntity(object obj)
        {
            IsEntityVisible = !IsEntityVisible;
            if(IsEntityVisible)
            {
                ImageName = "iconsxbox";
            }else
            {
                ImageName = "icon_search_2";
            }
        }

        public ICommand ShowSerachEntityCommand { get; set; }

        private bool isEntityVisible = true;

        public bool IsEntityVisible
        {
            get { return isEntityVisible; }
            set { isEntityVisible = value; OnPropertyChanged(); }
        }

        private string imageName = "icon_search_2";

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; OnPropertyChanged(); }
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
                if (changeCategory != value)
                {
                    changeCategory = value;
                    ChangeItem();
                    OnPropertyChanged();
                }
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

        private ItemPageModel selectedData;
        public ItemPageModel SelectedData
        {
            get
            {
                return selectedData;
            }
            set
            {
                if ( value != null)
                {
                    selectedData = value;
                    OnPropertyChanged();
                    EditDataMethod();
                }
                
            }
        }


        private List<ItemModel> itemDate;

        private List<CategoryModel> categoryData;

        private List<DiscountModel> discountData;

        private Status.InItemPage typePage;

        private async Task<List<ItemModel>> GetItemData()
        {

            List<ItemModel> mock = _service.GetItems().Items;

            var result = mock;
            return result;
        }

        private async Task<List<CategoryModel>> GetCategoryData()
        {
            try
            {
                var result = new List<CategoryModel>();
                var cate = _service.GetAllCategories();
                if (cate.Result != null)
                {
                    result = cate.Result.Categories;
                }
                return result;
            }
            catch (Exception)
            {

                return new List<CategoryModel>();
            }
            
        }

        private async Task<List<DiscountModel>> GetDiscountData()
        {
            List<DiscountModel> mock = _service.GetDiscounts().Discounts;

            var result = mock;
            return result;
        }

        public async void InputDataToBinding()
        {
            if(typePage == Status.InItemPage.Item)
            {
                categoryData = await GetCategoryData();
                itemDate = await GetItemData();
                TheData = itemDate.Select(x => new ItemPageModel()
                {
                    Id = x.Id,
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
                categoryData = await GetCategoryData();
                TheData = categoryData.Select(x => new ItemPageModel()
                {
                    Id = x.Id,
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
            else if (typePage == Status.InItemPage.Discount)
            {
                discountData = GetDiscountData().Result;
                TheData = discountData.Select(x => new ItemPageModel()
                {
                    Id = x.Id,
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

        private async void EditDataMethod()
        {
            if (typePage == Status.InItemPage.Item)
            {
                var item = itemDate.Where(x => x.Id == SelectedData.Id).FirstOrDefault();
                await Application.Current.MainPage.Navigation.PushAsync
                    (new CreateItem(new CreateItemPageViewModel(item)));
            }
            else if (typePage == Status.InItemPage.Categories)
            {
                var category = categoryData.Where(x => x.Id == SelectedData.Id).FirstOrDefault();
                await Application.Current.MainPage.Navigation.PushAsync
                    (new CreateCategoryPage(new CategoryPageViewModel(category)));
            }
            else
            {
                var discount = discountData.Where(x => x.Id == SelectedData.Id).FirstOrDefault();
                await Application.Current.MainPage.Navigation.PushAsync
                    (new CreateDiscountPage(new CreateDiscountPageViewModel(discount)));
            }
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
            typePage = Status.InItemPage.Item;
            InputDataToBinding();
        }

        public ICommand GotoCategory { get; set; }
        public void CategoryMethod()
        {
            typePage = Status.InItemPage.Categories;
            InputDataToBinding();
        }

        public ICommand GotoDiscount { get; set; }
        public void DiscountMethod()
        {
            typePage = Status.InItemPage.Discount;
            InputDataToBinding();
        }

        private void ChangeItem()
        {
            if( changeCategory == null || changeCategory == "All Items" )
            {
                InputDataToBinding();
            }
            else
            {
                TheData = itemDate.Where(y => y.CategoryName == changeCategory).Select(x => new ItemPageModel()
                {
                    Id = x.Id,
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
