using AutoMapper;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace EnixerPos.Domain.Services
{
    public class SaleService : ISaleService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IMapper _mapper;

        public SaleService(IReceiptRepository receiptRepository, IMapper mapper)
        {
            _receiptRepository = receiptRepository;
            _mapper = mapper;
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
            
            string itemList = JsonConvert.SerializeObject(payment.ItemList);
            ReceiptEntity receiptEntity = new ReceiptEntity()
            {
                ShiftId = payment.ShiftId,
                Store = payment.Store,
                Pos = payment.Pos,
                ItemList = itemList,
                Discount = payment.Discount,
                Total = payment.Total,
            };

            _receiptRepository.Create(receiptEntity);

            receiptEntity.Reference = GenerateRef(receiptEntity.Id);

            _receiptRepository.Update(receiptEntity);

            return new ReceiptDto()
            {
                Reference = receiptEntity.Reference,
                ShiftId = payment.ShiftId,
                Store = payment.Store,
                Pos = payment.Pos,
                ItemList = _mapper.Map<List<DtoModels.Sale.OrderItemModel>>(itemList),
                Discount = payment.Discount,
                Total = payment.Total,
                CreateDateTime = receiptEntity.CreateDateTime
            };
        }

        private string GenerateRef(int id)
        {
            throw new NotImplementedException();
        }
    }
}
