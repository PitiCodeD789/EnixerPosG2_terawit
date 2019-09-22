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
        public CategoryPageViewModel()
        {
            ColorSelectCommand = new Command(ColorSelect);
            CreateCategoryCommand = new Command(CreateCategory);
            CancelCategoryCommand = new Command(CancelCategory);
        }

        private void CancelCategory(object obj)
        {
            
        }

        private async void CreateCategory()
        {
            bool result =  _productService.AddCategory(CategoryName, "tescolor").Result;
            if(result)
            {
                //  ErrorViewModel viewModel = new ErrorViewModel("ok",2);
                //  PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));
                Application.Current.MainPage.DisplayAlert("ok", "ok", "ok");
            }else
            {
                // ErrorViewModel viewModel = new ErrorViewModel("Error", 0);
                // PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));

                Application.Current.MainPage.DisplayAlert("ok", "ok", "error");
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


    }
}
