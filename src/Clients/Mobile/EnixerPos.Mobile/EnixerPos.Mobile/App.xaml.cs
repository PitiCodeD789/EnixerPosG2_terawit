
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
using EnixerPos.Api.ViewModels.Product;

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
        public static string AccountNumber { get; set; }
        public static List<CategoryModel> ListCategoryModels { get; set; }
        public static ItemsViewModel ItemsManuViewModel { get; set; }
        public App()
        {
            //SecureStorage.SetAsync("Token","eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NzkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.KenpoWXIjdFh1OulXkLftQZ1to4aBM33Lv-FmwYRTyY");

            InitializeComponent();
            TicketList = new List<ReceiptViewModel>();

            // MainPage = new MainPage();
            //  MainPage = new SaleView();
            //CategoryPageViewModel viewModel = new CategoryPageViewModel();
            //CreateItemPageViewModel createItem = new CreateItemPageViewModel();
            //CreateDiscountPageViewModel createDiscount = new CreateDiscountPageViewModel();
            //MainPage = new NavigationPage(new Views.Item.CreateDiscountPage(createDiscount));
            //MainPage = new NavigationPage(new SaleView());
            //  MainPage = new NavigationPage(new Views.ItemPage());
           // PermissionReq();
        }

        protected async override void OnStart()
        {
            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            Email = await SecureStorage.GetAsync("Email");
            StoreName = await SecureStorage.GetAsync("StoreName");
            AccountNumber = await SecureStorage.GetAsync("AccountNumber");
            if (String.IsNullOrEmpty(refreshToken) || String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(StoreName) || String.IsNullOrEmpty(AccountNumber))
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
                Application.Current.MainPage = new NavigationPage(new Login())
                {
                    BackgroundColor = Color.White
                };
                PermissionReq();
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new EnterPin())
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
