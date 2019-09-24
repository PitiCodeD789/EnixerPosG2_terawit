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
        Task<ResultServiceModel<LoginViewModel>> Login(string email, string password);
        Task<ResultServiceModel<LoginByPinViewModel>> LoginByPin(string pin, string refreshToken, string email);
        Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshMerchant(string email, string refreshToken);
        Task<ResultServiceModel<GetTokenByRefreshViewModel>> GetTokenByRefreshUser(string refreshToken, string user);
        Task<ResultServiceModel<DummyModel>> Logout();
    }
}
