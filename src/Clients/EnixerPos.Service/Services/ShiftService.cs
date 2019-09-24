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
        private string serviceUrl = Helper.BaseUrl + "Shift/";

      
        public bool CloseListShift(int openShiftId, int userId)
        {
            string url = serviceUrl + "CloseShift";
            CloseShiftCommand closeShift = new CloseShiftCommand { ShiftId = openShiftId, UserId =  userId };
            var status =   Post<OpenShiftViewModel>(url, closeShift).Result;
            if(status.IsError == System.Net.HttpStatusCode.OK)
            {
                return true;
            }else
            {
                return false;
            }

        }

        public List<GetShiftViewModel> GetListShift()
        {
            string url = serviceUrl + "GetShifts";

            var result = Get<ResultViewModel>(url);

            return new List<GetShiftViewModel>();
               
        }

        public GetShiftViewModel GetShiftDetail(int shiftId, int userId)
        {
            string url = serviceUrl + $"GetShiftDetail/{shiftId}/UserId/{userId}";

            var result = Get<GetShiftViewModel>(url).Result;
            if(result.IsError ==   System.Net.HttpStatusCode.OK)
            {
                return result.Model;
            }else
            {
                return null;
            }
        
        }

        public async Task<bool> ManageCashPay(ManageCashCommand manage)
        {
            string url = serviceUrl + "ManageCash";
            var isokStatus =  await Post<DummyModel>(url, manage);
            if(isokStatus.IsError == System.Net.HttpStatusCode.OK)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public async Task<ResultServiceModel<OpenShiftViewModel>> OpenShift(OpenShiftCommand model)
        {
            string url = serviceUrl + "OpenShift";
            return await Post<OpenShiftViewModel>(url, model);
        }

     
    }
}
