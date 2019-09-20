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
        void UpdatePassword(string email, string newPass);
    }
}
