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
    public partial class Pos_ButtonView : ContentView
    {
        public Pos_ButtonView()
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

        private string textButton;

        public string TextButton
        {
            get { return textButton; }
            set
            {
                textButton = value;
                mButton.Text = textButton;
            }
        }
        #endregion

        #region BackgroundButton
        private Color backgroundButton;

        public Color BackgroundButton
        {
            get { return backgroundButton; }
            set
            {
                backgroundButton = value;
                mButton.BackgroundColor = backgroundButton;
            }
        }
        #endregion

        #region TextColor
        private Color textColor;

        public Color TextColor
        {
            get { return textColor; }
            set
            {
                textColor = value;
                mButton.TextColor = textColor;
            }
        }
        #endregion
    }
}