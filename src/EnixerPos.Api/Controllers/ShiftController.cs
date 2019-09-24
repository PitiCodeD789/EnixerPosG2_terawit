using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost("OpenShift")]
        public IActionResult OpenShift([FromBody]OpenShiftCommand openShift)
        {
            if(openShift.StartingCash < 0)
            {
                return BadRequest();
            }
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            string storeEmail = audience;
            string posUser = user;
            decimal startingCash = openShift.StartingCash;
            int posUserId = openShift.PosUserId;

            ShiftdetailDto newShift = _shiftService.OpenShift(storeEmail, startingCash, posUserId, posUser);

            if(newShift == null || newShift.Id == 0)
            {
                return BadRequest();
            }


            OpenShiftViewModel newOpenShiftViewModel = _mapper.Map<OpenShiftViewModel>(newShift);
            newOpenShiftViewModel.OpenShiftId = newShift.Id;
            return Ok(newOpenShiftViewModel);
        }
        [Authorize]
        [HttpGet("GetShifts")]
        public IActionResult GetListShift()
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            string storeEmail = audience;            
            string posUser = user;

            List<ShiftdetailDto> item = _shiftService.GetLast30DayShift(storeEmail, posUser);

            if(item == null)
            {
                return NoContent();
            }

            List<GetShiftViewModel> shiftlistViewModel = _mapper.Map<List<GetShiftViewModel>>(item);

            return Ok(shiftlistViewModel);
        }
        [Authorize]
        [HttpGet("GetShiftDetail/{shiftId}/UserId/{userId}")]
        public IActionResult GetShiftDetail(int shiftId,int userId)
        {
            if(shiftId == null )
            {
                return BadRequest();
            }
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            string storeEmail = audience;
         
     
            ShiftdetailDto shiftdetail = _shiftService.GetShiftDetailByShiftId(storeEmail, userId, shiftId);

            if(shiftdetail == null)
            {
                return NoContent();
            }
            GetShiftViewModel shiftViewModel = _mapper.Map<GetShiftViewModel>(shiftdetail);

            return Ok(shiftViewModel);
        }
        [Authorize]
        [HttpPost("CloseShift")]
        public IActionResult CloseShift([FromBody] CloseShiftCommand closeShift)
        {
            if(closeShift.UserId < 0)
            {
                return BadRequest();
            }
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            string storeEmail = audience;
            string posUser = user;
            int posUserId = closeShift.UserId;
            int shiftId = closeShift.ShiftId;
            bool isShift = _shiftService.IsShiftAvailable(storeEmail, posUserId, shiftId);
            if(!isShift)
            {
                return BadRequest();
            }

            bool closeShiftStatus = _shiftService.CloseShift(storeEmail, posUserId, shiftId);
            if(!closeShiftStatus)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();

        }
        [Authorize]
        [HttpPost("ManageCash")]
        public IActionResult ManageCash([FromBody] ManageCashCommand manageCash)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;
            string storeEmail = audience;
            string posUser = user;
            int posUserId = manageCash.PosUserId;
            ManageCashStatus cashStatus = manageCash.ManageCashStatus;
            decimal amount = manageCash.Amount;
            string comment = manageCash.Comment;
            int shiftId = manageCash.ShiftId;
        
            ManageCashStatus manageType = manageCash.ManageCashStatus;

            bool isShift = _shiftService.IsShiftAvailable(storeEmail, posUserId, shiftId);
            if (!isShift)
            {
                return BadRequest();
            }



            //  ManageCashDto manageCashDto = _mapper.Map<ManageCashCommand,ManageCashDto>(manageCash);
            ManageCashDto manageCashDto = new ManageCashDto
            {
                Amount = manageCash.Amount,
                Comment = manageCash.Comment,
                ShiftId = manageCash.ShiftId,
                PosUserId = posUserId,
                StoreEmail = storeEmail,
                 ManageCashStatus = manageCash.ManageCashStatus
            };

            bool isManageCash = _shiftService.ManageCash(manageCashDto);
            
            if(!isManageCash)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            return Ok();
        }






    }
}