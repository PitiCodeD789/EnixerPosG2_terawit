using EnixerPos.Api.ViewModels.Product;
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

namespace EnixerPos.Service.Services
{
    public class ProductService : BaseService,IProductService
    {
        private string serviceUrl = Helper.BaseUrl + "Product/";
        private string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
        public async Task<ResultViewModel> CreateItem(ItemModel item)
        {
            try
            {
                string url = serviceUrl + "AddItem";

                string json = JsonConvert.SerializeObject(item);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.PostAsync(url, stringContent).Result;
                var a = response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var result = new ResultViewModel();
                result = JsonConvert.DeserializeObject<ResultViewModel>(responseBody);
                return result;
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
            HttpResponseMessage response = client.GetAsync("http://192.168.1.33:30000/api/product/getcategories").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CategoriesViewModel>(responseBody);
            var cartegories = result.Categories;
            return result;
        }
    }
}
