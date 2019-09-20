using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Helpers;

namespace EnixerPos.Domain.DtoModels.Shifts
{
    public class ManageCashDto : BaseDto
    {
        public  int PosUserId { get;  set; }
        public string PosIMEI { get;  set; }
        public string Comment { get;  set; }
        public Status.ManageCashStatus ManageCashStatus { get;  set; }
        public decimal Amount { get; internal set; }
        public int ShiftId { get; internal set; }
    }
}
