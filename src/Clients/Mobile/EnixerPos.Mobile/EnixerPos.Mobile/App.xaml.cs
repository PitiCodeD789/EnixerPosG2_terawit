
using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.Views;
using Plugin.DeviceInfo;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
﻿using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Mobile.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace EnixerPos.Mobile
{
    public partial class App : Application
    {
        public static List<ReceiptViewModel> TicketList { get; set; }
        public static string User { get; set; }
        public static string Email { get; set; }
        public static string StoreName { get; set; }
        public static int UserId { get; set; }
        public static bool CheckShift { get; set; } = false;
        public static int OpenShiftId { get; set; }
        public App()
        {
            InitializeComponent();
            TicketList = new List<ReceiptViewModel>();

            // MainPage = new MainPage();
            //  MainPage = new SaleView();
            //CategoryPageViewModel viewModel = new CategoryPageViewModel();
            //CreateItemPageViewModel createItem = new CreateItemPageViewModel();
            //CreateDiscountPageViewModel createDiscount = new CreateDiscountPageViewModel();
            //MainPage = new NavigationPage(new Views.Item.CreateDiscountPage(createDiscount));
            //MainPage = new NavigationPage(new SaleView());
          //  MainPage = new NavigationPage(new Views.SettingPage());
           // PermissionReq();
        }

        protected async override void OnStart()
        {
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            Email = await SecureStorage.GetAsync("Email");
            StoreName = await SecureStorage.GetAsync("StoreName");
            if (String.IsNullOrEmpty(refreshToken) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(StoreName))
            {
                MainPage = new NavigationPage(new Login())
                {
                    BackgroundColor = Color.White
                };
                PermissionReq();
            }
            else
            {
                MainPage = new NavigationPage(new EnterPin())
                {
                    BackgroundColor = Color.White
                };
                PermissionReq();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected async override void OnResume()
        {
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            Email = await SecureStorage.GetAsync("Email");
            StoreName = await SecureStorage.GetAsync("StoreName");
            if (String.IsNullOrEmpty(refreshToken) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(StoreName))
            {
                MainPage = new NavigationPage(new Login())
                {
                    BackgroundColor = Color.White
                };
                PermissionReq();
            }
            else
            {
                MainPage = new NavigationPage(new EnterPin())
                {
                    BackgroundColor = Color.White
                };
                PermissionReq();
            }
        }

        private async void PermissionReq()
        {
            try
            {
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

                
                if (storageStatus != PermissionStatus.Granted)
                {
                    storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                if (storageStatus != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "Can not use EnixerPos Application with out Storage Permission", "Ok");
                    Environment.Exit(0);
                }

            }
            catch (Exception ex)
            {
                Environment.Exit(0);
            }
        }
    }
}
