using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CreateDiscountPageViewModel : BaseViewModel
    {
        private IProductService _productService = new ProductService();
        public CreateDiscountPageViewModel()
        {
            CreateDiscountCommand = new Command(CreateDiscount);
            CancelCategoryCommand = new Command(Cancel);
        }

        private void Cancel(object obj)
        {
            throw new NotImplementedException();
        }

        private void CreateDiscount(object obj)
        {

            bool result = _productService.AddDiscount(DiscountName, true, Amount).Result;
            if (result)
            {
                //  ErrorViewModel viewModel = new ErrorViewModel("ok",2);
                //  PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));
                Application.Current.MainPage.DisplayAlert("ok", "ok", "ok");
            }
            else
            {
                // ErrorViewModel viewModel = new ErrorViewModel("Error", 0);
                // PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));

                Application.Current.MainPage.DisplayAlert("ok", "ok", "error");
            }
        }

      

        public ICommand CreateDiscountCommand { get; set; }
        public ICommand CancelCategoryCommand { get; set; }

        private string discountName;

        public string DiscountName
        {
            get { return discountName; }
            set { discountName = value; }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }


    }
}
