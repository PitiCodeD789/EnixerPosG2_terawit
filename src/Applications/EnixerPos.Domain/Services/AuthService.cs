using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Helpers;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public AuthService(IStoreRepository storeRepository, 
                           IDeviceRepository deviceRepository, 
                           ITokenRepository tokenRepository, 
                           IUserRepository userRepository)
        {
            _storeRepository = storeRepository;
            _deviceRepository = deviceRepository;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public bool CheckRefresh(string email, string imei, string refreshToken, int userId)
        {
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmailAndImei(email, imei);
            UserEntity userEntity = _userRepository.GetUserByUserName(userId);
            if (tokenEntity.RefreshToken != refreshToken || tokenEntity.UserId != userId)
            {
                return false;
            }
            return true;
        }

        public bool CheckRefresh(string email, string imei, string refreshToken)
        {
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmailAndImei(email, imei);
            if (tokenEntity.RefreshToken != refreshToken)
            {
                return false;
            }
            return true;
        }

        public string GetRefreshToken(string email, string imei)
        {
            string refreshToken = Generator.GenerateRandomString(10);
            TokenEntity tokenEntity = _tokenRepository.UpdateRefreshToken(email, imei);
            return refreshToken;
        }

        public LoginDto LoginMerchant(string email, string password, string imei)
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

            DeviceEntity deviceEntity = _deviceRepository.GetDeviceByImei(imei);
            if(deviceEntity == null)
            {
                return null;
            }
            if (storeEntity.Id != deviceEntity.StoreId)
            {
                return null;
            }

            LoginDto loginDto = new LoginDto()
            {
                StoreName = storeEntity.StoreName,
                PosName = deviceEntity.PosName,
            };
            return loginDto;
        }

        public LoginByPinDto LoginUser(string email, string imei, string pin)
        {
            string hashPin = Generator.HashPassword(pin, "");
            UserEntity userEntity = _userRepository.GetUserByEmialAndPin(email, hashPin);
            if(userEntity == null)
            {
                return null;
            }

            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmailAndImei(email, imei);
            if(tokenEntity == null)
            {
                return null;
            }
            if(tokenEntity.UserId != 0)
            {
                return null;
            }

            int userId = userEntity.Id;
            bool isUpdate = _tokenRepository.UpdateUserId(userId);
            if (!isUpdate)
            {
                return null;
            }



            LoginByPinDto loginByPinDto = new LoginByPinDto()
            {
                User = userEntity.User,
                UserId = userId
            };

            return loginByPinDto;
        }

    }
}
