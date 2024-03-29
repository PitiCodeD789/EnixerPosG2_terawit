﻿using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Auth;

namespace EnixerPos.Domain.Interfaces
{
    public interface IAuthService
    {
        LoginDto LoginMerchant(string email, string password);
        string GetRefreshToken(string email);
        LoginByPinDto LoginUser(string email, string pin, string refreshToken);
        bool CheckRefresh(string email, string refreshToken, string user);
        bool CheckRefresh(string email, string refreshToken);
        bool RegisterStore(RegisterStoreDtoCommand command);
        bool RegisterUserInStore(RegisterUserInStoreDtoCommand command);
        void ForgotPassword(string email);
        bool Logout(string email);
        bool ChechEmail(string email);
        bool CheckStore(string storeName);
        bool SetPassword(SetPasswordDtoCommand command);
        bool CheckPin(CheckPinDtoCommand checkPinDto);
        bool ChechNameUser(CheckNameUserDtoCommand checkNameUser);
        List<StaffDto> GetUserInStore(string email);
        bool DeleteNameUser(CheckNameUserDtoCommand checkNameUser);
        bool EditUserInStore(EditUserInStoreDtoCommand command);
    }
}
