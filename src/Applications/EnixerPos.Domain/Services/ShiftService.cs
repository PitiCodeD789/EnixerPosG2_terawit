using AutoMapper;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using EnixerPos.Api.ViewModels.Sale;
using static EnixerPos.Api.ViewModels.Enixer_Enumerations;

namespace EnixerPos.Domain.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IManageCashRepository _manageCashRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly IMapper _mapper;
        public ShiftService(IShiftRepository shiftRepository, IMapper mapper, IManageCashRepository manageCashRepository, IReceiptRepository receiptRepository)
        {
            _shiftRepository = shiftRepository;
            _manageCashRepository = manageCashRepository;
            _receiptRepository = receiptRepository;
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
            shiftEntity.UpdateDateTime = DateTime.UtcNow;
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


            List<ReceiptEntity> receiptEntity  = _receiptRepository.GetReceiptByShiftId(shiftEntity.ShiftId, shiftEntity.StoreEmail,shiftEntity.PosIMEI);
            

            decimal cashPayment = 0m;
            decimal cash = 0m;
            decimal discount = 0m;
            decimal debitCard = 0m;
            decimal creditCard = 0m;
            decimal qrCode = 0m;
            


            foreach (ReceiptEntity receipt in receiptEntity)
            {
               

                cashPayment += receipt.Total;

              //List<OrderItemModel> order  = JsonConvert.DeserializeObject<List<OrderItemModel>>(receipt.ItemList);
              //  foreach(OrderItemModel buff_order in order)
              //  {
              //      decimal fullprice = buff_order.ItemPrice * buff_order.Quantity;
              //      decimal discountPrice = 0m;
              //      if(buff_order.IsDiscountPercentage)
              //      {
              //          discountPrice = fullprice * 10m / 100m;
              //      }else
              //      {
              //          discountPrice = buff_order.ItemDiscount;
              //      }
              //      discount = discount + discountPrice;

                //  }

            }

         

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
