using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EnixerPos.Mobile.Models
{
    public class ShowOrderModel
    {
        public string ItemName { get; set; }
        public string QuantityAndPrice { get; set; }
        public string ItemDiscount { get; set; }
        public bool IsDiscount { get; set; }
        public bool IsOption { get; set; }
        public string ItemOption { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public TextDecorations Strike { get; set; }

    }
}
