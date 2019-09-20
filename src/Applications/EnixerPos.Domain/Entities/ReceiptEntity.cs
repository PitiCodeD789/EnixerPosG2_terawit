using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class ReceiptEntity
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int ShiftId { get; set; }
        public string StoreEmail { get; set; }
        public string PosImei { get; set; }
        public string ItemList { get; set; }
        public int Discount { get; set; }
        public decimal Total { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
