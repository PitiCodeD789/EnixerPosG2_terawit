using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class RegisterUserInStoreDtoCommand
    {
        public string Email { get; set; }
        public string NameUser { get; set; }
        public string Pin { get; set; }
    }
}
