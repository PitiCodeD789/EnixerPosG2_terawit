﻿using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Services;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ChargeViewModel : INotifyPropertyChanged
    {
        private IPopupNavigation _popup;
        private ReceiptViewModel _receipt;
        public ReceiptViewModel Receipt
        {
            get { return _receipt; }
            set { _receipt = value; }
        }
        ProductService _service = new ProductService();
        public ChargeViewModel(IPopupNavigation popup)
        {
            _popup = popup;
        }
        public ChargeViewModel(ReceiptViewModel receipt)
        {
            Receipt = receipt;
            TotalPrice = receipt.Total;
            TotalDiscount = receipt.TotalDiscount;
            CurrentTicket = new ObservableCollection<OrderItemModel>();
            foreach (var item in receipt.ItemList)
            {
                CurrentTicket.Add(item);
            }
            Cash = TotalPrice;
            CalculateExpectedCash();

            ExpectedCashCommand = new Command<decimal>(ChangeCash);
            PaymentCommand = new Command<string>(Payment);
            CashPaymentCommand = new Command(Payment);
            ClosePopupCommand = new Command(() => PopupNavigation.PopAllAsync());
            QrPaymentCompleteCommand = new Command(QrPaymentComplete);
        }

        public Command ExpectedCashCommand { get; set; }
        public Command PaymentCommand { get; set; }
        public Command CashPaymentCommand { get; set; }
        public Command ClosePopupCommand { get; set; }
        public Command QrPaymentCompleteCommand { get; set; }

        void CalculateExpectedCash()
        {
            ExpectedCash1 = GetExpectedCash(10);
            ExpectedCash2 = GetExpectedCash(20);
            if (ExpectedCash2 == ExpectedCash1)
                ExpectedCash2 = GetExpectedCash(50);
            ExpectedCash3 = GetExpectedCash(100);
            ExpectedCash4 = GetExpectedCash(500);
            if (ExpectedCash3 == ExpectedCash4)
                ExpectedCash4 = GetExpectedCash(1000);
            if (ExpectedCash1 == ExpectedCash2 && ExpectedCash2 == ExpectedCash3)
            {
                ExpectedCash1 = ExpectedCash2;
                ExpectedCash2 = ExpectedCash3;
                ExpectedCash3 = ExpectedCash4;
                ExpectedCash4 = GetExpectedCash(1000);
            }
        }
        decimal GetExpectedCash(int roundTo)
        {
            int multiplier = Convert.ToInt32(Math.Floor(TotalPrice / roundTo));
            if (!(multiplier * roundTo == TotalPrice))
                multiplier++;
            return (multiplier) * roundTo;
        }
        void ChangeCash(decimal amount)
        {
            Cash = amount;
        }
        async void Payment(string paymentType)
        {
            PaymentCommand payment = new PaymentCommand()
            {
                TotalDiscount = TotalDiscount,
                ItemList = CurrentTicket.ToList(),
                ShiftId = App.OpenShiftId,
                StoreEmail = App.Email,
                Total = TotalPrice
            };
            switch (paymentType)
            {
                case "Debit":
                    payment.PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Debit;
                    ReceiptViewModel result = _service.AddPayment(payment);
                    if (result != null)
                    {
                        PopupNavigation.PushAsync(new Error(new ErrorViewModel("Payment Completed", 3)));
                        Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                    }
                    return;

                case "Credit":
                    payment.PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Credit;
                    ReceiptViewModel resultC = _service.AddPayment(payment);

                    if (resultC != null)
                    {
                        PopupNavigation.PushAsync(new Error(new ErrorViewModel("Payment Completed", 3)));
                        Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                    }
                    break;

                case "Wallet":
                    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    GeneratePaymentModel model = new GeneratePaymentModel()
                    {
                        Amount = TotalPrice,
                        AccountNumber = App.AccountNumber,
                        FirstName = App.StoreName,
                        TransactionReference = unixTimestamp.ToString() + App.User,
                    };
                    QrValue = JsonConvert.SerializeObject(model);
                    await PopupNavigation.PushAsync(new QrPage(this));
                    await Task.Delay(2000);
                    PollCode(unixTimestamp.ToString() + App.User);
                    break;
                default:
                    break;
            }
        }
        async void Payment()
        {
            if (Cash < TotalPrice)
            {
                Application.Current.MainPage.DisplayAlert("Invalid Cash Amount", "Invalid cash amount, Please try again", "Ok");
                return;
            }
            Change = Cash - TotalPrice;
            PaymentCommand payment = new PaymentCommand()
            {
                TotalDiscount = TotalDiscount,
                ItemList = CurrentTicket.ToList(),
                PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Cash,
                ShiftId = App.OpenShiftId,
                StoreEmail = App.Email,
                Total = TotalPrice
            };
            payment.PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Cash;
            ReceiptViewModel resultW = _service.AddPayment(payment);
            if (resultW != null)
            {
                await PopupNavigation.Instance.PushAsync(new ShowChange(this));
                await Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
            }
        }
        async void PollCode(string referenceNumber)
        {
            IsPolling = true;
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                int delay = 3000;
                bool result = false;
                stopwatch.Start();
                while (!result)
                {
                    result = _service.CheckQrPayment(referenceNumber);
                    await Task.Delay(delay);
                    if (stopwatch.Elapsed.TotalMilliseconds >= delay*20)
                        break;
                }
                if(PopupNavigation.Instance.PopupStack.Any())
                    PopupNavigation.PopAllAsync();
                if (result)
                {
                    IsPolling = false;
                    QrPaymentComplete();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
                }
            }
            catch (global::System.Exception e)
            {
            }
            IsPolling = false;
        }
        public void QrPaymentComplete()
        {
            PaymentCommand payment = new PaymentCommand()
            {
                TotalDiscount = TotalDiscount,
                ItemList = CurrentTicket.ToList(),
                ShiftId = App.OpenShiftId,
                StoreEmail = App.Email,
                Total = TotalPrice
            };
            payment.PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Qr;
            ReceiptViewModel resultW = _service.AddPayment(payment);
            if (resultW != null)
            {
                PopupNavigation.PushAsync(new Error(new ErrorViewModel("Payment Completed", 3)));
                Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(this));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Payment Error", "Payment not completed please try again.", "Ok");
            }
            PopupNavigation.PopAllAsync();
        }

        #region propfull
        private bool _isPolling;

        public bool IsPolling
        {
            get { return _isPolling; }
            set { _isPolling = value; }
        }


        private string _qrValue;
        public string QrValue
        {
            get { return _qrValue; }
            set { _qrValue = value; }
        }

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
