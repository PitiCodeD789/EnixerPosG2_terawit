﻿using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Helpers;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace EnixerPos.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShiftRepository _shiftRepository;

        public AuthService(IStoreRepository storeRepository, 
                           ITokenRepository tokenRepository, 
                           IUserRepository userRepository,
                           IShiftRepository shiftRepository)
        {
            _storeRepository = storeRepository;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _shiftRepository = shiftRepository;
        }

        public bool CheckRefresh(string email, string refreshToken, string user)
        {
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(storeEntity.Id);
            UserEntity userEntity = _userRepository.GetUserByUserName(user);
            if (tokenEntity.RefreshToken != refreshToken || tokenEntity.UserId != userEntity.Id)
            {
                return false;
            }
            return true;
        }

        public bool CheckRefresh(string email, string refreshToken)
        {
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(storeEntity.Id);
            if (tokenEntity.RefreshToken != refreshToken)
            {
                return false;
            }
            return true;
        }

        public void ForgotPassword(string email)
        {
            string newPass = Generator.GenerateRandomString(10);
            _storeRepository.UpdatePassword(email, newPass);
            SendEmail(email, newPass);
        }

        private void SendEmail(string email, string value)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtpm.csloxinfo.com");

            mail.From = new MailAddress("student@enixer.net");
            mail.To.Add(email);
            mail.Subject = @"New Password";
            mail.Body = $"New Password : {value}";
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("student@enixer.net", "Gg123456789");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            SmtpServer.Dispose();
        }

        public string GetRefreshToken(string email)
        {
            string refreshToken = Generator.GenerateRandomString(10);
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            bool tokenEntity = _tokenRepository.UpdateRefreshToken(storeEntity.Id, refreshToken);
            return refreshToken;
        }

        public LoginDto LoginMerchant(string email, string password)
        {
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            if (storeEntity == null)
            {
                return null;
            }
            string hashPassword = Generator.HashPassword(password, storeEntity.Salt);
            if (hashPassword != storeEntity.Password)
            {
                return null;
            }

            LoginDto loginDto = new LoginDto()
            {
                StoreName = storeEntity.StoreName,
            };
            return loginDto;
        }

        public LoginByPinDto LoginUser(string email, string pin, string refreshToken)
        {
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            UserEntity userEntity = _userRepository.GetUserByEmialAndPin(storeEntity.Id, pin);
            if(userEntity == null)
            {
                return null;
            }

            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(storeEntity.Id);
            if(tokenEntity == null)
            {
                return null;
            }
            if(tokenEntity.RefreshToken != refreshToken)
            {
                return null;
            }

            int userId = userEntity.Id;
            bool isUpdate = _tokenRepository.UpdateUserId(storeEntity.Id, userId);
            if (!isUpdate)
            {
                return null;
            }

            ShiftEntity shiftEntity = _shiftRepository.GetShift(email, userId);
            int shiftId = 0;
            if(shiftEntity != null)
            {
                if (shiftEntity.Available == true)
                {
                    shiftId = shiftEntity.Id;
                }
            }
                
            LoginByPinDto loginByPinDto = new LoginByPinDto()
            {
                User = userEntity.NameUser,
                UserId = userId,
                ShiftId = shiftId
            };

            return loginByPinDto;
        }

        public bool RegisterStore(RegisterStoreDtoCommand command)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUserInStore(RegisterUserInStoreDtoCommand command)
        {
            throw new NotImplementedException();
        }

        public bool Logout(string email)
        {
            StoreEntity storeEntity = _storeRepository.GetStoreByEmail(email);
            bool isDelete = _tokenRepository.DeleteUserAndToken(storeEntity.Id);
            if (!isDelete)
            {
                return false;
            }
            return true;
        }
    }
}
