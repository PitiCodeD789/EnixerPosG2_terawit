using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Imei { get; set; }
    }
}
