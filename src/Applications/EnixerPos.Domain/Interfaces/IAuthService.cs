using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Auth;

namespace EnixerPos.Domain.Interfaces
{
    public interface IAuthService
    {
        LoginDto LoginMerchant(string email, string password, string imei);
        string GetRefreshToken(string email, string imei);
        LoginByPinDto LoginUser(string email, string imei, string pin);
        bool CheckRefresh(string email, string imei, string refreshToken, string user);
        bool CheckRefresh(string email, string imei, string refreshToken);
        bool RegisterStore(RegisterStoreDtoCommand command);
        bool RegisterUserInStore(RegisterUserInStoreDtoCommand command);
        void ForgotPassword(string email);
        bool Logout(string email, string imei);
    }
}
