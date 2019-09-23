using System;
using System.Collections.Generic;
using System.Text;
using static EnixerPos.Api.ViewModels.Enixer_Enumerations;

namespace EnixerPos.Domain.DtoModels.Shifts
{
    public class ManageCashDto : BaseDto
    {
        public  int PosUserId { get;  set; }
        public string Comment { get;  set; }
        public ManageCashStatus ManageCashStatus { get;  set; }
        public decimal Amount { get; internal set; }
        public int ShiftId { get; internal set; }
    }
}
