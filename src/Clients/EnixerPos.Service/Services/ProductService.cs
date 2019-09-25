using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EnixerPos.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        private string serviceUrl = Helper.BaseUrl + "Product/";
        private string checkPaymentUrl = "http://192.168.1.18:30000/api/transaction/CheckReference/1234";

        private string accessToken = SecureStorage.GetAsync("Token").Result;
        public async Task<ResultViewModel> CreateItem(ItemModel item)
        {
            try
            {
                string url = serviceUrl + "AddItem";
                var result = await Post<ResultViewModel>(url, item);
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new ResultViewModel() { IsError = true };
            }
            catch (Exception)
            {

                return new ResultViewModel() { IsError = true };
            }

        }

        public async Task<CategoriesViewModel> GetAllCategories()
        {
            try
            {
                var result = Get<CategoriesViewModel>(serviceUrl + "getcategories").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new CategoriesViewModel() { Categories = new List<CategoryModel>(),IsError = true };
            }
            catch (Exception e)
            {
                return new CategoriesViewModel() { Categories = new List<CategoryModel>(),IsError = true };
            }
        }
        
        public ItemsViewModel GetItems()
        {
            try
            {
                var result = Get<ItemsViewModel>(serviceUrl + "getitems").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new ItemsViewModel {IsError = true,Items = new List<ItemModel>()};
            }
            catch (Exception e)
            {
                return new ItemsViewModel { IsError = true, Items = new List<ItemModel>()}; 
            }
        }

        public ReceiptViewModel AddPayment(PaymentCommand payment)
        {
            try
            {
                var result =  Post<ReceiptViewModel>(Helper.BaseUrl + "sale/payment", payment).Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new ReceiptViewModel();
            }
            catch (Exception e)
            {
                return new ReceiptViewModel();
            }
        }

        public bool CheckPayment()
        {
            try
            {
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public DiscountsViewModel GetDiscounts()
        {
            try
            {
                var result = Get<DiscountsViewModel>(serviceUrl+"getdiscounts").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new DiscountsViewModel() { IsError = true, Discounts = new List<DiscountModel>() };
            }
            catch (Exception e)
            {
                return new DiscountsViewModel() { IsError = true, Discounts = new List<DiscountModel>() };
            }
        }
            public async Task<bool> AddCategory(string Name, string Color)
            {
                serviceUrl += "AddCategory";
                CategoryModel category = new CategoryModel { Color = Color, Name = Name };
                ResultServiceModel<ResultViewModel> result = await Post<ResultViewModel>(serviceUrl, category);

                if (result.Model.IsError == true)
                {
                    return false;
                } else
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

        public async Task<ResultViewModel> UpdateItem(ItemModel item)
        {
            try
            {
                var result = await Post<ResultViewModel>(serviceUrl + "UpdateItem",item);
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }
                return new ResultViewModel() { IsError = true};
            }
            catch (Exception)
            {

                return new ResultViewModel() { IsError = true };
            }
           
        }

        public async Task<ResultViewModel> UpdateCategory(CategoryModel item)
        {
            try
            {
                var result = await Post<ResultViewModel>(serviceUrl + "UpdateCategory", item);
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<ResultViewModel> UpdateDiscount(DiscountModel item)
        {
            try
            {
                var result = await Post<ResultViewModel>(serviceUrl + "UpdateDiscount", item);
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public bool CheckQrPayment(string referenceNumber)
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = client.GetAsync(checkPaymentUrl).Result;
                client.Timeout = TimeSpan.FromSeconds(20);
                bool boolResult = Convert.ToBoolean(result.Content.ReadAsStringAsync().Result);
                return boolResult;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
 } 
