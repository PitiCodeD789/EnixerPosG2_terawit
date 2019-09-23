using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string NameUser { get; set; }
        public int StoreId { get; set; }
        public string Pin { get; set; }
    }
}
