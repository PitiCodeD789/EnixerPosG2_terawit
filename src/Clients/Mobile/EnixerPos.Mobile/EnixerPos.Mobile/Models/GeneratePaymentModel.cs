using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.Models
{
    public class GeneratePaymentModel
    {
        public string FirstName { get; set; }
        public string AccountNumber { get; set; }
        public string CheckSum { get; set; }
        public decimal Amount { get; set; }
        public string TransactionReference { get; set; }
    }
}
