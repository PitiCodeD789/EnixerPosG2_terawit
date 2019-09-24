using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Interfaces
{
    public interface IShiftService
    {
        List<GetShiftViewModel> GetListShift();
        Task<ResultServiceModel<OpenShiftViewModel>> OpenShift(OpenShiftCommand model);
        bool CloseListShift(int openShiftId, int userId);
        ResultServiceModel<GetShiftViewModel> GetShiftDetail( int shiftId, int userId);
    }
}
