using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginByPinViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string User { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
    }
}
