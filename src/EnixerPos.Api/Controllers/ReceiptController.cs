using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("GetReceiptsByDate")]
        public IActionResult GetReceiptsByDate()
        {
            var audience = "";
            var user = "";
            try
            {
                audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
                user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            try
            {
                List<ReceiptDto> receiptDtos = _receiptService.GetReceiptsByDate(DateTime.UtcNow.Date, audience);

                if (receiptDtos != null)
                {
                    //List<ReceiptViewModel> model = new List<ReceiptViewModel>();
                    //foreach (var item in receiptDtos)
                    //{
                    //    model.Add(_mapper.Map<ReceiptViewModel>(item));
                    //}
                    //return Ok(model);
                    return Ok(receiptDtos);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();

            }

        }

    }
}