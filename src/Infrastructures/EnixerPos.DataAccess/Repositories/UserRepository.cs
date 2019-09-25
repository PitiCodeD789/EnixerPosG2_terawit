using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUserInStore(int storeId, string pin, string nameUser)
        {
            try
            {
                UserEntity userEntity = new UserEntity()
                {
                    StoreId = storeId,
                    Pin = pin,
                    NameUser = nameUser
                };

                _context.User.Add(userEntity);

                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public bool DeleteUserByEmialAndUser(int storeId, string user)
        {
            try
            {
                var userData = _context.User.Where(x => x.StoreId == storeId && x.NameUser == user).Single();

                _context.User.Remove(userData);

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public bool EditUserInStore(int storeId, string pin, string nameUser, string oldUser)
        {
            try
            {
                var userData = _context.User.Where(x => x.StoreId == storeId && x.NameUser == oldUser).Single();

                userData.NameUser = nameUser;

                userData.Pin = pin;

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public List<UserEntity> GetAllUserInStore(int storeId)
        {
            try
            {
                return _context.User.Where(x => x.StoreId == storeId).ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public UserEntity GetUserByEmialAndPin(int storeId, string hashPin)
        {
            try
            {
                return _context.User.Where(x => x.StoreId == storeId).Where(x => x.Pin == hashPin).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public UserEntity GetUserByEmialAndUser(int storeId, string user)
        {
            try
            {
                return _context.User.Where(x => x.StoreId == storeId).Where(x => x.NameUser == user).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public UserEntity GetUserByUserName(string user)
        {
            try
            {
                return _context.User.Where(x => x.NameUser == user).FirstOrDefault();
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
