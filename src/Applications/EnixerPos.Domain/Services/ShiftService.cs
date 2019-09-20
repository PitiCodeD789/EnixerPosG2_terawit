using AutoMapper;
using EnixerPos.Api.ViewModels.Helpers;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using static EnixerPos.Api.ViewModels.Helpers.Status;

namespace EnixerPos.Domain.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IManageCashRepository _manageCashRepository;
        private readonly IMapper _mapper;
        public ShiftService(IShiftRepository shiftRepository, IMapper mapper, IManageCashRepository manageCashRepository)
        {
            _shiftRepository = shiftRepository;
            _manageCashRepository = manageCashRepository;
            _mapper = mapper;
        }


        public bool CloseShift(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {

            ShiftEntity shiftEntity = _shiftRepository.GetShiftById(shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            shiftEntity.ShiftId = shiftId;
            shiftEntity.StoreEmail = storeEmail;
            shiftEntity.PosIMEI = posIMEI;
            shiftEntity.PosUserId = posUserId;
            shiftEntity.UpdateDatetime = DateTime.UtcNow;
            return _shiftRepository.Update(shiftEntity);

        }

        public List<ShiftdetailDto> GetLast30DayShift()
        {
            List<ShiftEntity> shiftEntity = _shiftRepository.GetLast30DayShift();
            if(shiftEntity == null)
            {
                return null;
            }

           return _mapper.Map<List<ShiftdetailDto>>(shiftEntity);

        }

        public ShiftdetailDto GetShiftDetailByShiftId(int shiftId)
        {
            ShiftEntity shiftEntity = _shiftRepository.Get(shiftId);
            if(shiftEntity == null)
            {
                return null;
            }

            if(shiftEntity.Available == true)
            {
                shiftEntity =  GetShiftFormReceipts(shiftEntity);
            }

            return _mapper.Map<ShiftdetailDto>(shiftEntity);
        }

        private ShiftEntity GetShiftFormReceipts(ShiftEntity shiftEntity)
        {
          


            return shiftEntity;
        }

        public bool IsShiftAvailable(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {
            ShiftEntity shiftEntity = _shiftRepository.Get(shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            if(shiftEntity.StoreEmail == storeEmail && shiftEntity.PosIMEI == posIMEI)
            {
                return true;
            }else
            {
                return false;
            }
        }



        public bool ManageCash(ManageCashDto manageCash)
        {
            int posUserId = manageCash.PosUserId;
            string posIMEI = manageCash.PosIMEI;
            int shiftId = manageCash.ShiftId;
            decimal amount = manageCash.Amount;
            string comment = manageCash.Comment;
            ManageCashStatus manageCashStatus = manageCash.ManageCashStatus;
           
            ManageCashEntity manageCashEntity = _mapper.Map<ManageCashEntity>(manageCash);
            bool isManageCash =  _manageCashRepository.AddManageCash(manageCashEntity);
            return isManageCash;

        }

        public int OpenShift(string storeEmail, string posIMEI, decimal startingCash, int posUserId)
        {
            ShiftEntity shiftEntity = new ShiftEntity();
            shiftEntity.StoreEmail = storeEmail;
            shiftEntity.PosIMEI = posIMEI;
            shiftEntity.StartingCash = startingCash;
            shiftEntity.PosUserId = posUserId;

            int openShiftId = _shiftRepository.CreateShift(shiftEntity);
            return openShiftId;
        }
    }
}
