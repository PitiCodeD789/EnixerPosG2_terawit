using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels
{
    public class DiscountDto : BaseDto
    {
        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPercentage { get; set; }
        public int StoreId { get; set; }
    }
}
