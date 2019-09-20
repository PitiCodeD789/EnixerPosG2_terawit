using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IStoreRepository
    {
        StoreEntity GetStoreByEmail(string email);
    }
}
