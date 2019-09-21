using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EnixerPos.Mobile.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pos_InlineLable : ContentView
    {
        public Pos_InlineLable()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public string LeftTexLable
        {
            get { return (string)GetValue(LeftTexLableProperty); }
            set { SetValue(LeftTexLableProperty, value); }
        }

        public static readonly BindableProperty LeftTexLableProperty =
            BindableProperty.Create(
                propertyName: "LeftTexLable",
                returnType: typeof(string),
                declaringType: typeof(Pos_InlineLable),
                defaultBindingMode: BindingMode.TwoWay);

        public string RightTexLable
        {
            get { return (string)GetValue(RightTexLableProperty); }
            set { SetValue(RightTexLableProperty, value); }
        }

        public static readonly BindableProperty RightTexLableProperty =
            BindableProperty.Create(
                propertyName: "RightTexLable",
                returnType: typeof(string),
                declaringType: typeof(Pos_InlineLable),
                defaultBindingMode: BindingMode.TwoWay);

        private Color labletextColor = Color.Black;

        public Color LableTextColor
        {
            get { return labletextColor; }
            set
            {
                labletextColor = value;
                mLeftTexLable.TextColor = labletextColor;
                mRightTexLable.TextColor = labletextColor;
            }
        }

        private Thickness lableTextMargin;

        public Thickness LableTextMargin
        {
            get { return lableTextMargin; }
            set
            {
                lableTextMargin = value;
                mLeftTexLable.Margin = lableTextMargin;
                mRightTexLable.Margin = lableTextMargin;
            }
        }


        private FontAttributes lableFontAttributes = FontAttributes.None;

        public FontAttributes LableFontAttributes
        {
            get { return lableFontAttributes; }
            set
            {
                lableFontAttributes = value;
                mLeftTexLable.FontAttributes = lableFontAttributes;
                mRightTexLable.FontAttributes = lableFontAttributes;
            }
        }





    }
}