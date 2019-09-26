using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CategoryPageViewModel : BaseViewModel
    {
        private IProductService _productService = new ProductService();
        private CategoryModel UpdateCategory { get; set; }
        public CategoryPageViewModel()
        {
            ColorSelectCommand = new Command(ColorSelect);
            CreateCategoryCommand = new Command(CreateCategory);
            CancelCategoryCommand = new Command(CancelCategory);
            IsUpdate = false;
            TitleAndButtonText = "Create Category";
        }
        public CategoryPageViewModel(CategoryModel category)
        {
            ColorSelectCommand = new Command(ColorSelect);
            CreateCategoryCommand = new Command(CreateCategory);
            CancelCategoryCommand = new Command(CancelCategory);
            SetShowCategory(category);
            UpdateCategory = category;
            IsUpdate = true;
            TitleAndButtonText = "Update Category";
        }

        private void SetShowCategory(CategoryModel category)
        {
            CategoryName = category.Name;
            setColor(GetColorNum(category.Color));
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

        public bool IsUpdate { get; set; }

        private void CancelCategory(object obj)
        {
            
        }

        private async void CreateCategory()
        {
            if (IsUpdate)
            {
                var category = new CategoryModel()
                {
                    Id = UpdateCategory.Id,
                    Name = CategoryName,
                    Color = GetColor(),
                    CountItem = UpdateCategory.CountItem,
                    CreateDateTime = UpdateCategory.CreateDateTime,
                    UpdateDateTime = UpdateCategory.UpdateDateTime
                };

                var result = _productService.UpdateCategory(category).Result;

                if (result != null && !result.IsError)
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
            else
            {
                bool result = _productService.AddCategory(CategoryName, GetColor()).Result;
                if (result)
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
        }

        private void ColorSelect(object obj)
        {
            int id = int.Parse( obj.ToString());                        
            
            setColor(id);
        }

        public ICommand ColorSelectCommand { get; set; }
        public ICommand CreateCategoryCommand { get; set; }
        public ICommand CancelCategoryCommand { get; set; }

        private void setColor(int colorIndex)
        {
            SelectColor = colorIndex;
            if (colorIndex == 1)
            {
                Color1 = true;
            }else
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




        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
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
            set
            {
                titleAndButtonText = value;
                OnPropertyChanged();
            }
        }
    }
}
