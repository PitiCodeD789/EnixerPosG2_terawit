using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LoginByPinCommand
    {
        [Required]
        [RegularExpression(@"^(\d{4})$")]
        public string Pin { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z0-9a-z]{10})$")]
        public string RefreshToken { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
