using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public object StoreName { get; set; }
        public object PosName { get; set; }
    }
}
