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

        public int CountUser(int id)
        {
            try
            {
                return _context.Token.Where(x => x.UserId == id).Count();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return 1;
            }
        }

        public bool DeleteUserAndToken(string email)
        {
            try
            {
                var token = _context.Token.Where(x => x.Email == email).Single();
                token.UserId = 0;
                token.RefreshToken = null;

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public TokenEntity GetTokenByEmail(string email)
        {
            try
            {
                return _context.Token.Where(x => x.Email == email).Single();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return null;
            }
        }

        public bool UpdateRefreshToken(string email, string refreshToken)
        {
            try
            {
                var token = _context.Token.Where(x => x.Email == email).Single();
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

        public bool UpdateUserId(string email, int userId)
        {
            try
            {
                var token = _context.Token.Where(x => x.Email == email).Single();
                token.UserId = userId;
                token.UpdateDateTime = DateTime.UtcNow;

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
