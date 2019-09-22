using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Models;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ReceiptPageViewModel:BaseViewModel
    {
        private readonly IReceiptService _receiptService;

        public ReceiptPageViewModel()
        {
            _receiptService = new ReceiptService();
            AllReciept = _receiptService.GetReceipt();
        }

        private decimal totalPrice;

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set {
                totalPrice = value;
                OnPropertyChanged();
            }
        }
        private string discountAll;

        public string DiscountAll
        {
            get { return discountAll; }
            set { discountAll = value; OnPropertyChanged(); }
        }

        private string paymentType;

        public string PaymentType
        {
            get { return paymentType; }
            set {
                paymentType = value;
                OnPropertyChanged();
            }
        }


        private string discountValue;

        public string DiscountValue
        {
            get { return discountValue; }
            set { discountValue = value; OnPropertyChanged(); }
        }

        private string date;

        public string Date
        {
            get { return DateTime.Now.ToString("dddd, dd MMMM yyyy"); }
        }



        private List<ReceiptViewModel> allReciept;

        public List<ReceiptViewModel> AllReciept
        {
            get { return allReciept; }
            set
            {
                allReciept = value;
                OnPropertyChanged();
            }
        }

        private ReceiptViewModel selectedReceipt;

        public ReceiptViewModel SelectedReceipt
        {
            get { return selectedReceipt; }
            set {
                selectedReceipt = value;
                OnPropertyChanged();
                if (SelectedReceipt != null)
                {
                    TotalPrice = GetTotalPrice();
                    DiscountAll = GetDiscountString(selectedReceipt.Discount, selectedReceipt.IsDiscountPercentage);
                    DiscountValue = GetDiscountValue();
                    GetShowOrder();
                }
            }
        }

        private string GetDiscountValue()
        {
            decimal discount = SelectedReceipt.Discount;
            if (SelectedReceipt.IsDiscountPercentage)
            {
                discount = SelectedReceipt.Total * (SelectedReceipt.Discount / 100);
            }
            return "-"+discount;
        }

        private decimal GetTotalPrice()
        {
            var total = SelectedReceipt.Total;
            decimal discount = SelectedReceipt.Discount;
            if (SelectedReceipt.IsDiscountPercentage)
            {
                discount = total * (SelectedReceipt.Discount / 100);
            }
            return total - discount;
        }

        private List<ShowOrderModel> selectItem;

        public List<ShowOrderModel> SelectItem
        {
            get { return selectItem; }
            set {
                selectItem = value;
                OnPropertyChanged();
            }
        }


        private void GetShowOrder()
        {
            var order = SelectedReceipt.ItemList;

            SelectItem = new List<ShowOrderModel>() { };
            foreach (var item in order)
            {
                selectItem.Add(new ShowOrderModel()
                {
                    ItemName = item.ItemName,
                    QuantityAndPrice = $"{item.Quantity} x {item.ItemPrice.ToString("N2")}",
                    ItemDiscount = GetDiscountString(item.ItemDiscount,item.IsDiscountPercentage),
                    IsDiscount = GetIsDiscountOrOption(item.ItemDiscount),
                    ItemOption = GetOptionString(item.OptionName,item.OptionPrice),
                    IsOption = GetIsDiscountOrOption(item.OptionPrice),
                    ItemTotalPrice = GetTotalItemPrice(item.ItemPrice,item.Quantity,item.OptionPrice),
                    DiscountPrice = GetDiscountPrice(item.ItemPrice, item.Quantity, item.OptionPrice,item.ItemDiscount,item.IsDiscountPercentage),
                    Strike = GetStrikeString(GetIsDiscountOrOption(item.ItemDiscount))
                });
            }

        }

        private TextDecorations GetStrikeString(bool isDiscount)
        {
            if (isDiscount)
            {
                return TextDecorations.Strikethrough ;
            }
            return TextDecorations.None;
        }

        private decimal GetDiscountPrice(decimal itemPrice, int quantity, decimal optionPrice, decimal itemDiscount, bool isDiscountPercentage)
        {
            var total = (itemPrice + optionPrice) * quantity;
            decimal discount = itemDiscount;
            if (isDiscountPercentage)
            {
                discount = total*(itemDiscount / 100);
            }
            return total - discount;
        }

        private decimal GetTotalItemPrice(decimal itemPrice, int quantity, decimal optionPrice)
        {
            return (itemPrice + optionPrice) * quantity;
        }

        private string GetOptionString(string optionName, decimal optionPrice)
        {
            if (optionName != null)
            {
                return $"+ {optionName} ({optionPrice.ToString("N2")})";
            }
            else
            {
                return "";
            }
        }

        private bool GetIsDiscountOrOption(decimal value)
        {
            if (value > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetDiscountString(decimal itemDiscount,bool isPercent)
        {
            if (itemDiscount>0 && isPercent)
            {
                return $"Discount {itemDiscount.ToString("N2")}%"; 
            }
            else if(itemDiscount > 0)
            {
                return $"Discount {itemDiscount.ToString("N2")}";
            }
            else
            {
                return "";
            }
        }

        private int selectReceipt;

        public int SelectReceipt
        {
            get { return selectReceipt; }
            set
            {
                selectReceipt = value;
                OnPropertyChanged();
                SelectedReceipt = allReciept[selectReceipt];
            }
        }




    }
}
