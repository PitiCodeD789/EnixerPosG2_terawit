﻿using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _context;
        public TokenRepository(DataContext context)
        {
            _context = context;
        }
        public TokenEntity GetTokenByEmailAndImei(string email, string imei)
        {
            try
            {
                return _context.Token.Where(x => x.Email == email && x.Imei == imei).Single();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

        public bool UpdateRefreshToken(string email, string imei, string refreshToken)
        {
            try
            {
                var token = _context.Token.Where(x => x.Email == email && x.Imei == imei).Single();
                token.RefreshToken = refreshToken;

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public bool UpdateUserId(string email, string imei, int userId)
        {
            try
            {
                var token = _context.Token.Where(x => x.Email == email && x.Imei == imei).Single();
                token.UserId = userId;

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }
    }
}