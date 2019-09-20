using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class ShiftRepository : IShiftRepository
    {
        private readonly DataContext _context;
        public ShiftRepository(DataContext context)
        {
            _context = context;
        }

        public int CreateShift(ShiftEntity shiftEntity)
        {
            throw new NotImplementedException();
        }

        public ShiftEntity Get(int shiftId)
        {
            throw new NotImplementedException();
        }

        public List<ShiftEntity> GetLast30DayShift()
        {
            throw new NotImplementedException();
        }

        public ShiftEntity GetShiftById(int shiftId)
        {
            throw new NotImplementedException();
        }

        public ShiftEntity GetShiftDetailByShiftId(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {
            throw new NotImplementedException();
        }

        public bool Update(ShiftEntity shiftEntity)
        {
            throw new NotImplementedException();
        }
    }
}
