using System.Collections.Generic;

namespace EnixerPos.Api.ViewModels.Sale
{
    public class OrderItemModel
    {
        public int ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemDiscount { get; set; }
        public int Quantity { get; set; }
        public List<int> Option { get; set; }
    }
}