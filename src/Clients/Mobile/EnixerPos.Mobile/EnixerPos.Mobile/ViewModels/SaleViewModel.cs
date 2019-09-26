using AiForms.Renderers;
using AutoMapper;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Components;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views;
using EnixerPos.Mobile.Views.Popup;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xam.Plugin.TabView;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class SaleViewModel : INotifyPropertyChanged
    {
        IProductService _service = new ProductService();
        public SaleViewModel()
        {
            ShowOpenButton = true;
            CurrentTicket = new ObservableCollection<OrderItemModel>();
            CurrentTotalDiscount = new DiscountModel();
            Quantity = 1;
            AllMenu = new List<MenuModel>();
            SetItemMenu();
            foreach (var category in AllMenu)
            {
                var grid = new Grid()
                {
                    ColumnSpacing = 10,
                    RowSpacing = 10,
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                        new ColumnDefinition(){ Width = GridLength.Star},
                    },
                    RowDefinitions = new RowDefinitionCollection()
                    {
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                        new RowDefinition(){ Height = GridLength.Star},
                    }
                };

                int row = 0;
                int col = 0;
                if (category != null || category.Items.Count > 0)
                {
                    foreach (var item in category.Items)
                    {
                        if (col % 4 == 0 && col != 0)
                        {
                            row++;
                            col = 0;
                        }

                        var button = new Button();
                        button.Text = item.Name;
                        button.HeightRequest = 150;
                        button.WidthRequest = 150;
                        button.HorizontalOptions = LayoutOptions.Center;
                        button.VerticalOptions = LayoutOptions.Center;
                        button.Command = new Command<ItemModel>((itemModel) => OpenOption(itemModel));
                        button.CommandParameter = item;
                        button.BackgroundColor = Color.FromHex(item.Color);
                        grid.Children.Add(button, col, row);
                        col++;
                    }
                }

                var scrollView = new ScrollView()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Content = grid
                };
                ViewChildren.Add(scrollView);
                TabChildren.Add(new TabItem(category.CategoryName, scrollView));
            };
            QuantityChangeCommand = new Command<string>((change) => QuantityChange(change));
            SaveItemCommand = new Command(SaveItemToTicket);
            SaveTicketCommand = new Command(SaveTicket);
            OpenTicketListCommand = new Command(OpenTicketList);
            OpenTicketCommand = new Command<int>((ticketNumber) => OpenTicket(ticketNumber));
            ChargeCommand = new Command(Charge);
            DeleteItemCommand = new Command<OrderItemModel>(DeleteItem);
            CloudsyncClickCommand = new Command(CloudsyncClick);
            GetDiscount();
        }

        public Command QuantityChangeCommand { get; set; }
        public Command SaveItemCommand { get; set; }
        public Command SaveTicketCommand { get; set; }
        public Command OpenTicketListCommand { get; set; }
        public Command OpenTicketCommand { get; set; }
        public Command ChargeCommand { get; set; }
        public Command DeleteItemCommand { get; set; }
        public Command AddDiscountCommand { get; set; }
        public Command CloudsyncClickCommand { get; set; }

        public void QuantityChange(string change)
        {
            if (change == "+")
            {
                Quantity++;
            }
            else if (change == "-")
            {
                if (Quantity > 0)
                {
                    Quantity--;
                }
            }
        }
        public void SaveItemToTicket()
        {
            ShowOpenButton = false;
            try
            {
                decimal optionPrice = OptionList[CurrentSelectedOptionIndex].Price;
                bool isDiscount = true;
                DiscountModel currentDiscount = new DiscountModel();
                if (Discount1)
                    currentDiscount = new DiscountModel { DiscountName = Discounts[0].DiscountName, Amount = Discounts[0].Amount, IsPercentage = Discounts[0].IsPercentage };
                else if (Discount2)
                    currentDiscount = new DiscountModel { DiscountName = Discounts[1].DiscountName, Amount = Discounts[1].Amount, IsPercentage = Discounts[1].IsPercentage };
                else if (Discount3)
                    currentDiscount = new DiscountModel { DiscountName = Discounts[2].DiscountName, Amount = Discounts[2].Amount, IsPercentage = Discounts[2].IsPercentage };
                else if (Discount4)
                    currentDiscount = new DiscountModel { DiscountName = Discounts[3].DiscountName, Amount = Discounts[3].Amount, IsPercentage = Discounts[3].IsPercentage };
                else
                    isDiscount = false;
                decimal discountedAmount = (currentDiscount.IsPercentage) ? 
                    (currentDiscount.Amount / 100 * (CurrentItem.Price + optionPrice) * Quantity) 
                    : ((currentDiscount.Amount < (CurrentItem.Price + optionPrice) * Quantity) ? currentDiscount.Amount : (CurrentItem.Price + optionPrice) * Quantity);

                CurrentTicket.Add(new OrderItemModel
                {
                    ItemName = CurrentItem.Name,
                    IsDiscounted = isDiscount,
                    ItemPrice = (CurrentItem.Price + optionPrice) * Quantity,
                    Quantity = Quantity,
                    OptionName = OptionList[CurrentSelectedOptionIndex].OptionName,
                    OptionPrice = OptionList[CurrentSelectedOptionIndex].Price,
                    DiscountName = CurrentTotalDiscount.DiscountName,
                    DiscountedPrice = (CurrentItem.Price + optionPrice ) * Quantity - discountedAmount,
                    IsDiscountPercentage = currentDiscount.IsPercentage,
                    ItemDiscount = currentDiscount.Amount
                });

                CalculateTotalAmount();

                Discount1 = false;
                Discount2 = false;
                Discount3 = false;
                Discount4 = false;
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", "Cannot add item to ticket", "Ok");
            }

            CurrentTicket = CurrentTicket;
            if (PopupNavigation.Instance.PopupStack.Any())
                PopupNavigation.PopAllAsync();
            CurrentItem = new ItemModel();
            CurrentOption = new List<string>();
            CurrentSelectedOptionIndex = 0;
            Quantity = 1;
        }
        void CalculateTotalAmount()
        {
            SetTotalDiscount();
            decimal currentAmountWithoutDiscount = 0;
            foreach (var item in CurrentTicket)
            {
                if (item.ItemDiscount == 0)
                    currentAmountWithoutDiscount += item.ItemPrice;
            }
            if (CurrentTotalDiscount.IsPercentage)
            {
                CurrentDiscountAmount = CurrentTotalDiscount.Amount / 100 * currentAmountWithoutDiscount;
            }
            else
            {
                CurrentDiscountAmount = CurrentTotalDiscount.Amount;
            }
            decimal _totalPrice = 0;
            foreach (var item in CurrentTicket)
            {
                _totalPrice += item.DiscountedPrice;
            }
            CurrentDiscountAmount = (currentAmountWithoutDiscount - CurrentDiscountAmount > 0) ? CurrentDiscountAmount : currentAmountWithoutDiscount;
            TotalPrice = _totalPrice - CurrentDiscountAmount;
        }
        void SetTotalDiscount()
        {
            DiscountModel currentDiscount = new DiscountModel();
            if (TotalDiscount1)
                currentDiscount = new DiscountModel { DiscountName = Discounts[0].DiscountName, Amount = Discounts[0].Amount, IsPercentage = Discounts[0].IsPercentage };
            else if (TotalDiscount1)
                currentDiscount = new DiscountModel { DiscountName = Discounts[1].DiscountName, Amount = Discounts[1].Amount, IsPercentage = Discounts[1].IsPercentage };
            else if (TotalDiscount1)
                currentDiscount = new DiscountModel { DiscountName = Discounts[2].DiscountName, Amount = Discounts[2].Amount, IsPercentage = Discounts[2].IsPercentage };
            else if (TotalDiscount1)
                currentDiscount = new DiscountModel { DiscountName = Discounts[3].DiscountName, Amount = Discounts[3].Amount, IsPercentage = Discounts[3].IsPercentage };
            CurrentTotalDiscount = currentDiscount;
        }
        void OpenOption(ItemModel item)
        {
            CurrentItem = item;
            CurrentOption = new List<string>();
            OptionList = new List<ItemOptionModel>();

            if (!string.IsNullOrEmpty(item.Option1))
                OptionList.Add(new ItemOptionModel() { OptionName = item.Option1, Price = item.Option1Price });

            if (!string.IsNullOrEmpty(item.Option2))
                OptionList.Add(new ItemOptionModel() { OptionName = item.Option2, Price = item.Option2Price });

            if (!string.IsNullOrEmpty(item.Option3))
                OptionList.Add(new ItemOptionModel() { OptionName = item.Option3, Price = item.Option3Price });

            if (!string.IsNullOrEmpty(item.Option4))
                OptionList.Add(new ItemOptionModel() { OptionName = item.Option4, Price = item.Option4Price });

            foreach (var option in OptionList)
            {
                CurrentOption.Add(option.OptionName + "  ( +" + option.Price.ToString("#,0.00") + " )");
            }

            CurrentItemPrice = item.Price;

            if (item.Option2 == null && item.Option3 == null && item.Option4 == null)
            {
                if (!string.IsNullOrEmpty(item.Option1))
                    OptionList.Add(new ItemOptionModel() { OptionName = item.Option1, Price = item.Option1Price });
                CurrentSelectedOptionIndex = 0;
                CurrentItemPrice = item.Price;
                SaveItemToTicket();
            }
            else
            {
                PopupNavigation.PushAsync(new ItemPopup(this));
            }
        }
        void SetItemMenu()
        {
            if (App.ListCategoryModels == null)
            {

                var getCate = _service.GetAllCategories();

                if (!getCate.Result.IsError)
                {
                    CategoriesList = getCate.Result.Categories;
                    App.ListCategoryModels = CategoriesList;
                }
            } else
            {
                CategoriesList = App.ListCategoryModels;
            }
            if (CategoriesList == null)
            {
                CategoriesList = new List<CategoryModel>();

                foreach (var category in CategoriesList)
                {
                    AllMenu.Add(new MenuModel()
                    {
                        CategoryName = category.Name,
                        Items = new ObservableCollection<ItemModel>()
                    });
                }
            }
            ItemsViewModel items;
            if (App.ItemsManuViewModel == null || App.ItemsManuViewModel.Items.Count == 0)
            {
                items = _service.GetItems();
                App.ItemsManuViewModel = items;
            }
            else
            {
                items = App.ItemsManuViewModel;
            }
            if (items.Items == null || items.Items.Count == 0)
            {
                //Application.Current.MainPage.DisplayAlert("", "ไม่พบรายการอาหารในระบบ", "OK");
                //PopupNavigation.PushAsync(new Error(new ErrorViewModel("ไม่พบรายการอาหารในระบบ")));
                return;
            }

            foreach (var item in items.Items)
            {
                var isCatExist = AllMenu.Where(a => a.CategoryName == item.CategoryName).FirstOrDefault();
                if (isCatExist == null)
                {
                    AllMenu.Add(new MenuModel()
                    {
                        CategoryName = item.CategoryName,
                        Items = new ObservableCollection<ItemModel>()
                    }); ;
                }
                if (item.Color == null)
                {
                    item.Color = "DDDDDD";
                }
                AllMenu.Where(a => a.CategoryName == item.CategoryName).FirstOrDefault().Items.Add(item);
            }

        }
        void SaveTicket()
        {
            var maxNumber = 0;
            try
            {
                maxNumber = App.TicketList.Max(t => t.TicketNumber);
            }
            catch (Exception e)
            {
            }
            ReceiptViewModel receipt = new ReceiptViewModel()
            {
                ItemList = CurrentTicket.ToList(),
                Total = TotalPrice,
                TicketNumber = maxNumber + 1,
                CreateDateTime = DateTime.Now,
            };
            App.TicketList.Add(receipt);
            ClearTicket();
        }
        void OpenTicketList()
        {
            ClearTicket();
            PopupNavigation.PushAsync(new OpenTicketsPopup(this));
            TicketList = App.TicketList;
        }
        void ClearTicket()
        {
            CurrentTicket = new ObservableCollection<OrderItemModel>();
            Quantity = 1;
            QuantityChangeCommand = new Command<string>((change) => QuantityChange(change));
            SaveItemCommand = new Command(SaveItemToTicket);
            CurrentTicket = new ObservableCollection<OrderItemModel>();
            CurrentTotalDiscount = new DiscountModel();
            CurrentDiscountAmount = 0;
            TotalPrice = 0;
            ShowOpenButton = true;
        }
        void OpenTicket(int ticketNumber)
        {
            var ticket = App.TicketList.Where(t => t.TicketNumber == ticketNumber).FirstOrDefault();
            if (ticket.ItemList == null || ticket.ItemList.Count == 0)
                return;
            foreach (var item in ticket.ItemList)
            {
                CurrentTicket.Add(item);
                TotalPrice += item.DiscountedPrice;
            }
            App.TicketList.Remove(ticket);
            PopupNavigation.PopAllAsync();
        }
        void Charge()
        {
            if (CurrentTicket == null || CurrentTicket.Count == 0)
            {
                PopupNavigation.PushAsync(new Error(new ErrorViewModel("Please add item to ticket before proceeding")));
                return;
            }
            decimal totalDiscount = 0;
            foreach (var item in CurrentTicket)
            {
                totalDiscount += item.ItemDiscount;
            }
            ReceiptViewModel receipt = new ReceiptViewModel()
            {
                ItemList = CurrentTicket.ToList(),
                Total = TotalPrice,
                CreateDateTime = DateTime.Now,
                TotalDiscount = totalDiscount+ CurrentDiscountAmount,
                Discount = CurrentDiscountAmount,
            };
            Application.Current.MainPage.Navigation.PushAsync(new ChargePage(receipt));
        }
        void GetDiscount()
        {
            var discountVM = _service.GetDiscounts();
            Discounts = discountVM.Discounts;
            if (Discounts.Count > 0)
            {
                DiscountName1 = Discounts[0].DiscountName;
                HasDiscount1 = true;
                Discount1 = false;
            }

            if (Discounts.Count > 1)
            {
                DiscountName2 = Discounts[1].DiscountName;
                HasDiscount2 = true;
                Discount2 = false;
            }

            if (Discounts.Count > 2)
            {
                DiscountName3 = Discounts[2].DiscountName;
                HasDiscount3 = true;
                Discount3 = false;
            }

            if (Discounts.Count > 3)
            {
                DiscountName4 = Discounts[3].DiscountName;
                HasDiscount4 = true;
                Discount4 = false;
            }

        }
        void DeleteItem(OrderItemModel model)
        {
            CurrentTicket.Remove(model);
            CalculateTotalAmount();
        }
        void CloudsyncClick()
        {
            IsLoadding = true;
            var getCate = _service.GetAllCategories();

            if (!getCate.Result.IsError)
            {
                CategoriesList = getCate.Result.Categories;
                App.ListCategoryModels = CategoriesList;
            }

           
              var  items = _service.GetItems();
           
           
            if (items.Items == null || items.Items.Count == 0)
            {
                //Application.Current.MainPage.DisplayAlert("", "ไม่พบรายการอาหารในระบบ", "OK");
                //PopupNavigation.PushAsync(new Error(new ErrorViewModel("ไม่พบรายการอาหารในระบบ")));
                return;
            }
           App.ItemsManuViewModel = items;
            Application.Current.MainPage = new NavigationPage(new Views.SaleView());


        }

        #region Propfull
        private decimal _currentDiscountAmount;
        public decimal CurrentDiscountAmount
        {
            get { return _currentDiscountAmount; }
            set { _currentDiscountAmount = value; OnPropertyChanged(); }
        }

        private DiscountModel _currentTotalDiscount;
        public DiscountModel CurrentTotalDiscount
        {
            get { return _currentTotalDiscount; }
            set { _currentTotalDiscount = value; OnPropertyChanged(); }
        }

        private List<DiscountModel> _discounts;
        public List<DiscountModel> Discounts
        {
            get { return _discounts; }
            set { _discounts = value; }
        }

        private bool isLoadding = false;

        public bool IsLoadding
        {
            get { return isLoadding = false; }
            set { isLoadding  = value; OnPropertyChanged(); }
        }


        private bool _openButton;
        public bool ShowOpenButton
        {
            get { return _openButton; }
            set { _openButton = value; OnPropertyChanged(); }
        }

        private List<ReceiptViewModel> _ticketList;
        public List<ReceiptViewModel> TicketList
        {
            get { return _ticketList; }
            set { _ticketList = value; OnPropertyChanged(); }
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

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        private int _currentSelectedOptionIndex;
        public int CurrentSelectedOptionIndex
        {
            get { return _currentSelectedOptionIndex; }
            set
            {
                _currentSelectedOptionIndex = value;
                OnPropertyChanged();
            }
        }

        private List<string> currentOption;
        public List<string> CurrentOption
        {
            get { return currentOption; }
            set
            {
                currentOption = value;
                OnPropertyChanged();
            }
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

        private ItemModel _currentItem;
        public ItemModel CurrentItem
        {
            get { return _currentItem; }
            set
            {
                _currentItem = value;
                OnPropertyChanged();
            }
        }

        private decimal _currentItemPrice;
        public decimal CurrentItemPrice
        {
            get { return _currentItemPrice; }
            set { _currentItemPrice = value; }
        }

        private List<MenuModel> _allMenu;
        public List<MenuModel> AllMenu
        {
            get { return _allMenu; }
            set { _allMenu = value; }
        }

        private List<CategoryModel> _categoriesList;
        public List<CategoryModel> CategoriesList
        {
            get { return _categoriesList; }
            set { _categoriesList = value; }
        }

        public List<TabItem> TabChildren = new List<TabItem>();
        public List<View> ViewChildren = new List<View>();

        #endregion

        #region Options
        private List<ItemOptionModel> _optionList;

        public List<ItemOptionModel> OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }

        public object ItemPrice { get; private set; }

        #endregion

        #region Discount
        private bool _totalDiscount1;
        public bool TotalDiscount1
        {
            get { return _totalDiscount1; }
            set
            {
                if (_totalDiscount1 != value)
                {
                    _totalDiscount1 = value;
                    if (value)
                    {
                        TotalDiscount2 = false;
                        TotalDiscount3 = false;
                        TotalDiscount4 = false;
                    }
                    OnPropertyChanged();
                    CalculateTotalAmount();
                }
            }
        }

        private bool _totalDiscount2;
        public bool TotalDiscount2
        {
            get { return _totalDiscount2; }
            set
            {
                if (_totalDiscount2 != value)
                {
                    _totalDiscount2 = value;
                    if (value)
                    {
                        TotalDiscount1 = false;
                        TotalDiscount3 = false;
                        TotalDiscount4 = false;
                    }
                    OnPropertyChanged();
                    CalculateTotalAmount();
                }
            }
        }

        private bool _totalDiscount3;
        public bool TotalDiscount3
        {
            get { return _totalDiscount3; }
            set
            {
                if (_totalDiscount3 != value)
                {
                    _totalDiscount3 = value;
                    if (value)
                    {
                        TotalDiscount2 = false;
                        TotalDiscount1 = false;
                        TotalDiscount4 = false;
                    }
                    OnPropertyChanged();
                    CalculateTotalAmount();
                }
            }
        }

        private bool _totalDiscount4;
        public bool TotalDiscount4
        {
            get { return _totalDiscount4; }
            set
            {
                if (_totalDiscount4 != value)
                {
                    _totalDiscount4 = value;
                    if (value)
                    {
                        TotalDiscount2 = false;
                        TotalDiscount3 = false;
                        TotalDiscount1 = false;
                    }
                    OnPropertyChanged();
                    CalculateTotalAmount();
                }
            }
        }

        private bool discount1;
        public bool Discount1
        {
            get { return discount1; }
            set
            {
                if (value)
                {
                    Discount2 = false;
                    Discount3 = false;
                    Discount4 = false;
                }
                discount1 = value;
                OnPropertyChanged();
            }
        }

        private string discountName1;
        public string DiscountName1
        {
            get { return discountName1; }
            set { discountName1 = value; OnPropertyChanged(); }
        }

        private bool hasDiscount1;
        public bool HasDiscount1
        {
            get { return hasDiscount1; }
            set { hasDiscount1 = value; OnPropertyChanged(); }
        }

        private bool discount2;
        public bool Discount2
        {
            get { return discount2; }
            set
            {
                if (value)
                {
                    Discount1 = false;
                    Discount3 = false;
                    Discount4 = false;
                }
                discount2 = value; OnPropertyChanged(); }
        }

        private string discountName2;
        public string DiscountName2
        {
            get { return discountName2; }
            set { discountName2 = value; OnPropertyChanged(); }
        }

        private bool hasDiscount2;
        public bool HasDiscount2
        {
            get { return hasDiscount2; }
            set { hasDiscount2 = value; OnPropertyChanged(); }
        }

        private bool discount3;
        public bool Discount3
        {
            get { return discount3; }
            set
            {
                if (value)
                {
                    Discount1 = false;
                    Discount2 = false;
                    Discount4 = false;
                }
                discount3 = value; OnPropertyChanged(); }
        }

        private string discountName3;
        public string DiscountName3
        {
            get { return discountName3; }
            set { discountName3 = value; OnPropertyChanged(); }
        }

        private bool hasDiscount3;
        public bool HasDiscount3
        {
            get { return hasDiscount3; }
            set { hasDiscount3 = value; OnPropertyChanged(); }
        }

        private bool discount4;
        public bool Discount4
        {
            get { return discount4; }
            set
            {
                if (value)
                {
                    Discount1 = false;
                    Discount2 = false;
                    Discount3 = false;
                }
                discount4 = value; OnPropertyChanged(); }
        }

        private string discountName4;
        public string DiscountName4
        {
            get { return discountName4; }
            set { discountName4 = value; OnPropertyChanged(); }
        }

        private bool hasDiscount4;
        public bool HasDiscount4
        {
            get { return hasDiscount4; }
            set { hasDiscount4 = value; OnPropertyChanged(); }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
