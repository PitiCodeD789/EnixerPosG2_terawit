using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class ManageCashRepository : IManageCashRepository
    {
        private readonly DataContext _context;
        public ManageCashRepository(DataContext dataContext)
        {
            _context = dataContext;

        }
        public bool AddManageCash(ManageCashEntity manageCash)
        {
            try
            {
                _context.ManageCash.Add(manageCash);
                _context.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
         
        }

        public List<ManageCashEntity> GetManageCashByShiftId(int shiftId, string storeEmail)
        {
            return _context.ManageCash.Where(x => x.ShiftId == shiftId).ToList();
        }
    }
}
