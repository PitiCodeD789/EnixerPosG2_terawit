using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile
{
    public partial class App : Application
    {
        public static List<ReceiptViewModel> TicketList { get; set; }
        public App()
        {
            InitializeComponent();
            TicketList = new List<ReceiptViewModel>();

            // MainPage = new MainPage();
            MainPage = new SaleView();
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
