using System;
using System.Collections.Generic;
using System.Text;
using static EnixerPos.Api.ViewModels.Helpers.Status;

namespace EnixerPos.Domain.Entities
{
    public class ManageCashEntity
    {
        public int PosUserId { get; set; }
        public string PosIMEI { get; set; }
        public string Comment { get; set; }
        public ManageCashStatus ManageCashStatus { get; set; }
        public decimal Amount { get; internal set; }
        public int ShiftId { get; internal set; }

    }
}
