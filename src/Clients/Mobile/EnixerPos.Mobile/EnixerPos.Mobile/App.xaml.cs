
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
            //CategoryPageViewModel viewModel = new CategoryPageViewModel();
            //MainPage = new NavigationPage(new Views.Item.CreateCategoryPage(viewModel));
            MainPage = new NavigationPage(new ItemPage());
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
