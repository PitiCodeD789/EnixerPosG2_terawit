using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Services
{
    public class ProductService : BaseService,IProductService
    {
        private string serviceUrl = Helper.BaseUrl + "Product/";

        public async Task<ResultServiceModel<ResultViewModel>> CreateItem(ItemModel item)
        {
            string url = serviceUrl + "AddItem";

            return await Post<ResultViewModel>(url, item);
        }

        public Task<ResultServiceModel<CategoriesViewModel>> GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}
