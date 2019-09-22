using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Product
{
    public class ItemsViewModel
    {
        public string Message { get; set; }
        public List<ItemModel> Items { get; set; }
        public bool IsError { get; set; }
    }
}
