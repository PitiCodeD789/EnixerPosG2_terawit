using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public UserEntity GetUserByEmialAndPin(string email, string hashPin)
        {
            try
            {
                return _context.User.Where(x => x.Email.ToLower() == email.ToLower()).Where(x => x.Pin == hashPin).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public UserEntity GetUserByUserName(int userId)
        {
            try
            {
                return _context.User.Where(x => x.Id == userId).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
