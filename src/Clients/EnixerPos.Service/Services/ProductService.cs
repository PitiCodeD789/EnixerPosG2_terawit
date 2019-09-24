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
        private string accessToken = SecureStorage.GetAsync("Token").Result;
        public async Task<ResultViewModel> CreateItem(ItemModel item)
        {
            try
            {
                string url = serviceUrl + "AddItem";

                //string json = JsonConvert.SerializeObject(item);
                //var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage response = client.PostAsync(url, stringContent).Result;
                //var a = response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = new ResultViewModel();
                //result = JsonConvert.DeserializeObject<ResultViewModel>(responseBody);
                //return result;

                var result = await Post<ResultViewModel>(url, item);
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new ResultViewModel() { IsError = true };
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<CategoriesViewModel> GetAllCategories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = client.GetAsync(serviceUrl+"getcategories").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CategoriesViewModel>(responseBody);
            var cartegories = result.Categories;
            return result;
        }
        public List<CategoryModel> GetCategories()
        {
            try
            {
                ////var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                //var client = new HttpClient();
                //string accessToken = SecureStorage.GetAsync("Token").Result;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage response = client.GetAsync(serviceUrl + "getcategories").Result;
                //response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = JsonConvert.DeserializeObject<CategoriesViewModel>(responseBody);
                //var cartegories = result.Categories;
                //return cartegories;

                var result = Get<CategoriesViewModel>(serviceUrl + "getcategories").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model.Categories;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public ItemsViewModel GetItems()
        {
            try
            {
                ////var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                //var client = new HttpClient();
                //string accessToken = SecureStorage.GetAsync("Token").Result;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage response = client.GetAsync(serviceUrl + "getitems").Result;
                //response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = JsonConvert.DeserializeObject<ItemsViewModel>(responseBody);
                //return result;

                var result = Get<ItemsViewModel>(serviceUrl + "getitems").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new ItemsViewModel {IsError = true};
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ReceiptViewModel AddPayment(PaymentCommand payment)
        {
            try
            {
                ////var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                //var client = new HttpClient();
                //string accessToken = SecureStorage.GetAsync("Token").Result;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpContent content = GetHttpContent(payment);
                //HttpResponseMessage response = client.PostAsync(Helper.BaseUrl + "sale/" + "payment", content).Result;
                //response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = JsonConvert.DeserializeObject<ReceiptViewModel>(responseBody);
                //return result;

                var result =  Post<ReceiptViewModel>(Helper.BaseUrl + "sale/payment", payment).Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckPayment()
        {
            try
            {
                ////var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                //var client = new HttpClient();
                //string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage response = client.GetAsync(serviceUrl + "getitems").Result;
                //response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = JsonConvert.DeserializeObject<bool>(responseBody);
                //return result;
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
                ////var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                //var client = new HttpClient();
                //string accessToken = SecureStorage.GetAsync("Token").Result;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                //HttpResponseMessage response = client.GetAsync(serviceUrl + "getdiscounts").Result;
                //response.EnsureSuccessStatusCode();
                //string responseBody = response.Content.ReadAsStringAsync().Result;
                //var result = JsonConvert.DeserializeObject<DiscountsViewModel>(responseBody);
                //return result;

                var result = Get<DiscountsViewModel>(serviceUrl+"getdiscounts").Result;
                if (result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }

                return new DiscountsViewModel() { IsError = true };
            }
            catch (Exception e)
            {
                return null;
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
                return null;
            }
            catch (Exception)
            {

                return null;
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
    }
 } 
