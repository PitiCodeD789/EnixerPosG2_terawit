using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Service.Models;

namespace EnixerPos.Service.Interfaces
{
    public interface IProductService
    {
        Task<ResultViewModel> CreateItem(ItemModel item);
        Task<CategoriesViewModel> GetAllCategories();
        Task<bool> AddCategory(string categoryName, string color);
        Task<bool> AddDiscount(string discountName, bool discountType, string amount);
    }
}
