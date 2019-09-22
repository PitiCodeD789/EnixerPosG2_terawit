using EnixerPos.Api.ViewModels.Auth;
using EnixerPos.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Interfaces
{
    public interface IAuthService
    {
        Task<ResultServiceModel<LoginViewModel>> Login(string email, string password, string imei);
        Task<ResultServiceModel<LoginByPinViewModel>> LoginByPin(string pin);
        Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshMerchant(string email, string imei, string refreshToken);
        Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshUser(string refreshToken, string user);
        Task<ResultServiceModel<DummyModel>> Logout();
    }
}
