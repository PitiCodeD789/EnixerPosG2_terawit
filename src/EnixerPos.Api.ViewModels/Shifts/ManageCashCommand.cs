using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Helpers;
using static EnixerPos.Api.ViewModels.Helpers.Status;

namespace EnixerPos.Api.ViewModels.Shifts
{
    public class ManageCashCommand
    {
        public ManageCashStatus ManageCashStatus { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int ShiftId { get; set; }
    }
}
