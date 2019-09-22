
using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            //  MainPage = new SaleView();
            CategoryPageViewModel viewModel = new CategoryPageViewModel();
            CreateItemPageViewModel createItem = new CreateItemPageViewModel();
            CreateDiscountPageViewModel createDiscount = new CreateDiscountPageViewModel();
            MainPage = new NavigationPage(new Views.Item.CreateDiscountPage(createDiscount));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
