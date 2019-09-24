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
        public async Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetAccessToken()
        {
            ResultServiceModel<GetTokenByRefreshViewModel> resultService = new ResultServiceModel<GetTokenByRefreshViewModel>();
            try
            {
                string url = Helper.BaseUrl + "auth/tokenuser";

                HttpClient client = new HttpClient();

                client.Timeout = TimeSpan.FromSeconds(20);

                string refreshToken = "";

                string user = "";

                string email = "";

                try
                {
                    refreshToken = await SecureStorage.GetAsync("RefreshToken");

                    email = await SecureStorage.GetAsync("Email");

                    user = await SecureStorage.GetAsync("User");
                }
                catch (Exception e)
                {
                    CloseApp();
                    return null;
                }

                GetTokenByRefreshUserCommand model = new GetTokenByRefreshUserCommand
                {
                    RefreshToken = refreshToken,

                    Email = email,

                    User = user
                };

                HttpContent content = GetHttpContent(model);

                var result = client.PostAsync(url, content).Result;

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
                    return null;
                }
            }
            catch (Exception e)
            {
                //return null;
                CloseApp();
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
