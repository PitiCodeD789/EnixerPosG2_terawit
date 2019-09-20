using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class ShiftEntity
    {
        public int ShiftId { get; internal set; }
        public string StoreEmail { get; internal set; }
        public string PosIMEI { get; internal set; }
        public int PosUserId { get; internal set; }
        public DateTime UpdateDatetime { get; internal set; }
    }
}
