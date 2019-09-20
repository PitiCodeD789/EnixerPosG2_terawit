using System;
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
        public int Discount { get; set; }
        public decimal Total { get; set; }
    }
}
