using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Product
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int StoreId { get; set; }
        public int CountItem { get; set; }
    }
}
