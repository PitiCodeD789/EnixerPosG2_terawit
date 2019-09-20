using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Entities
{
    public class TokenEntity
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
