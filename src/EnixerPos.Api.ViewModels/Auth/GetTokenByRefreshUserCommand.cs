using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class GetTokenByRefreshUserCommand
    {
        public string RefreshToken { get; set; }
        public string User { get; set; }
    }
}
