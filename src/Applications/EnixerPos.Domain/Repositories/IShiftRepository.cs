using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IShiftRepository
    {
       
        bool Update(ShiftEntity shiftEntity);
        List<ShiftEntity> GetLast30DayShift(string storeEmail, string posUserId);        
        int CreateShift(ShiftEntity shiftEntity);
        ShiftEntity GetShiftDetailByShiftId(string storeEmail, int posUserId, int shiftId);
        int GetShiftId(string storeEmail, int posUserId);
        ShiftEntity GetShift(string storeEmail, int posUserId);
    }
}
