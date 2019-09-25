using EnixerPos.Api.ViewModels.Shifts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ShiftPopUpPageViewModel : BaseViewModel
    {
        //public List<GetShiftViewModel> testData { get; set; }

        public ObservableCollection<GetShiftViewModel> GetShiftListViewModel { get; set; }
        public ShiftPopUpPageViewModel()
        {
            //GetShiftListViewModel = new ObservableCollection<GetShiftViewModel>
            //{ new GetShiftViewModel
            //{ CreateDateTime = DateTime.Now, UpdateDateTime = DateTime.Now.AddHours(12),
            //    StartingCash = 5005, CashPayment = 500, Cash = 1500, CashRefunds = 0, CreditCard = 500, DebitCard = 1500, Discount = 250, Grosssales = 550, Paidin = 200 },
            //    new GetShiftViewModel{ CreateDateTime = DateTime.Now,UpdateDateTime = DateTime.Now.AddHours(1), StartingCash = 600},
            //     new GetShiftViewModel{ CreateDateTime = DateTime.Now.AddHours(5),UpdateDateTime = DateTime.Now.AddHours(2), StartingCash = 700},
            //      new GetShiftViewModel{ CreateDateTime = DateTime.Now.AddHours(6),UpdateDateTime = DateTime.Now.AddHours(3)},
            //       new GetShiftViewModel{ CreateDateTime = DateTime.Now,UpdateDateTime = DateTime.Now.AddHours(4)},
            //        new GetShiftViewModel{ CreateDateTime = DateTime.Now,UpdateDateTime = DateTime.Now.AddHours(5)},
            //         new GetShiftViewModel{ CreateDateTime = DateTime.Now,UpdateDateTime = DateTime.Now.AddHours(6)},
            //};
            ShiftCloseClickCommand = new Command(ShiftCloseClick);



        }

        private void ShiftCloseClick(object obj)
        {
            throw new NotImplementedException();
        }

        private GetShiftViewModel shiftSelect;

        public GetShiftViewModel ShiftSelect
        {
            get { return shiftSelect; }
            set
            {
                if(shiftSelect != value)
                {
                    shiftSelect = value;
                    itemSectecHandle(shiftSelect);
                }
                
            }
        }

        private async void itemSectecHandle(GetShiftViewModel viewModel)
        {
            //Application.Current.MainPage.DisplayAlert("", "Error", "Ok");
            await PopupNavigation.Instance.PushAsync(new Views.Popup.ShiftReportPopup(viewModel));
        }


      

        public ICommand ShiftDetailClickCommand { get; set; }
        public ICommand ShiftCloseClickCommand { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

      




    }
}
