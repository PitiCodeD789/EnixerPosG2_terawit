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
    public class ProductService : BaseService, IProductService
    {
        private string serviceUrl = Helper.BaseUrl + "Product/";
       
        public async Task<bool> AddCategory(string Name, string Color)
        {
            serviceUrl += "AddCategory";
            CategoryModel category = new CategoryModel { Color = Color, Name = Name };
            ResultServiceModel<ResultViewModel> result = await Post<ResultViewModel>(serviceUrl, category);

            if(result.Model.IsError == true)
            {
                return false;
            }else
            {
                return true;

            }
         
          

        }

        public async Task<bool> AddDiscount(string discountName, bool discountType, string amount)
        {
            serviceUrl += "AddDiscount";
            DiscountModel discountModel = new DiscountModel { Amount = decimal.Parse(amount), DiscountName = discountName, IsPercentage = discountType };
            ResultServiceModel<ResultViewModel> result = await Post<ResultViewModel>(serviceUrl, discountModel);

            if (result.Model.IsError == true)
            {
                return false;
            }
            else
            {
                return true;

            }

        }
    }
}
