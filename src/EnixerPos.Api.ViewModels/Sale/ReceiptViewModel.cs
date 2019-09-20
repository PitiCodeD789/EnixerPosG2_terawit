using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class ReceiptViewModel
    {
        public int ReceiptId { get; set; }
        public string ReceiptData { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
