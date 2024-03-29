﻿using EnixerPos.Api.ViewModels.Auth;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private string serviceUrl = Helper.BaseUrl + "auth/";

        public async Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshMerchant(string email, string refreshToken)
        {
            GetTokenByRefreshMerchantCommand model = new GetTokenByRefreshMerchantCommand()
            {
                Email = email.ToLower(),
                RefreshToken = refreshToken
            };

            string url = serviceUrl + "tokenmerchant";
            return await Post<GetTokenByRefreshViewModel>(url, model);
        }

        public async Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshUser(string refreshToken, string user)
        {
            GetTokenByRefreshUserCommand model = new GetTokenByRefreshUserCommand()
            {
                RefreshToken = refreshToken,
                User = user
            };
            string url = serviceUrl + "tokenuser";
            return await Post<GetTokenByRefreshViewModel>(url, model);
        }

        public async Task<ResultServiceModel<LoginViewModel>> Login(string email, string password)
        {
            LoginCommand model = new LoginCommand()
            {
                Email = email.ToLower(),
                Password = password
            };

            string url = serviceUrl + "login";
            return await Post<LoginViewModel>(url, model);
        }

        public async Task<ResultServiceModel<LoginByPinViewModel>> LoginByPin(string pin, string refreshToken, string email)
        {
            LoginByPinCommand model = new LoginByPinCommand()
            {
                Pin = pin,
                RefreshToken = refreshToken,
                Email = email
            };

            string url = serviceUrl + "loginbypin";
            return await Post<LoginByPinViewModel>(url, model);
        }

        public async Task<ResultServiceModel<DummyModel>> Logout(string email)
        {
            LogoutCommand model = new LogoutCommand()
            {
                Email = email
            };

            string url = serviceUrl + "logout";
            return await Post<DummyModel>(url, model);
        }
    }
}
