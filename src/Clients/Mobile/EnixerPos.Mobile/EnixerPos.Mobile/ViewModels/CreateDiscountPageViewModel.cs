using EnixerPos.Api.ViewModels.Product;
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
        private DiscountModel updateDiscount { get; set; }
        private IProductService _productService = new ProductService();
        public CreateDiscountPageViewModel()
        {
            CreateDiscountCommand = new Command(CreateDiscount);
            CancelCategoryCommand = new Command(Cancel);
            IsUpdate = false;
        }

        public CreateDiscountPageViewModel(DiscountModel discount)
        {
            SetShowDiscount(discount);
            updateDiscount = discount;
            IsUpdate = true;
        }
        public bool IsUpdate { get; set; }

        private void SetShowDiscount(DiscountModel discount)
        {
            if (discount.IsPercentage)
            {
                Type = 1;
            }
            else
            {
                Type = 0;
            }

            DiscountName = discount.DiscountName;
            Amount = discount.Amount.ToString("N2");
        }

        private bool IsPercentage(int selectIndex)
        {
            if (selectIndex == 0)
            {
                return false;
            }
            return true;
        }

        private void Cancel(object obj)
        {
            throw new NotImplementedException();
        }


        private void CreateDiscount()
        {
            if (IsUpdate)
            {
                var discount = new DiscountModel()
                {
                    Id = updateDiscount.Id,
                    DiscountName = DiscountName,
                    Amount = StringToDecimal(Amount),
                    IsPercentage = IsPercentage(Type),
                    StoreId = updateDiscount.StoreId,
                    CreateDateTime = updateDiscount.CreateDateTime,
                    UpdateDateTime = updateDiscount.UpdateDateTime
                };

                var result = _productService.UpdateDiscount(discount).Result;

                if (result != null || result.IsError)
                {
                    //TODO
                }
                else
                {
                    //TODO
                }
            }
            else
            {
                bool result = _productService.AddDiscount(DiscountName, IsPercentage(Type), Amount).Result;
                if (result)
                {
                    //  ErrorViewModel viewModel = new ErrorViewModel("ok",2);
                    //  PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));
                    Application.Current.MainPage.DisplayAlert("ok", "ok", "ok"); //TODO
                }
                else
                {
                    // ErrorViewModel viewModel = new ErrorViewModel("Error", 0);
                    // PopupNavigation.PushAsync(new Views.Popup.Error(viewModel));

                    Application.Current.MainPage.DisplayAlert("ok", "ok", "error"); //TODO
                }
            }
        }

        public decimal StringToDecimal(string value)
        {
            try
            {
                if (String.IsNullOrEmpty(value))
                {
                    return 0;
                }

                return decimal.Parse(value);
            }
            catch (Exception)
            {

                return 0;
            }
           
        }

      

        public ICommand CreateDiscountCommand { get; set; }
        public ICommand CancelCategoryCommand { get; set; }

        private string discountName;

        public string DiscountName
        {
            get { return discountName; }
            set { discountName = value;
                OnPropertyChanged();
            }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value;
                OnPropertyChanged();
            }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { type = value;
                OnPropertyChanged();
            }
        }



    }
}
