using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels.Shifts;

namespace EnixerPos.Domain.Interfaces
{
    public interface IShiftService
    {
        List<ShiftdetailDto> GetLast30DayShift(string storeEmail, string posUser);
        ShiftdetailDto GetShiftDetailByShiftId(string storeEmail, int posUserId, int shiftId);
        ShiftdetailDto OpenShift(string storeEmail, decimal startingCash, int posUserId, string posUser);
        bool IsShiftAvailable(string storeEmail, int posUserId, int shiftId, Api.ViewModels.Enixer_Enumerations.ManageCashStatus cashStatus, decimal amount);
        bool CloseShift(string storeEmail, int posUserId, int shiftId);
        bool ManageCash(ManageCashDto manageCash);
        bool IsShiftAvailable(string storeEmail, int posUserId, int shiftId);
    }
}
