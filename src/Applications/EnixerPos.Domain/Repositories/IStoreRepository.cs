using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IStoreRepository
    {
        int GetStoreIdByEmail(string audience);
        StoreEntity GetStoreByEmail(string email);
        void UpdateOtp(string email, string newPass);
        bool CreateStore(string email, string storeName, string eWalletAccNo, string otp);
        int GetStoreIdByStoreName(string storeName);
        bool AddPassword(string email, string hashPass, string salt, string otp);
    }
}
