﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Sale
{
    public class PaymentDtoCommand
    {
        public int ShiftId { get; set; }
        public string StoreEmail { get; set; }
        public string PosImei { get; set; }
        public List<OrderItemModel> ItemList { get; set; }
        public decimal Discount { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
