using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface ITokenRepository
    {
        TokenEntity GetTokenByEmailAndImei(string email, string imei);
        TokenEntity UpdateRefreshToken(string email, string imei);
        bool UpdateUserId(int userId);
    }
}
