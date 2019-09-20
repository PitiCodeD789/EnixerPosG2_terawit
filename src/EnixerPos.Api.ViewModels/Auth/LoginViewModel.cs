using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public object MerchantName { get; set; }
        public object PosName { get; set; }
    }
}
