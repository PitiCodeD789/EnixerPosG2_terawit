using AutoMapper;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IMapper _mapper;
        public ShiftService(IShiftRepository shiftRepository, IMapper mapper)
        {
            _shiftRepository = shiftRepository;
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
            throw new NotImplementedException();
        }

        public bool IsShiftAvailable(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {
            throw new NotImplementedException();
        }

        public bool ManageCash(int posUserId, string posIMEI, int shiftId, decimal amount, string comment)
        {
            throw new NotImplementedException();
        }

        public int OpenShift(string storeEmail, string posIMEI, decimal startingCash, int posUserId)
        {
            throw new NotImplementedException();
        }
    }
}
