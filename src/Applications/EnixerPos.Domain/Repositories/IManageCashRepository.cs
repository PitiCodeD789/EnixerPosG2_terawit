using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IManageCashRepository
    {
        bool AddManageCash(ManageCashEntity manageCash);
        List<ManageCashEntity> GetManageCashByShiftId(int shiftId, string storeEmail, string posIMEI);
    }
}
