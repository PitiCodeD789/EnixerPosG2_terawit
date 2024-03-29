﻿using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using Newtonsoft.Json;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;


namespace EnixerPos.Domain.Services
{
    public class SaleService : ISaleService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;

        public SaleService(IReceiptRepository receiptRepository, IMapper mapper
            ,IStoreRepository storeRepository)
        {
            _receiptRepository = receiptRepository;
            _mapper = mapper;
            _storeRepository = storeRepository;
        }
        public bool CheckPayment(string paymentRef)
        {
            try
            {
                //ยิงไป transaction เก่า

                //bool isPaymentSuccess = 
                //if (isPaymentSuccess != null)
                //{
                //    return true;

                //}
                return false;
                
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public ReceiptDto CreateReceipt(PaymentCommand payment)
        {
            try
            {
                string itemList = JsonConvert.SerializeObject(payment.ItemList);
                ReceiptEntity receiptEntity = new ReceiptEntity()
                {
                    ShiftId = payment.ShiftId,
                    StoreEmail = payment.StoreEmail,
                    ItemList = itemList,
                    Discount = payment.Discount,
                    IsDiscountPercentage = payment.IsDiscountPercentage,
                    Total = payment.Total,
                    TotalDiscount = payment.TotalDiscount,
                    PaymentType = payment.PaymentType
                };

                _receiptRepository.Create(receiptEntity);

                receiptEntity.Reference = GenerateRef(payment.ShiftId,receiptEntity.Id);

                _receiptRepository.Update(receiptEntity);

                var storeName = _storeRepository.GetStoreByEmail(payment.StoreEmail).StoreName;


                return new ReceiptDto()
                {
                    Reference = receiptEntity.Reference,
                    ShiftId = payment.ShiftId,
                    Store = storeName,
                    ItemList = JsonConvert.DeserializeObject<List<DtoModels.Sale.OrderItemModel>>(itemList),
                    Discount = payment.Discount,
                    IsDiscountPercentage = payment.IsDiscountPercentage,
                    Total = payment.Total,
                    TotalDiscount = payment.TotalDiscount,
                    CreateDateTime = receiptEntity.CreateDateTime,
                    PaymentType = payment.PaymentType
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string GenerateRef(int shiftId, int id)
        {
            return "#" + shiftId + "-" + id.ToString("D4");
        }
    }
}
