using EnixerPos.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual ICommand BackButton { get; set; }
        public async virtual void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public virtual void ForceLogout()
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        public virtual void CloseApp()
        {
            SecureStorage.RemoveAll();
            Environment.Exit(0);
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
