using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.ViewModels
{
    public class CategoryPageViewModel : BaseViewModel
    {
        public CategoryPageViewModel()
        {

        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

    }
}
