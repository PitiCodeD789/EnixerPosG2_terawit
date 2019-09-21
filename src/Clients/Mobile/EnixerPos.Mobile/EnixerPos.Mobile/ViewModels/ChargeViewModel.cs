using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ChargeViewModel : INotifyPropertyChanged
    {
        ReceiptViewModel _receipt;
        public ChargeViewModel(ReceiptViewModel receipt)
        {
            _receipt = receipt;
            TotalPrice = receipt.Total;
            TotalDiscount = receipt.TotalDiscount;
            CurrentTicket = new ObservableCollection<OrderItemModel>();
            foreach (var item in receipt.ItemList)
            {
                CurrentTicket.Add(item);
            }
            Cash = TotalPrice;
            ExpectedCash1 = GetExpectedCash(10);
            ExpectedCash2 = GetExpectedCash(20);
            if (ExpectedCash2 == ExpectedCash1)
                ExpectedCash2 = GetExpectedCash(50);
            ExpectedCash3 = GetExpectedCash(100);
            ExpectedCash4 = GetExpectedCash(500);
            if (ExpectedCash3 == ExpectedCash4)
                ExpectedCash4 = GetExpectedCash(1000);

            ExpectedCashCommand = new Command<decimal>(ChangeCash);
            PaymentCommand = new Command<string>(Payment);
            CashPaymentCommand = new Command(Payment);
            ClosePopupCommand = new Command(() => PopupNavigation.PopAllAsync());
        }

        public Command ExpectedCashCommand { get; set; }
        public Command PaymentCommand { get; set; }
        public Command CashPaymentCommand { get; set; }
        public Command ClosePopupCommand { get; set; }

        decimal GetExpectedCash(int roundTo)
        {
            int multiplier = Convert.ToInt32(TotalPrice / roundTo);
            if (!(multiplier * roundTo == TotalPrice))
                multiplier ++;
            return (multiplier) * roundTo;
        }
        void ChangeCash(decimal amount)
        {
            Cash = amount;
        }
        void Payment(string paymentType)
        {
            switch (paymentType)
            {
                case "Debit":
                    //TODO: Add Payment
                    if (true)
                        Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
                    else
                        Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                    break;
                case "Credit":
                    //TODO: Add Payment
                    if (true)
                        Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
                    else
                        Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                    break;
                case "Wallet":
                    //TODO: Add Payment
                    if (true)
                        Application.Current.MainPage.Navigation.PushAsync(new QrPage(this));
                    else
                        Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                    break;
                default:
                    break;
            }
        }
        void Payment()
        {
            Change = Cash - TotalPrice;
            PopupNavigation.PushAsync(new ShowChange(this));
            Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
        }

        #region propfull
        private decimal _change;
        public decimal Change
        {
            get { return _change; }
            set { _change = value; OnPropertyChanged(); }
        }


        private ObservableCollection<OrderItemModel> _currentTicket;
        public ObservableCollection<OrderItemModel> CurrentTicket
        {
            get { return _currentTicket; }
            set
            {
                _currentTicket = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged();
            }
        }

        private decimal _discount;
        public decimal TotalDiscount
        {
            get { return _discount; }
            set { _discount = value; OnPropertyChanged(); }
        }

        public decimal FullPrice
        {
            get { return TotalPrice + TotalDiscount; }
        }

        public bool IsDiscount
        {
            get { return TotalDiscount > 0; }
        }

        private decimal _cash;
        public decimal Cash
        {
            get { return _cash; }
            set { _cash = value; OnPropertyChanged(); }
        }

        public decimal ExpectedCash1 { get; set; }
        public decimal ExpectedCash2 { get; set; }
        public decimal ExpectedCash3 { get; set; }
        public decimal ExpectedCash4 { get; set; }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
