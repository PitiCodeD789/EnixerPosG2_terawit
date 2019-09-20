using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Product
{
    public class ItemModel : BaseModel
    {
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public ItemOptionModel[] ItemOptions { get; set; }
        public string Color { get; set; }
    }
}
