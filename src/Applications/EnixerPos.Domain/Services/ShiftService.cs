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
using EnixerPos.Api.ViewModels.Shifts;
using System.Linq;

namespace EnixerPos.Domain.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IManageCashRepository _manageCashRepository;
        private readonly IUserRepository _userRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly IMapper _mapper;
        public ShiftService(IShiftRepository shiftRepository, IMapper mapper, 
            IManageCashRepository manageCashRepository, IUserRepository userRepository,
            IReceiptRepository receiptRepository)
        {
            _shiftRepository = shiftRepository;
            _manageCashRepository = manageCashRepository;
            _receiptRepository = receiptRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public bool CloseShift(string storeEmail, int posUserId, int shiftId)
        {

            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posUserId, shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            if(shiftEntity.Available == true)
            {
                shiftEntity.Available = false;
                shiftEntity = GetShiftFormReceipts(shiftEntity);
                shiftEntity.UpdateDateTime = DateTime.UtcNow;
                return _shiftRepository.Update(shiftEntity);
            }else
            {
                return false;
            }
          

        }

     

        public ShiftdetailDto GetShiftDetailByShiftId(string storeEmail, int posUserId, int shiftId)
        {
           
            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail,posUserId, shiftId);
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


            List<ReceiptEntity> receiptEntity  = _receiptRepository.GetReceiptByShiftId(shiftEntity.Id, shiftEntity.StoreEmail);
            

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
            shiftEntity.Discount = discount;

            decimal payIn = 0;
            decimal payOut = 0;
            List<ManageCashEntity> manageCash = _manageCashRepository.GetManageCashByShiftId(shiftEntity.Id, shiftEntity.StoreEmail);
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

        public bool IsShiftAvailable(string storeEmail, int posUserId, int shiftId, Api.ViewModels.Enixer_Enumerations.ManageCashStatus cashStatus, decimal amount)
        {
            
            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posUserId, shiftId);
            if(shiftEntity == null)
            {
                return false;
            }
            if(cashStatus == ManageCashStatus.PayOut)
            {
                decimal expectedcashamount = shiftEntity.StartingCash + shiftEntity.CashPayment + shiftEntity.Paidin - shiftEntity.Paidout - shiftEntity.Refunds;
                if(expectedcashamount < amount)
                {
                    return false;
                }
            }
            if(shiftEntity.StoreEmail == storeEmail && shiftEntity.Available == true)
            {
                return true;
            }else
            {
                return false;
            }
        }



        public bool ManageCash(ManageCashDto manageCash)
        {

            //ManageCashEntity manageCashEntity = _mapper.Map<ManageCashEntity>(manageCash);
            ManageCashEntity manageCashEntity = new ManageCashEntity
            {
                Amount = manageCash.Amount,
                Comment = manageCash.Comment,
                PosUserId = manageCash.PosUserId,
                ShiftId = manageCash.ShiftId,
                ManageCashStatus = manageCash.ManageCashStatus, 
                StoreEmail = manageCash.StoreEmail
            };


            bool isManageCash =  _manageCashRepository.AddManageCash(manageCashEntity);
            return isManageCash;

        }

   

        public List<ShiftdetailDto> GetLast30DayShift(string storeEmail, string posUser)
        {
             List<ShiftEntity> shiftEntity = _shiftRepository.GetLast30DayShift(storeEmail, posUser);
            if (shiftEntity == null)
            {
                return null;
            }

            //var ResCarColor = ModelColor.Select(c => new CarColorResultModel()
            //{
            //    ColorId = c.ColorId,
            //    ColorName = c.ColorName
            //}).ToList();

            var ShiftDetail = shiftEntity.Select(c => new ShiftdetailDto()
            {
                StartingCash = c.StartingCash,
                CashPayment = c.CashPayment,
                CashRefunds = c.CashRefunds,
                Paidin = c.Paidin,
                Paidout = c.Paidout,               
                DebitCard = c.DebitCard,
                CreditCard = c.CreditCard,
                QRCode = c.QRCode,               
                UpdateDateTime = c.UpdateDateTime,
                Refunds = c.Refunds,
                Id = c.Id,
                Discount = c.Discount,
                CreateDateTime = c.CreateDateTime
            }).ToList();

            return ShiftDetail;

            //return _mapper.Map<List<ShiftdetailDto>>(shiftEntity);
        }

    
        ShiftdetailDto IShiftService.OpenShift(string storeEmail, decimal startingCash, int posUserId,string posUser)
        {
            try
            {
                UserEntity user = _userRepository.GetUserByUserName(posUser);
                if(user == null)
                {
                    return null;
                }
                ShiftEntity shiftEntity = new ShiftEntity();
                shiftEntity.StoreEmail = storeEmail;
                shiftEntity.StartingCash = startingCash;
                shiftEntity.PosUserId = posUserId;
                shiftEntity.Available = true;
                
                ShiftEntity data = _shiftRepository.GetShift(storeEmail, user.Id);
                ShiftEntity newShift;
                if (data == null)
                {
                    newShift = _shiftRepository.CreateShift(shiftEntity);
                    return _mapper.Map<ShiftdetailDto>(newShift);

                }
                if (data.Available == true)
                {
                    return null;
                }
                newShift = _shiftRepository.CreateShift(shiftEntity);
                ShiftdetailDto shiftdetailDto = _mapper.Map<ShiftdetailDto>(newShift);

                return shiftdetailDto;

            }
            catch (Exception e)
            {

                return null;
            }
        }

        public bool IsShiftAvailable(string storeEmail, int posUserId, int shiftId)
        {
            ShiftEntity shiftEntity = _shiftRepository.GetShiftDetailByShiftId(storeEmail, posUserId, shiftId);
            if (shiftEntity == null)
            {
                return false;
            }
            if (shiftEntity.StoreEmail == storeEmail && shiftEntity.Available == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
