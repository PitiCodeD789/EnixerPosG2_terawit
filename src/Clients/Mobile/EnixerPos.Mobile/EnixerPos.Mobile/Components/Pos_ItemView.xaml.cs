using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pos_ItemView : ContentView
    {
        public Pos_ItemView()
        {
            InitializeComponent();
        }

        #region CommandButton
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(Pos_ButtonView));
        private void MButton_Clicked(object sender, EventArgs e)
        {
            Command?.Execute(null);
        }

        #endregion

        #region TextButton

        public string TextButton
        {
            get { return (string)GetValue(TextButtonProperty); }
            set { SetValue(TextButtonProperty, value); }
        }

        public static readonly BindableProperty TextButtonProperty =
            BindableProperty.Create(
                propertyName: "TextButton",
                returnType: typeof(string),
                declaringType: typeof(Pos_ItemView),
                defaultBindingMode: BindingMode.TwoWay); //คนที่ประกาศ
        #endregion//

    }
}