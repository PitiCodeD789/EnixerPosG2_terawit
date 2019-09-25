using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class SetPasswordDtoCommand
    {
        public string OTP { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
