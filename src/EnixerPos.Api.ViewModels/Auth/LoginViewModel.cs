using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string RefreshToken { get; set; }
        public string StoreName { get; set; }
        public string EWalletAccNo { get; set; }
    }
}
