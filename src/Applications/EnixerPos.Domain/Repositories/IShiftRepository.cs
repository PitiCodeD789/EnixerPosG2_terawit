using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IShiftRepository
    {
       
        bool Update(ShiftEntity shiftEntity);
        List<ShiftEntity> GetLast30DayShift();        
        int CreateShift(ShiftEntity shiftEntity);
        ShiftEntity GetShiftDetailByShiftId(string storeEmail, string posIMEI, int posUserId, int shiftId);
    }
}
