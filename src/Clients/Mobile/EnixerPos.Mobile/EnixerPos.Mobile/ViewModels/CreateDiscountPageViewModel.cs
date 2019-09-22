﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CreateDiscountPageViewModel : BaseViewModel
    {
        public CreateDiscountPageViewModel()
        {
            CreateDiscountCommand = new Command(CreateDiscount);
            CancelCategoryCommand = new Command(CancelCategory);
        }

        private void CancelCategory(object obj)
        {
            throw new NotImplementedException();
        }

        private void CreateDiscount(object obj)
        {
            throw new NotImplementedException();
        }

      

        public ICommand CreateDiscountCommand { get; set; }
        public ICommand CancelCategoryCommand { get; set; }

        private string discountName;

        public string DiscountName
        {
            get { return discountName; }
            set { discountName = value; }
        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }


    }
}