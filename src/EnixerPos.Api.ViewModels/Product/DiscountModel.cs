using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Product
{
    public class DiscountModel : BaseModel
    {
        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPercentage { get; set; }
        public int StoreId { get; set; }
    }
}
