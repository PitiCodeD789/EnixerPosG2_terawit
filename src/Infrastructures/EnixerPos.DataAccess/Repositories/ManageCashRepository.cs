using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class ManageCashRepository : IManageCashRepository
    {
        private readonly DataContext _context;

        public ManageCashRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddManageCash(ManageCashEntity manageCash)
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
