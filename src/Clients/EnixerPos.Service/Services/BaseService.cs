using EnixerPos.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EnixerPos.Service.Services
{
    public class BaseService
    {
        private readonly TokenService _tokenService = new TokenService();
        protected async Task<ResultServiceModel<T>> Post<T>(string url, object model) where T : class
        {
        StartMethod:
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

                string token = "";

                try
                {
                    token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                    //token = await SecureStorage.GetAsync("Token");
                }
                catch (Exception e)
                {
                    throw e;
                }
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpContent content = GetHttpContent(model);

                var result =  client.PostAsync(url, content).Result;

                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        var json_result = result.Content.ReadAsStringAsync().Result;

                        T obj = GetModelFormResult<T>(json_result);

                        resultService.IsError = System.Net.HttpStatusCode.OK;

                        resultService.Model = obj;

                        return resultService;
                    }
                    catch (Exception e)
                    {
                        return new ResultServiceModel<T>();
                    }
                   
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    try
                    {
                        var tokenData = await _tokenService.GetAccessToken();
                        if (tokenData != null)
                        {
                            if (tokenData.Model == null || tokenData.Model.Token == null)
                            {
                                resultService.IsError = result.StatusCode;
                            }
                            await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                            goto StartMethod;
                        }
                        else
                        {
                            resultService.IsError = result.StatusCode;
                        }
                    }
                    catch (Exception e)
                    {
                        resultService.IsError = result.StatusCode;
                    }
                }
                else
                {
                    client.Dispose();
                    resultService.IsError = result.StatusCode;
                }
            }
            catch (Exception e)
            {
                resultService.IsError = System.Net.HttpStatusCode.InternalServerError;
            }
            return resultService;
        }

        protected async Task<ResultServiceModel<T>> Get<T>(string url) where T : class
        {
        StartMethod:
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

                string token = "";
                token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";

                try
                {
                    //token = await SecureStorage.GetAsync("Token");
                    token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpbWVpIjoiMTIzNDU2Nzg5IiwibmJmIjoxNTY5MDQxMDA2LCJleHAiOjE1NjkxNTEzMDYsImlzcyI6IkVuaXhlclBvc0cyIiwiYXVkIjoiZUBlIiwidXNlciI6Ik5hdCJ9.2NVziPg0aE3eXlSLL9MyGp453CaW2UYMLMV5GMqPDJs";
                }
                catch (Exception e)
                {
                    throw e;
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var result = client.GetAsync(url).Result;

                if (result.IsSuccessStatusCode)
                {
                    var json_result = result.Content.ReadAsStringAsync().Result;

                    T obj = GetModelFormResult<T>(json_result);

                    resultService.IsError = result.StatusCode;

                    resultService.Model = obj;

                    return resultService;
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    try
                    {
                        var tokenData = await _tokenService.GetAccessToken();
                        if (tokenData != null)
                        {
                            if (tokenData.Model == null || tokenData.Model.Token == null)
                            {
                                resultService.IsError = result.StatusCode;
                            }
                            await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                            goto StartMethod;
                        }
                        else
                        {
                            resultService.IsError = result.StatusCode;
                        }
                    }
                    catch (Exception e)
                    {
                        resultService.IsError = result.StatusCode;
                    }
                }
                else
                {
                    client.Dispose();
                    resultService.IsError = result.StatusCode;
                }
            }
            catch (Exception e)
            {
                resultService.IsError = System.Net.HttpStatusCode.InternalServerError;
            }
            return resultService;
        }

        protected HttpContent GetHttpContent(object model)
        {
            string json = JsonConvert.SerializeObject(model);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected T GetModelFormResult<T>(string json_result) where T : class
        {

            return JsonConvert.DeserializeObject<T>(json_result);
        }
    }
}
