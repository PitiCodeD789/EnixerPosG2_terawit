using EnixerPos.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Service.Interfaces
{
    public interface IAuthService
    {
        LoginViewModel Login(string email, string password, string imei);
        LoginByPinViewModel LoginByPin(string pin);
        GetTokenByRefreshViewModel GetTokenByRefreshMerchant(string email, string password, string refreshToken);
        GetTokenByRefreshViewModel GetTokenByRefreshUser(string refreshToken, string user);
        bool Logout();
    }
}
