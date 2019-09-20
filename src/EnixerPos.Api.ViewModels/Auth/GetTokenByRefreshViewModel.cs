using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class GetTokenByRefreshViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
    }
}
