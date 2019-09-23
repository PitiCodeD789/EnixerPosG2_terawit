using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginByPinCommand
    {
        
        public string Pin { get; set; }
        public string Email { get; set; }
    }
}
