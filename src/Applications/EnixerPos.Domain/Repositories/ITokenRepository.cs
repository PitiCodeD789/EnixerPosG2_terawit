using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface ITokenRepository
    {
        TokenEntity GetTokenByEmail(string email);
        bool UpdateRefreshToken(string email, string refreshToken);
        bool UpdateUserId(string email, int userId);
        bool DeleteUserAndToken(string email);
        int CountUser(int id);
    }
}
