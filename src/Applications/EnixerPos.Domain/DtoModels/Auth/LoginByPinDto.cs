using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class LoginByPinDto
    {
        public string User { get; set; }
        public object UserId { get; set; }
    }
}
