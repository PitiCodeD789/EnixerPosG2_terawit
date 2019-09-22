using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using EnixerPos.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnixerPos.Service.Services
{
    public class ShiftService : BaseService, IShiftService
    {
        private string serviceUrl = Helper.BaseUrl + "Shift/GetShifts";
        public List<GetShiftViewModel> GetListShift()
        {

           var result = Get<ResultViewModel>(serviceUrl);

            return new List<GetShiftViewModel>();
               
        }

        public async Task<ResultServiceModel<OpenShiftViewModel>> OpenShift(OpenShiftCommand model)
        {
            string url = serviceUrl + "OpenShift";
            return await Post<OpenShiftViewModel>(url, model);
        }
    }
}
