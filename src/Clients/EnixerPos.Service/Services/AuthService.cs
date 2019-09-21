using EnixerPos.Api.ViewModels.Auth;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Service.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private string serviceUrl = Helper.BaseUrl + "auth/";
        public GetTokenByRefreshViewModel GetTokenByRefreshMerchant(string email, string password, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public GetTokenByRefreshViewModel GetTokenByRefreshUser(string refreshToken, string user)
        {
            throw new NotImplementedException();
        }

        public LoginViewModel Login(string email, string password, string imei)
        {
            throw new NotImplementedException();
        }

        public LoginByPinViewModel LoginByPin(string pin)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
