using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class TokenEntity : BaseEntity
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
