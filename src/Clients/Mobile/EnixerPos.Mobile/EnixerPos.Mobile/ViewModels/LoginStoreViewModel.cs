using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EnixerPos.Mobile.ViewModels
{
    public class LoginStoreViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public LoginStoreViewModel()
        {

        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }


    }
}
