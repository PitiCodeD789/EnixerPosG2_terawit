using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly DataContext _context;
        public ShiftRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public int CreateShift(ShiftEntity shiftEntity)
        {
            try
            {
                _context.Shift.Add(shiftEntity);
                _context.SaveChanges();
                return shiftEntity.Id;
               
            }
            catch
            {
                return 0;
            }           
           
        }

      

        public List<ShiftEntity> GetLast30DayShift(string storeEmail, string posUserId)
        {
            return _context.Shift.Where(x => x.StoreEmail == storeEmail  && x.UpdateDateTime > DateTime.UtcNow.AddDays(-30)).ToList();

        }

        public ShiftEntity GetShift(string storeEmail, int posUserId)
        {
            return _context.Shift.LastOrDefault(x => x.StoreEmail == storeEmail && x.PosUserId == posUserId);
        }

        public ShiftEntity GetShiftDetailByShiftId(string storeEmail, int posUserId, int shiftId)
        {
            return _context.Shift.LastOrDefault(x => x.ShiftId == shiftId);
        }

        public int GetShiftId(string storeEmail, int posUserId)
        {
            return _context.Shift.LastOrDefault(x => x.StoreEmail == storeEmail && x.PosUserId == posUserId && x.Available == true).ShiftId;
        }

        public bool Update(ShiftEntity shiftEntity)
        {

            try
            {
                ShiftEntity entity = _context.Shift.FirstOrDefault(x => x.Id == shiftEntity.Id);
                entity = shiftEntity;
                _context.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
           
        }
    }
}
