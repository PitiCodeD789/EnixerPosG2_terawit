using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class DeviceEntity : BaseEntity
    {
        public int StoreId { get; set; }
        public string PosName { get; set; }
        public string Imei { get; set; }
    }
}
