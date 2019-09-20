using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class GetTokenByRefreshMerchantCommand
    {
        public string Email { get; set; }
        public string Imei { get; set; }
        public string RefreshToken { get; set; }
    }
}
