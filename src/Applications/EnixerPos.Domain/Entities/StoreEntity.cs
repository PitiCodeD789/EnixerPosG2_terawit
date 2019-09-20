using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class StoreEntity : BaseEntity
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        public int Id { get; set; }
        public string StoreName { get; set; }
    }
}
