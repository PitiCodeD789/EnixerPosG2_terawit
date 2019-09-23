using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Shifts;

namespace EnixerPos.Domain.Interfaces
{
    public interface IShiftService
    {
        List<ShiftdetailDto> GetLast30DayShift(string storeEmail, string posUser);
        ShiftdetailDto GetShiftDetailByShiftId(string storeEmail, int posUserId, int shiftId);
        int OpenShift(string storeEmail, decimal startingCash, int posUserId);
        bool IsShiftAvailable(string storeEmail, int posUserId, int shiftId);
        bool CloseShift(string storeEmail, int posUserId, int shiftId);
        bool ManageCash(ManageCashDto manageCash);
    }
}
