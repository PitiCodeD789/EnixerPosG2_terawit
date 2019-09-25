using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _context;
        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddPassword(string email, string hashPass, string salt, string otp)
        {
            try
            {
                var store = _context.Store.Where(x => x.Email.ToLower() == email.ToLower() && x.OTP == otp).SingleOrDefault();
                if (store == default)
                {
                    return false;
                }

                store.Password = hashPass;
                store.Salt = salt;
                store.OTP = null;

                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public bool CreateStore(string email, string storeName, string eWalletAccNo, string otp)
        {
            try
            {
                StoreEntity storeEntity = new StoreEntity()
                {
                    Email = email,
                    StoreName = storeName,
                    EWalletAccountNo = eWalletAccNo,
                    OTP = otp
                };

                _context.Store.Add(storeEntity);

                _context.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return false;
            }
        }

        public StoreEntity GetStoreByEmail(string email)
        {
            try
            {
                if (email == null)
                    return null;
                return _context.Store.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GetStoreIdByEmail(string audience)
        {
            try
            {
                return _context.Store.Where(x => x.Email.ToLower() == audience.ToLower()).FirstOrDefault().Id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return default;
            }
        }

        public int GetStoreIdByStoreName(string storeName)
        {
            try
            {
                return _context.Store.Where(x => x.StoreName == storeName).Select(x => x.Id).SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error : " + e.Message);
                return default;
            }
        }

        public void UpdateOtp(string email, string otp)
        {
            try
            {
                var store = _context.Store.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
                store.OTP = otp;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
