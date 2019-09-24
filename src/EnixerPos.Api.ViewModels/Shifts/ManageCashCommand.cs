using System;
using System.Collections.Generic;
using System.Text;
using static EnixerPos.Api.ViewModels.Enixer_Enumerations;

namespace EnixerPos.Api.ViewModels.Shifts
{
    public class ManageCashCommand
    {
        public ManageCashStatus ManageCashStatus { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int ShiftId { get; set; }
        public int PosUserId { get; set; }

       
    }
}
