using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.Domain.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IStoreRepository _storeRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IStoreRepository storeRepository)
        {
            _receiptRepository = receiptRepository;
            _storeRepository = storeRepository;
        }
        public List<ReceiptDto> GetReceiptsByDate(DateTime date, string email,int shiftId)
        {
            try
            {
                List<ReceiptEntity> receipts = _receiptRepository.GetReceiptsByDateAndShift(date, shiftId);

                List<ReceiptDto> receiptDto = receipts.Select(s => new ReceiptDto()
                {
                    Reference = s.Reference,
                    ShiftId = s.ShiftId,
                    Store = _storeRepository.GetStoreByEmail(email).StoreName,
                    ItemList = JsonConvert.DeserializeObject<List<OrderItemModel>>(s.ItemList),
                    Discount = s.Discount,
                    IsDiscountPercentage = s.IsDiscountPercentage,
                    Total = s.Total,
                    TotalDiscount = s.TotalDiscount,
                    CreateDateTime = s.CreateDateTime,
                    PaymentType = s.PaymentType
                }).ToList();

                return receiptDto;
            }
            catch (Exception e)
            {

                return null;
            }
            
        }
    }
}
