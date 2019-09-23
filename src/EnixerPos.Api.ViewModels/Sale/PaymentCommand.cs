using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class PaymentCommand
    {
        public int ShiftId { get; set; }
        public string StoreEmail { get; set; }
        public List<OrderItemModel> ItemList { get; set; }
        public decimal Discount { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
        public Enixer_Enumerations.EP_PaymentTypeEnum PaymentType { get; set; }
    }
}
