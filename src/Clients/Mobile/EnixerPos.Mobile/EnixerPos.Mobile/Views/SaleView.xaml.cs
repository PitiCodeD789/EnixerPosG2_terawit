using DLToolkit.Forms.Controls;
using EnixerPos.Mobile.ViewModels;
using EnixerPos.Mobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaleView : ContentPage
    {
        List<Button> btnList = new List<Button>();
        List<BoxView> lineList = new List<BoxView>();
        SaleViewModel vm = new SaleViewModel();
        public SaleView()
        {
            InitializeComponent();
            BindingContext = vm;
            foreach (var itemSource in vm.TabChildren)
            {
                //TabView.AddTab(itemSource);
                StackLayout stack = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                };
                BoxView line = new BoxView()
                {
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 3,
                };
                Button button = new Button()
                {
                    Text = itemSource.HeaderText,
                    FontSize = 20,
                    BackgroundColor = Color.Transparent,
                };
                stack.Children.Add(line);
                stack.Children.Add(button);
                TabChangeButton.Children.Add(stack);
                button.Clicked += OpenTab;
                btnList.Add(button);
                lineList.Add(line);
            }
            TabView.Children.Add(vm.ViewChildren[0]);
            HeaderLabel.Text = vm.TabChildren[0].HeaderText;
            lineList[0].BackgroundColor = Color.Orange;
        }

        private void OpenTab(object sender, EventArgs e)
        {
            string btnText = (sender as Button).Text;
            var btnIndex = btnList.FindIndex(b => b.Text == btnText);
            foreach (var line in lineList)
            {
                line.BackgroundColor = Color.Transparent;
            }
            lineList[btnIndex].BackgroundColor = Color.Orange;
            var view = vm.TabChildren.Where(t => t.HeaderText == btnText).FirstOrDefault();
            TabView.Children.Clear();
            TabView.Children.Add(view.Content);
            HeaderLabel.Text = view.HeaderText;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new SideMenu());
        }

        protected  override void OnAppearing()
        {
            base.OnAppearing();
            if (!App.CheckShift)
            {
                Navigation.PushPopupAsync(new OpenShiftButtonPopup());
            }
        }
    }
}