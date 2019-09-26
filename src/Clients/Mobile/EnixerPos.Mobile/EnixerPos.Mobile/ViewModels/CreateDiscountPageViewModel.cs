using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
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
            TitleAndButtonText = "Create Discount";
        }

        public CreateDiscountPageViewModel(DiscountModel discount)
        {
            CreateDiscountCommand = new Command(CreateDiscount);
            CancelCategoryCommand = new Command(Cancel);
            SetShowDiscount(discount);
            updateDiscount = discount;
            IsUpdate = true;
            TitleAndButtonText = "Update Discount";
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
                    IsPercentage = IsPercentage(Type),
                    Amount = StringToDecimal(Amount, IsPercentage(Type)),
                    StoreId = updateDiscount.StoreId,
                    CreateDateTime = updateDiscount.CreateDateTime,
                    UpdateDateTime = updateDiscount.UpdateDateTime
                };

                var result = _productService.UpdateDiscount(discount).Result;

                if (result != null && !result.IsError)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("บันทึกรายการสำเร็จ", 3);
                    PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    BackPageMethod();
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel("ผิดพลาด ไม่สามารถทำรายการได้", 1);
                    PopupNavigation.Instance.PushAsync(new Error(error));
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(DiscountName) && !String.IsNullOrEmpty(Amount))
                {
                    bool result = _productService.AddDiscount(DiscountName, IsPercentage(Type), Amount).Result;
                    if (result)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("บันทึกรายการสำเร็จ", 3);
                        PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                        BackPageMethod();
                    }
                    else
                    {
                        ErrorViewModel error = new ErrorViewModel("ผิดพลาด ไม่สามารถทำรายการได้", 1);
                        PopupNavigation.Instance.PushAsync(new Error(error));
                    }
                }
                else
                {
                    ErrorViewModel error = new ErrorViewModel("ผิดพลาด ไม่สามารถทำรายการได้", 1);
                    PopupNavigation.Instance.PushAsync(new Error(error));
                }
                
            }
        }

        public decimal StringToDecimal(string value,bool IsPercentage)
        {
            try
            {
                if (String.IsNullOrEmpty(value))
                {
                    return 0;
                }
                else
                {
                    var discount = decimal.Parse(value);
                    if (discount > 100 && IsPercentage == true)
                    {
                        return 100;
                    }
                    else if (discount > 0)
                    {
                        return discount;
                    }
                    else
                    {
                        return 0;
                    }
                }
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
