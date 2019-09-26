using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnixerPos.Web.Models
{
    public class DiscountWebModel
    {
        public string DiscountName { get; set; }
        public decimal Amount { get; set; }
        public bool IsPercentage { get; set; }
        public int StoreId { get; set; }
    }
}
