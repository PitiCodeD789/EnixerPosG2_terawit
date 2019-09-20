using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;
        public TokenRepository(DataContext context)
        {
            _context = context;
        }
        public TokenEntity GetTokenByEmailAndImei(string email, string imei)
        {
            throw new NotImplementedException();
        }

        public TokenEntity UpdateRefreshToken(string email, string imei)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
