using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnixerPos.Web.Models
{
    public class CategoriesWebModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int StoreId { get; set; }
        public int CountItem { get; set; }
    }
}
