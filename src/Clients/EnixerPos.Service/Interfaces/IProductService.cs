using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddCategory(string categoryName, string color);
        Task<bool> AddDiscount(string discountName, bool discountType, string amount);
    }
}
