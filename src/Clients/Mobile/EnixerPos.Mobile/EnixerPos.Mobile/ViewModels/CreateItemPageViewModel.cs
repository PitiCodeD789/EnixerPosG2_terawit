using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CreateItemPageViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private ItemModel UpdateItem { get; set;}
        public CreateItemPageViewModel()
        {
            _productService = new ProductService();
            ColorSelectCommand = new Command(ColorSelect);
            CreateItemCommand = new Command(CreateItem);
            GetAllCateAsync();
            CategoriesName = Categories.Select(x => x.Name).ToList();
            IsUpdate = false;
            TitleAndButtonText = "Create Item";
        }
        public CreateItemPageViewModel(ItemModel item)
        {
            _productService = new ProductService();
            ColorSelectCommand = new Command(ColorSelect);
            CreateItemCommand = new Command(CreateItem);
            GetAllCateAsync();
            CategoriesName = Categories.Select(x => x.Name).ToList();
            SetShowItem(item);
            UpdateItem = item;
            IsUpdate = true;
            SelectedCategory = item.CategoryName;
            TitleAndButtonText = "Update item";
        }

        private void SetShowItem(ItemModel item)
        {
            ItemName = item.Name;
            ItemPrice = item.Price.ToString("N2");
            ItemCost = item.Cost.ToString("N2");
            setColor(GetColorNum(item.Color));
            Option1 = item.Option1;
            Price1 = item.Option1Price.ToString("N2");
            Option2 = item.Option2;
            Price2 = item.Option2Price.ToString("N2");
            Option3 = item.Option3;
            Price3 = item.Option3Price.ToString("N2");
            Option4 = item.Option4;
            Price4 = item.Option4Price.ToString("N2");
        }

        private async Task<List<CategoryModel>> GetAllCateAsync()
        {
            try
            {
                var result = await _productService.GetAllCategories();
                if (result!=null || !result.IsError || result.Categories != null)
                {
                    Categories = result.Categories;
                    return result.Categories;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public bool IsUpdate { get; set; }

        public ICommand CreateItemCommand { get; set; }

        private List<string> categoriesName;

        public List<string> CategoriesName
        {
            get { return categoriesName; }
            set { categoriesName = value; }
        }
        private string selectedCategory;

        public string SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value;
                OnPropertyChanged();
                SelectCate = Categories.Where(x => x.Name == SelectedCategory).FirstOrDefault();
            }
        }


        public async override void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private CategoryModel selectCate;

        public CategoryModel SelectCate
        {
            get { return selectCate; }
            set
            {
                selectCate = value;
                OnPropertyChanged();
            }
        }
        private List<CategoryModel> categories;

        public List<CategoryModel> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }


        private async void CreateItem()
        {
            try
            {
                ResultViewModel result = new ResultViewModel { IsError = true };
                if (IsUpdate)
                {
                    ItemModel item = new ItemModel()
                    {
                        Id = UpdateItem.Id,
                        Name = ItemName,
                        CategoryName = SelectedCategory,
                        Price = ConvertPrice(ItemPrice),
                        Cost = ConvertPrice(ItemCost),
                        Color = GetColor(),
                        Option1 = Option1,
                        Option1Price = ConvertPrice(Price1),
                        Option2 = Option2,
                        Option2Price = ConvertPrice(Price2),
                        Option3 = Option3,
                        Option3Price = ConvertPrice(Price3),
                        Option4 = Option4,
                        Option4Price = ConvertPrice(Price4),
                        CreateDateTime = UpdateItem.CreateDateTime,
                        UpdateDateTime = UpdateItem.UpdateDateTime,
                    };

                    result = await _productService.UpdateItem(item);

                }
                else
                {
                    ItemModel item = new ItemModel()
                    {
                        Name = ItemName,
                        CategoryName = SelectedCategory,
                        Price = ConvertPrice(ItemPrice),
                        Cost = ConvertPrice(ItemCost),
                        Color = GetColor(),
                        Option1 = Option1,
                        Option1Price = ConvertPrice(Price1),
                        Option2 = Option2,
                        Option2Price = ConvertPrice(Price2),
                        Option3 = Option3,
                        Option3Price = ConvertPrice(Price3),
                        Option4 = Option4,
                        Option4Price = ConvertPrice(Price4)

                    };

                    result = await _productService.CreateItem(item);

                }


                if ((result != null || !result.IsError))
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("บันทึกรายการสำเร็จ", 3);
                    PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    BackPageMethod();
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel("ผิดพลาด", 1);
                    PopupNavigation.Instance.PushAsync(new Error(error));
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private string GetColor()
        {
            switch (SelectColor)
            {
                case 1: return "#ffffff";
                case 2: return "#ffd5d5";
                case 3: return "#f8ffd3";
                case 4: return "#c6dbfc";
                case 5: return "#ffccf9";
                case 6: return "#e0e0e0";
                default: return null;
            }
        }

        private int GetColorNum(string ColorHex)
        {
            string hex = ColorHex.ToLower();
            switch (hex)
            {
                case "#ffffff": return 1;
                case "#ffd5d5": return 2;
                case "#f8ffd3": return 3;
                case "#c6dbfc": return 4;
                case "#ffccf9": return 5;
                case "#e0e0e0": return 6;
                default: return 0;
            }
        }

        private decimal ConvertPrice(string price)
        {
            try
            {
                if (price != null)
                {
                    return Decimal.Parse(price);
                }
                return 0;
            }
            catch (Exception)
            {

                return 0;

            }

        }

        private string itemName;

        public string ItemName
        {
            get { return itemName; }
            set {
                itemName = value;
                OnPropertyChanged();
            }
        }

        private string itemPrice;

        public string ItemPrice
        {
            get { return itemPrice; }
            set {
                itemPrice = value;
                OnPropertyChanged();
            }
        }

        private string itemCost;

        public string ItemCost
        {
            get { return itemCost; }
            set {
                itemCost = value;
                OnPropertyChanged();
            }
        }

        private string option1;
            
        public string Option1
        {
            get { return option1; }
            set {
                option1 = value;
                OnPropertyChanged();
            }
        }

        private string price1;

        public string Price1
        {
            get { return price1; }
            set {
                price1 = value;
                OnPropertyChanged();
            }
        }


        private string option2;

        public string Option2
        {
            get { return option2; }
            set {
                option2 = value;
                OnPropertyChanged();
            }
        }

        private string price2;

        public string Price2
        {
            get { return price2; }
            set {
                if (!String.IsNullOrEmpty(Option2))
                {
                    price2 = value;
                    OnPropertyChanged();
                }
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// 
        private string option3;
        public string Option3
        {
            get { return option3; }
            set {
                option3 = value;
                OnPropertyChanged();
            }
        }

        private string price3;

        public string Price3
        {
            get { return price3; }
            set {
                if (!String.IsNullOrEmpty(Option3))
                {
                price3 = value;
                OnPropertyChanged();

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// 
        private string option4;
        public string Option4
        {
            get { return option4; }
            set {

                option4 = value;
                OnPropertyChanged();
            }
        }

        private string price4;

        public string Price4
        {
            get { return price4; }
            set {
                if (!String.IsNullOrEmpty(Option4))
                {
                price4 = value;
                OnPropertyChanged();
                }
            }
        }

        private int selectColor;

        public int SelectColor
        {
            get { return selectColor; }
            set
            {
                selectColor = value;
                OnPropertyChanged();
            }
        }



        private void ColorSelect(object obj)
        {
            int id = int.Parse(obj.ToString());

            setColor(id);
        }

        public ICommand ColorSelectCommand { get; set; }

        private void setColor(int colorIndex)
        {
            SelectColor = colorIndex;
            if (colorIndex == 1)
            {
                Color1 = true;
            }
            else
            {
                Color1 = false;
            }

            if (colorIndex == 2)
            {
                Color2 = true;
            }
            else
            {
                Color2 = false;
            }
            if (colorIndex == 3)
            {
                Color3 = true;
            }
            else
            {
                Color3 = false;
            }

            if (colorIndex == 4)
            {
                Color4 = true;
            }
            else
            {
                Color4 = false;
            }

            if (colorIndex == 5)
            {
                Color5 = true;
            }
            else
            {
                Color5 = false;
            }

            if (colorIndex == 6)
            {
                Color6 = true;
            }
            else
            {
                Color6 = false;
            }

        }

        private bool color1 = true;

        public bool Color1
        {
            get { return color1; }
            set { color1 = value; OnPropertyChanged(); }
        }

        private bool color2;

        public bool Color2
        {
            get { return color2; }
            set { color2 = value; OnPropertyChanged(); }
        }

        private bool color3;

        public bool Color3
        {
            get { return color3; }
            set { color3 = value; OnPropertyChanged(); }
        }

        private bool color4;

        public bool Color4
        {
            get { return color4; }
            set { color4 = value; OnPropertyChanged(); }
        }
        private bool color5;

        public bool Color5
        {
            get { return color5; }
            set { color5 = value; OnPropertyChanged(); }
        }
        private bool color6;

        public bool Color6
        {
            get { return color6; }
            set { color6 = value; OnPropertyChanged(); }
        }

        private string titleAndButtonText;

        public string TitleAndButtonText
        {
            get { return titleAndButtonText; }
            set { titleAndButtonText = value;
                OnPropertyChanged();
            }
        }

       


    }
}
