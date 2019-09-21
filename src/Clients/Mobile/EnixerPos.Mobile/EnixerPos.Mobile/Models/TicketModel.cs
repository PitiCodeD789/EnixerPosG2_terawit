using EnixerPos.Api.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.Models
{
    public class TicketModel
    {
        public string Name { get; set; }
        public List<OrderItemModel> OrderItemList { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
