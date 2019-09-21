using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class DiscountEntity : BaseEntity
    {
        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPercentage { get; set; }
    }
}
