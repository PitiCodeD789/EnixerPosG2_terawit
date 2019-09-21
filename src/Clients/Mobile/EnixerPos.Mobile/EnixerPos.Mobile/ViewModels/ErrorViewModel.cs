using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class ErrorViewModel
    {
        public Action MyAction { get; set; }
        public ErrorViewModel()
        {
            TextError = "ทำรายการไม่สำเร็จ";
            MyAction = Pop;
            ImageError = "icon_Error";

            ClosePopup = new Command(PopPopup);
        }
        public ErrorViewModel(string title)
        {
            TextError = title;
            MyAction = Pop;
            ImageError = "icon_Error";


            ClosePopup = new Command(PopPopup);
        }
        public ErrorViewModel(string title, int errorType)
        {
            TextError = title;
            MyAction = Pop;
            if ((int)errorType == 0)
            {
                ImageError = "icon_Warning";
            }
            else if(((int)errorType == 1))
            {
                ImageError = "icon_Error";
            }
            else
            {
                ImageError = "icon_Correct";
            }

            ClosePopup = new Command(PopPopup);
        }
        public ErrorViewModel(string title, Action action)
        {
            TextError = title;
            MyAction = action == null ? Pop : action;
            ImageError = "icon_Error";

            ClosePopup = new Command(PopPopup);
        }
        public ErrorViewModel(string title, int errorType, Action action)
        {
            TextError = title;
            MyAction = action == null ? Pop : action;
            if ((int)errorType == 0)
            {
                ImageError = "icon_Warning";
            }
            else
            {
                ImageError = "icon_Error";
            }

            ClosePopup = new Command(PopPopup);
        }
        public ICommand ClosePopup { get; set; }
        public void PopPopup()
        {
            MyAction?.Invoke();
        }

        public void Pop()
        {
            Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        public string TextError { get; set; }
        public string ImageError { get; set; }
    }
}
