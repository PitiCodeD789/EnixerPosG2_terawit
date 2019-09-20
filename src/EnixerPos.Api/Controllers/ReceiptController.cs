using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IReceiptService _receiptService;

        public ReceiptController(IMapper mapper, IReceiptService receiptService)
        {
            _mapper = mapper;
            _receiptService = receiptService;
        }

        [HttpGet("GetReceiptsByDate")]
        public IActionResult GetReceiptsByDate()
        {
            try
            {
                List<ReceiptDto> receiptDtos = _receiptService.GetReceiptsByDate(DateTime.UtcNow.Date);

                if (receiptDtos != null)
                {
                    List<ReceiptViewModel> model = _mapper.Map<List<ReceiptViewModel>>(receiptDtos);
                    return Ok(model);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();

            }

        }

    }
}