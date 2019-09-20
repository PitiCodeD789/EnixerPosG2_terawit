using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EnixerPos.Api.ViewModels.Enixer_Enumerations;

namespace EnixerPos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        private readonly IMapper _mapper;
        public ShiftController(IShiftService shiftService, IMapper mapper)
        {
            _shiftService = shiftService;
            _mapper = mapper;
        }

        [HttpPost("OpenShift")]
        public IActionResult OpenShift([FromBody]OpenShiftCommand openShift)
        {
            if(openShift.StartingCash < 0)
            {
                return BadRequest();
            }
            decimal startingCash = openShift.StartingCash;
            string storeEmail = "sert@gmail.com";
            string posIMEI = "00200202020000";
            int posUserId = 12;

            int openId = _shiftService.OpenShift(storeEmail, posIMEI, startingCash, posUserId);

            if(openId == null)
            {
                return NoContent();
            }

            OpenShiftViewModel openShiftView = new OpenShiftViewModel { OpenShiftId = openId };

            return Ok(openShiftView);
        }

        [HttpGet("GetShifts")]
        public IActionResult GetListShift()
        {
            List<ShiftdetailDto> item = _shiftService.GetLast30DayShift();

            if(item == null)
            {
                return NoContent();
            }

            List<GetShiftViewModel> shiftlistViewModel = _mapper.Map<List<GetShiftViewModel>>(item);

            return Ok(shiftlistViewModel);
        }

        [HttpGet("GetShiftDetail/{shiftId}")]
        public IActionResult GetShiftDetail(int shiftId)
        {
            if(shiftId == null )
            {
                return BadRequest();
            }

            string storeEmail = "sert@gmail.com";
            string posIMEI = "00200202020000";
            int posUserId = 12;
          

            ShiftdetailDto shiftdetail = _shiftService.GetShiftDetailByShiftId(storeEmail,posIMEI,posUserId,shiftId);

            if(shiftdetail == null)
            {
                return NoContent();
            }
            GetShiftViewModel shiftViewModel = _mapper.Map<GetShiftViewModel>(shiftdetail);

            return Ok(shiftViewModel);
        }

        [HttpPost("CloseShift")]
        public IActionResult CloseShift([FromBody] CloseShiftCommand closeShift)
        {
            if(closeShift.UserId < 0)
            {
                return BadRequest();
            }
         
            string storeEmail = "sert@gmail.com";
            string posIMEI = "00200202020000";
            int posUserId = 12;
            int shiftId = closeShift.ShiftId;
            bool isShift = _shiftService.IsShiftAvailable(storeEmail, posIMEI, posUserId, shiftId);
            if(!isShift)
            {
                return BadRequest();
            }

            bool closeShiftStatus = _shiftService.CloseShift(storeEmail, posIMEI, posUserId, shiftId);
            if(!closeShiftStatus)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();

        }

        [HttpPost("ManageCash")]
        public IActionResult ManageCash([FromBody] ManageCashCommand manageCash)
        {
            ManageCashStatus cashStatus = manageCash.ManageCashStatus;
            decimal amount = manageCash.Amount;
            string comment = manageCash.Comment;
            int shiftId = manageCash.ShiftId;
            string storeEmail = "sert@gmail.com";
            string posIMEI = "00200202020000";
            int posUserId = 12;
            ManageCashStatus manageType = manageCash.ManageCashStatus;

            bool isShift = _shiftService.IsShiftAvailable(storeEmail, posIMEI, posUserId, shiftId);
            if (!isShift)
            {
                return BadRequest();
            }

           

            ManageCashDto manageCashDto = _mapper.Map<ManageCashDto>(manageCash);

            bool isManageCash = _shiftService.ManageCash(manageCashDto);
            
            if(!isManageCash)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            return Ok();
        }






    }
}