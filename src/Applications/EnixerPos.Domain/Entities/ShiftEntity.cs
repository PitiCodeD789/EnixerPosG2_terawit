using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class ShiftEntity : BaseEntity
    {
        public int ShiftId { get; internal set; }
        public string StoreEmail { get; internal set; }
        public int PosUserId { get; internal set; }       
        public decimal StartingCash { get; internal set; }
        public decimal CashPayment { get; set; }
        public decimal CashRefunds { get; set; }
        public decimal Paidin { get; set; }
        public decimal Paidout { get; set; }
        public decimal Refunds { get; set; }
        public decimal Discount { get; set; }
        public decimal DebitCard { get; set; }
        public decimal CreditCard { get; set; }
        public decimal QRCode { get; set; }
        public bool Available { get;  set; }
    }
}
