using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EnixerPos.Mobile.ViewModels
{
    public class CreateItemPageViewModel : BaseViewModel
    {
        public CreateItemPageViewModel()
        {
            ColorSelectCommand = new Command(ColorSelect);

        }

        private string itemName;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        private string itemPrice;

        public string ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        private string itemCost;

        public string ItemCost
        {
            get { return itemCost; }
            set { itemCost = value; }
        }

        private string option1;
            
        public string Option1
        {
            get { return option1; }
            set { option1 = value; }
        }

        private string price1;

        public string Price1
        {
            get { return price1; }
            set { price1 = value; }
        }


        private string option2;

        public string Option2
        {
            get { return option2; }
            set { option2 = value; }
        }

        private string price2;

        public string Price2
        {
            get { return price2; }
            set { price2 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// 
        private string option3;
        public string Option3
        {
            get { return option3; }
            set { option3 = value; }
        }

        private string price3;

        public string Price3
        {
            get { return price3; }
            set { price3 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// 
        private string option4;
        public string Option4
        {
            get { return option4; }
            set { option4 = value; }
        }

        private string price4;

        public string Price4
        {
            get { return price4; }
            set { price4 = value; }
        }


        private void ColorSelect(object obj)
        {
            int id = int.Parse(obj.ToString());

            setColor(id);
        }

        public ICommand ColorSelectCommand { get; set; }

        private void setColor(int colorIndex)
        {
            if (colorIndex == 1)
            {
                Color1 = true;
            }
            else
            {
                Color1 = false;
            }

            if (colorIndex == 2)
            {
                Color2 = true;
            }
            else
            {
                Color2 = false;
            }
            if (colorIndex == 3)
            {
                Color3 = true;
            }
            else
            {
                Color3 = false;
            }

            if (colorIndex == 4)
            {
                Color4 = true;
            }
            else
            {
                Color4 = false;
            }

            if (colorIndex == 5)
            {
                Color5 = true;
            }
            else
            {
                Color5 = false;
            }

            if (colorIndex == 6)
            {
                Color6 = true;
            }
            else
            {
                Color6 = false;
            }

        }

        private bool color1 = true;

        public bool Color1
        {
            get { return color1; }
            set { color1 = value; OnPropertyChanged(); }
        }

        private bool color2;

        public bool Color2
        {
            get { return color2; }
            set { color2 = value; OnPropertyChanged(); }
        }

        private bool color3;

        public bool Color3
        {
            get { return color3; }
            set { color3 = value; OnPropertyChanged(); }
        }

        private bool color4;

        public bool Color4
        {
            get { return color4; }
            set { color4 = value; OnPropertyChanged(); }
        }
        private bool color5;

        public bool Color5
        {
            get { return color5; }
            set { color5 = value; OnPropertyChanged(); }
        }
        private bool color6;

        public bool Color6
        {
            get { return color6; }
            set { color6 = value; OnPropertyChanged(); }
        }
    }
}
