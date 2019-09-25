using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class CheckNameUserDtoCommand
    {
        public string Email { get; set; }
        public string NameUser { get; set; }
    }
}
