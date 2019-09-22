using EnixerPos.Api.ViewModels.Shifts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Service.Interfaces
{
    public interface IShiftService
    {
        List<GetShiftViewModel> GetListShift();
    }
}
