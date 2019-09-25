using EnixerPos.Api.ViewModels.Helpers;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Helpers;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static EnixerPos.Domain.Helpers.Status;

namespace EnixerPos.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShiftRepository _shiftRepository;
        private readonly string SentEmailValue = StaticValue.BaseUrl + ":20000/";

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
            string otp = Generator.GenerateRandomString(20);
            _storeRepository.UpdateOtp(email, otp);
            string value = SentEmailValue + $"SetPassword?otp={otp}&&email={email}";
            SendEmail(email, value);
        }

        private void SendEmail(string email, string value)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtpm.csloxinfo.com");

            mail.From = new MailAddress("student@enixer.net");
            mail.To.Add(email);
            mail.Subject = @"Set Password";
            mail.Body = value;
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
            string email = command.Email.ToLower();
            string storeName = command.StoreName;
            string eWalletAccNo = command.EWalletAccNo;
            string otp = Generator.GenerateRandomString(20);

            bool isCreateStore = _storeRepository.CreateStore(email, storeName, eWalletAccNo, otp);
            if (!isCreateStore)
            {
                return false;
                
            }

            int storeId = _storeRepository.GetStoreIdByEmail(email);
            if (storeId == default)
            {
                return false;
            }

            bool isCreateToken = _tokenRepository.CreateTokenByStoreId(storeId);

            string value = SentEmailValue + $"SetPassword?otp={otp}&&email={email}";
            SendEmail(email, value);
            return true;
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

        public bool ChechEmail(string email)
        {
            int storeId = _storeRepository.GetStoreIdByEmail(email);
            if (storeId != default)
            {
                return true;
            }
            return false;
        }

        public bool CheckStore(string storeName)
        {
            int storeId = _storeRepository.GetStoreIdByStoreName(storeName);
            if (storeId != default)
            {
                return true;
            }
            return false;
        }

        public bool SetPassword(SetPasswordDtoCommand command)
        {
            string email = command.Email.ToLower();
            string password = command.Password;
            string otp = command.OTP;
            string salt = Generator.GenerateRandomString(10);
            string hashPass = Generator.HashPassword(password, salt);

            return _storeRepository.AddPassword(email, hashPass, salt, otp);
        }
    }
}
