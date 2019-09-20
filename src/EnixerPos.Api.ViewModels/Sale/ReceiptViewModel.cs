using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class ReceiptViewModel
    {
        public string Reference { get; set; }
        public int ShiftId { get; set; }
        public string Store { get; set; }
        public string Pos { get; set; }
        public List<OrderItemModel> ItemList { get; set; }
        public decimal Discount { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
        public Enixer_Enumerations.EP_PaymentTypeEnum PaymentType { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
