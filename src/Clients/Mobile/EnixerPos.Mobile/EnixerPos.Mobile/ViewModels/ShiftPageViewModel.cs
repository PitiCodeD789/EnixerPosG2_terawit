﻿using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ShiftPageViewModel : BaseViewModel
    {
        private IShiftService _shiftService = new ShiftService();
        public GetShiftViewModel GetShiftView { get; set; }
        public ShiftPageViewModel()
        {
            
            ShowShiftListCommand = new Command(ShowShiftList);
            CloseShiftListCommand = new Command(CloseShiftList);
            CashManagePageClickCommand = new Command(CashManagePageClick);
            ShiftReportListClickCommand = new Command(ShiftReportListClick);



        }

        private async void ShiftReportListClick(object obj)
        {
          
            var shiftListVM = await _shiftService.GetListShift();
            ObservableCollection<GetShiftViewModel> shiftListVMCollection = new ObservableCollection<GetShiftViewModel>(shiftListVM);
            ShiftPopUpPageViewModel shiftPopUp = new ShiftPopUpPageViewModel();
            shiftPopUp.GetShiftListViewModel = shiftListVMCollection;
            await PopupNavigation.PushAsync(new Views.Popup.ShiftPopUpPage(shiftPopUp));
        }

      
        private async void CashManagePageClick(object obj)
        {
            await PopupNavigation.PushAsync(new Views.Popup.LoadingPopup());
            await Application.Current.MainPage.Navigation.PushAsync(new Views.CashManagePage());
             

        }

        private void CloseShiftList(object obj)
        {
            var isClose = _shiftService.CloseListShift(App.OpenShiftId, App.UserId);
            if(isClose)
            {
                //App.Current.MainPage.DisplayAlert("ok", "Close Ok", "ok");
                App.CheckShift = false;
                App.Current.MainPage = new NavigationPage(new Views.EnterPin());

            }else
            {
                App.Current.MainPage.DisplayAlert("ok", "Can not close", "ok");
            }
        }

        private async void ShowShiftList(object obj)
        {
            var shiftListVM = await _shiftService.GetListShift();
            ObservableCollection<GetShiftViewModel> shiftListVMCollection = new ObservableCollection<GetShiftViewModel>(shiftListVM);
            ShiftPopUpPageViewModel shiftPopUp = new ShiftPopUpPageViewModel();
            shiftPopUp.GetShiftListViewModel = shiftListVMCollection;
            await PopupNavigation.PushAsync(new Views.Popup.ShiftPopUpPage(shiftPopUp));
        }

        public ICommand ShowShiftListCommand { get; set; }
        public ICommand CloseShiftListCommand { get; set; }
        public ICommand ShiftReportListClickCommand { get; set; }
        public ICommand CashManagePageClickCommand { get; set; }

    }
}
