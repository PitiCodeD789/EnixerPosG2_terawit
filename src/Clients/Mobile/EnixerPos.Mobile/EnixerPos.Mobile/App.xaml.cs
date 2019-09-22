
using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.Views;
using Plugin.DeviceInfo;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile
{
    public partial class App : Application
    {
        public static string DeviceId { get; set; }

        public App()
        {
            InitializeComponent();

            // MainPage = new MainPage();
            //  MainPage = new SaleView();
            CategoryPageViewModel viewModel = new CategoryPageViewModel();
            MainPage = new NavigationPage(new Views.Item.CreateCategoryPage(viewModel));
            PermissionReq();
        }

        protected override void OnStart()
        {
            DeviceId = CrossDevice.Device.DeviceId;
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
