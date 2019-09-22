using EnixerPos.Api.ViewModels.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace EnixerPos.Service.Services
{
    public class ProductService : BaseService
    {
        public List<CategoryModel> GetCategories()
        {
            try
            {
                //var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                var client = new HttpClient();
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.GetAsync("http://192.168.1.33:30000/api/product/getcategories").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CategoriesViewModel>(responseBody);
                var cartegories = result.Categories;
                return cartegories;
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
                //var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                var client = new HttpClient();
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.GetAsync("http://192.168.1.33:30000/api/product/getitems").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ItemsViewModel>(responseBody);
                return result;
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
                //var result = Get<CategoriesViewModel>("http://192.168.1.33:30000/api/product/getcategories").Result;
                var client = new HttpClient();
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = client.GetAsync("http://192.168.1.33:30000/api/product/getitems").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<bool>(responseBody);
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
