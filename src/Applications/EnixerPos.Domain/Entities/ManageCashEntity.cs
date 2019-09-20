using System;
using System.Collections.Generic;
using System.Text;
using static EnixerPos.Api.ViewModels.Enixer_Enumerations;

namespace EnixerPos.Domain.Entities
{
    public class ManageCashEntity : BaseEntity
    {
        public int PosUserId { get; set; }
        public string PosIMEI { get; set; }
        public string Comment { get; set; }
        public ManageCashStatus ManageCashStatus { get; set; }
        public decimal Amount { get; internal set; }
        public int ShiftId { get; internal set; }
        public string StoreEmail { get; set; }
        public string PosImei { get; set; }

    }
}
