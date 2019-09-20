﻿using System.Collections.Generic;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class OrderItemModel
    {
        public int ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemDiscount { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public int Quantity { get; set; }
        public string OptionName { get; set; }
        public decimal OptionPrice { get; set; }

    }
}