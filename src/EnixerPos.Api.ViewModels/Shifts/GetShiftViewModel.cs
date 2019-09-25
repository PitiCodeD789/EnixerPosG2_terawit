using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Shifts
{
    public class GetShiftViewModel
    {
      
        public decimal  StartingCash { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CashRefunds { get; set; }
        public decimal Paidin { get; set; }
        public decimal Paidout { get; set; }
        
       
        public decimal Refunds { get; set; }
        public decimal Discount { get; set; }
       
      
        public decimal DebitCard { get; set; }
        public decimal CreditCard { get; set; }
        public decimal QRCode { get; set; }
      
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }




    }
}
