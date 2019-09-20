using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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


        public SaleController(ISaleService saleService,IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }

        //Payment(type)
        //CheckPayment


        public IActionResult Payment(PaymentCommand payment)
        {
            PaymentDtoCommand paymentDto = _mapper.Map<PaymentDtoCommand>(payment);

            ReceiptViewModel receiptDto = _saleService.CreateReceipt(payment);

            if (receiptDto != null)
            {
                return BadRequest();
            }

            ReceiptViewModel model = _mapper.Map<ReceiptViewModel>(receiptDto);


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