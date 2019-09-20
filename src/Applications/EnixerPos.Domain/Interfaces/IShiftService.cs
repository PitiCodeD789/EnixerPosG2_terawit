﻿using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Shifts;

namespace EnixerPos.Domain.Interfaces
{
    public interface IShiftService
    {
        List<ShiftdetailDto> GetLast30DayShift();
        ShiftdetailDto GetShiftDetailByShiftId(int shiftId);
        int OpenShift(string storeEmail, string posIMEI, decimal startingCash, int posUserId);
        bool IsShiftAvailable(string storeEmail, string posIMEI, int posUserId, int shiftId);
        bool CloseShift(string storeEmail, string posIMEI, int posUserId, int shiftId);
        bool ManageCash(int posUserId, string posIMEI, int shiftId, decimal amount, string comment);
    }
}