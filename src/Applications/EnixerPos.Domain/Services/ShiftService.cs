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

            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posIMEI, posUserId, shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            if(shiftEntity.Available == true)
            {
                shiftEntity = GetShiftFormReceipts(shiftEntity);
                shiftEntity.UpdateDateTime = DateTime.UtcNow;
                return _shiftRepository.Update(shiftEntity);
            }else
            {
                return false;
            }
          

        }

     

        public ShiftdetailDto GetShiftDetailByShiftId(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {
           
            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posIMEI,posUserId, shiftId);
            if (shiftEntity == null)
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
            decimal discount = 0m;
            decimal debitCard = 0m;
            decimal creditCard = 0m;
            decimal qrCode = 0m;
            


            foreach (ReceiptEntity receipt in receiptEntity)
            {
               
                if(receipt.PaymentType == EP_PaymentTypeEnum.Cash)
                {
                cashPayment += receipt.Total;
                }else if(receipt.PaymentType == EP_PaymentTypeEnum.Credit)
                {
                    creditCard += receipt.Total;
                }
                else if (receipt.PaymentType == EP_PaymentTypeEnum.Debit)
                {
                    debitCard += receipt.Total;
                }
                else if (receipt.PaymentType == EP_PaymentTypeEnum.Qr)
                {
                    qrCode += receipt.Total;
                }

                discount += receipt.TotalDiscount;




            }


        
            shiftEntity.CashPayment = cashPayment;
            shiftEntity.CreditCard = creditCard;
            shiftEntity.DebitCard = debitCard;
            shiftEntity.QRCode = qrCode;

            decimal payIn = 0;
            decimal payOut = 0;
            List<ManageCashEntity> manageCash = _manageCashRepository.GetManageCashByShiftId(shiftEntity.ShiftId, shiftEntity.StoreEmail, shiftEntity.PosIMEI);
            foreach(ManageCashEntity manage in manageCash)
            {
                if(manage.ManageCashStatus == ManageCashStatus.PayIn)
                {
                    payIn += manage.Amount;

                }else if (manage.ManageCashStatus == ManageCashStatus.PayOut)
                {
                    payOut += manage.Amount;

                }
            }

            shiftEntity.Paidin = payIn;
            shiftEntity.Paidout = payOut;


            return shiftEntity;
        }

        public bool IsShiftAvailable(string storeEmail, string posIMEI, int posUserId, int shiftId)
        {
            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posIMEI, posUserId, shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            if(shiftEntity.StoreEmail == storeEmail && shiftEntity.PosIMEI == posIMEI && shiftEntity.Available == true)
            {
                return true;
            }else
            {
                return false;
            }
        }



        public bool ManageCash(ManageCashDto manageCash)
        {
           
            ManageCashEntity manageCashEntity = _mapper.Map<ManageCashEntity>(manageCash);
            bool isManageCash =  _manageCashRepository.AddManageCash(manageCashEntity);
            return isManageCash;

        }

        public int OpenShift(string storeEmail, string posIMEI, decimal startingCash)
        {
            ShiftEntity shiftEntity = new ShiftEntity();
            shiftEntity.StoreEmail = storeEmail;
            shiftEntity.PosIMEI = posIMEI;
            shiftEntity.StartingCash = startingCash;
           
            bool isCreateShift = _shiftRepository.CreateShift(shiftEntity);
            if(isCreateShift)
            {
               return _shiftRepository.GetShiftId(shiftEntity.StoreEmail, shiftEntity.PosIMEI, shiftEntity.PosUserId);
            }
            else
            {
                return 0;
            }
           
           
        }

        public List<ShiftdetailDto> GetLast30DayShift(string storeEmail, string posIMEI, int posUserId)
        {
            List<ShiftEntity> shiftEntity = _shiftRepository.GetLast30DayShift(storeEmail, posIMEI, posUserId);
            if (shiftEntity == null)
            {
                return null;
            }

            return _mapper.Map<List<ShiftdetailDto>>(shiftEntity);
        }

        public int OpenShift(string storeEmail, string posIMEI, decimal startingCash, int posUserId)
        {
            throw new NotImplementedException();
        }
    }
}
