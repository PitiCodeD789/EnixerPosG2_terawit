﻿using EnixerPos.Domain.DtoModels.Sale;
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
        private readonly IDeviceRepository _deviceRepository;

        public ReceiptService(IReceiptRepository receiptRepository, IStoreRepository storeRepository,
            IDeviceRepository deviceRepository)
        {
            _receiptRepository = receiptRepository;
            _storeRepository = storeRepository;
            _deviceRepository = deviceRepository;
        }
        public List<ReceiptDto> GetReceiptsByDate(DateTime date)
        {
            try
            {
                List<ReceiptEntity> receipts = _receiptRepository.GetReceiptsByDate(date);

                List<ReceiptDto> receiptDto = receipts.Select(s => new ReceiptDto()
                {
                    Reference = s.Reference,
                    ShiftId = s.ShiftId,
                    Store = _storeRepository.GetStoreByEmail(s.StoreEmail).StoreName,
                    Pos = _deviceRepository.GetDeviceByImei(s.PosImei).PosName,
                    ItemList = JsonConvert.DeserializeObject<List<OrderItemModel>>(s.ItemList),
                    Discount = s.Discount,
                    IsDiscountPercentage = s.IsDiscountPercentage,
                    Total = s.Total,
                    TotalDiscount = s.TotalDiscount,
                    CreateDateTime = s.CreateDateTime
                }).ToList();

                return receiptDto;
            }
            catch (Exception)
            {

                return null;
            }
            
        }
    }
}