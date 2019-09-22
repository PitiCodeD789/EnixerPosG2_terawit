using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Mobile.Models;
using EnixerPos.Mobile.Views.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels.ItemPage
{
    public class SubItemViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public SubItemViewModel()
        {
            itemDate = GetItemData().Result;
            InputDataToBinding();
            EditData = new Command<string>(EditDataMethod);
            CreateData = new Command(CreateDataMethod);
        }

        private List<ItemPageModel> theData;
        public List<ItemPageModel> TheData
        {
            get
            {
                return theData;
            }
            set
            {
                theData = value;
                OnPropertyChanged();
            }
        }

        private List<ItemModel> itemDate;

        private async Task<List<ItemModel>> GetItemData()
        {
            List<ItemModel> mock = new List<ItemModel>()
            {
                new ItemModel()
                {
                    Color = "#7DDC36",
                    Name = "Ice Tea",
                    Price = 60.25m
                },
                 new ItemModel()
                {
                    Color = "#7DDC36",
                    Name = "Black Tea",
                    Price = 55.50m
                }
            };

            var result = mock;
            return result;
        }

        private void InputDataToBinding()
        {
            theData = itemDate.Select(x => new ItemPageModel()
            {
                Color = x.Color,
                DataName = x.Name,
                CountItemVisible = false,
                Number = x.Price.ToString("#,###.00"),
                NumberVisible = true
            }).ToList();
        }

        public virtual ICommand EditData { get; set; }
        public async void EditDataMethod(string dataName)
        {
            
        }

        public virtual ICommand CreateData { get; set; }
        public async void CreateDataMethod()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CreateItem());
        }
    }
}
