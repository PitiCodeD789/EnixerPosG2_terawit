using EnixerPos.Domain.DtoModels.Auth;
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

        public AuthService(IStoreRepository storeRepository, 
                           ITokenRepository tokenRepository, 
                           IUserRepository userRepository)
        {
            _storeRepository = storeRepository;
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public bool CheckRefresh(string email, string refreshToken, string user)
        {
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(email);
            UserEntity userEntity = _userRepository.GetUserByUserName(user);
            if (tokenEntity.RefreshToken != refreshToken || tokenEntity.UserId != userEntity.Id)
            {
                return false;
            }
            return true;
        }

        public bool CheckRefresh(string email, string refreshToken)
        {
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(email);
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
            bool tokenEntity = _tokenRepository.UpdateRefreshToken(email, refreshToken);
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

        public LoginByPinDto LoginUser(string email, string pin)
        {
            string hashPin = Generator.HashPassword(pin, null);
            UserEntity userEntity = _userRepository.GetUserByEmialAndPin(email, hashPin);
            if(userEntity == null)
            {
                return null;
            }

            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(email);
            if(tokenEntity == null)
            {
                return null;
            }
            //if(tokenEntity.UserId != 0)
            //{
            //    return null;
            //}

            //int countUser = _tokenRepository.CountUser(userEntity.Id);
            //if(countUser > 0)
            //{
            //    return null;
            //}

            int userId = userEntity.Id;
            bool isUpdate = _tokenRepository.UpdateUserId(email, userId);
            if (!isUpdate)
            {
                return null;
            }



            LoginByPinDto loginByPinDto = new LoginByPinDto()
            {
                User = userEntity.NameUser,
                UserId = userId
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
            bool isDelete = _tokenRepository.DeleteUserAndToken(email);
            if (!isDelete)
            {
                return false;
            }
            return true;
        }
    }
}
