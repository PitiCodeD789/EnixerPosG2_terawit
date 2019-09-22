using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EnixerPos.Mobile.ViewModels
{
    public class ItemMainViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ItemMainViewModel()
        {
            categoryName = GetCategoriese().Result;
            string a = "";
        }

        private List<string> categoryName;
        public List<string> CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
                OnPropertyChanged();
            }
        }

        public async Task<List<string>> GetCategoriese()
        {
            //var categoriesData = 
            List<string> result = new List<string>()
            {
                "Drink",
                "Food",
                "Other"
            };
            return result;
        }

    }
}
