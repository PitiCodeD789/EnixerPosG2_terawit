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
        Task<ResultServiceModel<OpenShiftViewModel>> OpenShift(OpenShiftCommand model);
    }
}
