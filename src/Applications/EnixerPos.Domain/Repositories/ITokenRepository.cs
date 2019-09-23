using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface ITokenRepository
    {
        TokenEntity GetTokenByEmail(int storeId);
        bool UpdateRefreshToken(int storeId, string refreshToken);
        bool UpdateUserId(int storeId, int userId);
        bool DeleteUserAndToken(int storeId);
        int CountUser(int id);
    }
}
