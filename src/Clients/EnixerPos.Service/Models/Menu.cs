using EnixerPos.Api.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EnixerPos.Service.Models
{
    public class MenuModel
    {
        public string CategoryName { get; set; }
        public ObservableCollection<ItemModel> Items { get; set; }
    }
}
