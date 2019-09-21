using System.Collections.Generic;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class OrderItemModel
    {
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ItemDiscount { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public string OptionName { get; set; }
        public decimal OptionPrice { get; set; }
        public bool IsDiscounted { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}