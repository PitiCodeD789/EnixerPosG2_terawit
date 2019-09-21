using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.ViewModels
{
    public class CashManagePageViewModel
    {
        public CashManagePageViewModel()
        {

        }

        private string amount;

        public string Amount
        {
            get { return amount; }
            set { amount = value; }
        }

    }
}
