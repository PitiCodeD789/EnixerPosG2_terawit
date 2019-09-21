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
        public StoreEntity GetStoreByEmail(string email)
        {
            try
            {
                return _context.Store.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
            }
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdatePassword(string email, string newPass)
        {
            try
            {
                var store = _context.Store.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
                store.Password = newPass;

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
