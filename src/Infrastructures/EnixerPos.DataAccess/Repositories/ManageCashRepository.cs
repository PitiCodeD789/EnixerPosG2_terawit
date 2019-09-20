using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ManageCashEntity> GetManageCashByShiftId(int shiftId, string storeEmail, string posIMEI)
        {
            List<ManageCashEntity> manageCashes = _context.ManageCash.Where(x => x.ShiftId == shiftId).Where(x => x.StoreEmail == storeEmail.ToLower()).Where(x => x.PosImei == posIMEI).ToList();
            return manageCashes;
        }
    }
}
