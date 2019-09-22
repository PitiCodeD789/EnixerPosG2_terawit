using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CashManagePageViewModel : BaseViewModel
    {
        public CashManagePageViewModel()
        {
            PayinClickCommand = new Command(PayinClick);
            PayoutClickCommand = new Command(PayoutClick);

        }

        private void PayoutClick(object obj)
        {
            throw new NotImplementedException();
        }

        private void PayinClick(object obj)
        {
            string compare = obj.ToString();
        }

        public ICommand PayinClickCommand { get; set; }
        public ICommand PayoutClickCommand { get; set; }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; OnPropertyChanged("Amount"); }
        }

        private string comment;

        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged("Comment"); }
        }
    }
}
