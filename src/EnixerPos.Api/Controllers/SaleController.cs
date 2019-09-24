using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Helpers;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        private readonly RealAutoMapper _realMapper = new RealAutoMapper();

        public SaleController(ISaleService saleService,IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        //Payment(type)
        //CheckPayment

        [HttpPost("Payment")]
        public IActionResult Payment([FromBody]PaymentCommand payment)
        {
            //PaymentDtoCommand paymentDto = _mapper.Map<PaymentDtoCommand>(payment);

            ReceiptDto receiptDto = _saleService.CreateReceipt(payment);

            if (receiptDto == null)
            {
                return BadRequest();
            }

            ReceiptViewModel model = new ReceiptViewModel()
            {
                CreateDateTime = receiptDto.CreateDateTime,
                Discount = receiptDto.Discount,
                IsDiscountPercentage = receiptDto.IsDiscountPercentage,
                PaymentType = receiptDto.PaymentType,
                TotalDiscount = receiptDto.TotalDiscount,
                ItemList = new List<ViewModels.Sale.OrderItemModel>(),
                Reference = receiptDto.Reference,
                ShiftId = receiptDto.ShiftId,
                Store = receiptDto.Store,
                Total = receiptDto.Total
            };

            foreach (var i in receiptDto.ItemList)
            {
                var m = new ViewModels.Sale.OrderItemModel()
                {
                    IsDiscountPercentage = i.IsDiscountPercentage,
                    ItemDiscount = i.ItemDiscount,
                    ItemName = i.ItemName,
                    ItemPrice = i.ItemPrice,
                    OptionName = i.OptionName,
                    OptionPrice = i.OptionPrice,
                    Quantity = i.Quantity
                };
                model.ItemList.Add(m);
            }

            return Ok(model);
        }

        [HttpGet("CheckPayment/{paymentRef}")]
        public IActionResult CheckPayment(string paymentRef)
        {
            bool isPaymentSuccess = _saleService.CheckPayment(paymentRef);

            if (isPaymentSuccess)
            {
                return NoContent();
            }
            return BadRequest();
        }
       


    }
}