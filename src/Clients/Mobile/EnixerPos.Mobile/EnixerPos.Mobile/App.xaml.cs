﻿
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
        public static string DeviceId { get; set; }
        public static string User { get; set; }
        public static string Email { get; set; }
        public static string StoreName { get; set; }
        public static string PosName { get; set; }
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
            MainPage = new NavigationPage(new Login());
            PermissionReq();
        }

        protected async override void OnStart()
        {
            DeviceId = CrossDevice.Device.DeviceId;
            //var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            //if (refreshToken != null)
            //{
            //    MainPage = new NavigationPage(new EnterPin());
            //    PermissionReq();
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new Login());
            //    PermissionReq();
            //}
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
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
