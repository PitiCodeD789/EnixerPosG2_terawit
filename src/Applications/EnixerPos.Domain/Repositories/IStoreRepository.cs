using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Repositories
{
    public interface IStoreRepository
    {
        int GetStoreIdByEmail(string audience);
    }
}
