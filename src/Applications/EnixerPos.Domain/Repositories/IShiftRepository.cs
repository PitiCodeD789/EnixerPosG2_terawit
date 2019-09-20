using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IShiftRepository
    {
        ShiftEntity GetShiftById(int shiftId);
        bool Update(ShiftEntity shiftEntity);
        List<ShiftEntity> GetLast30DayShift();
        ShiftEntity Get(int shiftId);
        int CreateShift(ShiftEntity shiftEntity);
    }
}
