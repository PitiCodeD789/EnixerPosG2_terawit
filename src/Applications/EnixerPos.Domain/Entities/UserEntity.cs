using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string User { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
        public string Salt { get; set; }

    }
}
