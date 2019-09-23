using EnixerPos.Api.ViewModels.Auth;
using EnixerPos.Service.Helpers;
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
    public class TokenService
    {
        public async Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetAccessStoreToken()
        {
            ResultServiceModel<GetTokenByRefreshViewModel> resultService = new ResultServiceModel<GetTokenByRefreshViewModel>();
            try
            {
                string url = Helper.BaseUrl + "auth/tokenmerchant";

                HttpClient client = new HttpClient();

                string refreshToken = "";

                string email = "";

                try
                {
                    refreshToken = SecureStorage.GetAsync("RefreshToken").Result;
                    email = SecureStorage.GetAsync("Email").Result;
                }
                catch (Exception e)
                {
                    return null;
                }

                GetTokenByRefreshMerchantCommand model = new GetTokenByRefreshMerchantCommand
                {
                    Email = email,

                    RefreshToken = refreshToken,
                };

                HttpContent content = GetHttpContent(model);

                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    GetTokenByRefreshViewModel obj = GetModelFormResult<GetTokenByRefreshViewModel>(json_result);

                    resultService.IsError = result.StatusCode;

                    resultService.Model = obj;

                    return resultService;
                }
                else
                {
                    client.Dispose();
                    CloseApp();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return resultService;
        }

        public async Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetAccessToken()
        {
            ResultServiceModel<GetTokenByRefreshViewModel> resultService = new ResultServiceModel<GetTokenByRefreshViewModel>();
            try
            {
                string url = Helper.BaseUrl + "auth/tokenuser";

                HttpClient client = new HttpClient();

                string refreshToken = "";

                string user = "";

                string token = "";

                try
                {
                    var stoerToken = await GetAccessStoreToken();
                    if(stoerToken == null)
                    {
                        return null;
                    }
                    refreshToken = stoerToken.Model.RefreshToken;

                    token = stoerToken.Model.Token;

                    user = SecureStorage.GetAsync("User").Result;
                }
                catch (Exception e)
                {
                    return null;
                }

                GetTokenByRefreshUserCommand model = new GetTokenByRefreshUserCommand
                {
                    RefreshToken = refreshToken,

                    User = user
                };

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpContent content = GetHttpContent(model);

                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    GetTokenByRefreshViewModel obj = GetModelFormResult<GetTokenByRefreshViewModel>(json_result);

                    resultService.IsError = result.StatusCode;

                    resultService.Model = obj;

                    return resultService;
                }
                else
                {
                    client.Dispose();
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
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

        private void CloseApp()
        {
            SecureStorage.RemoveAll();
            Environment.Exit(0);
        }
    }
}
