using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class GetTokenByRefreshUserCommand
    {
        [Required]
        [RegularExpression(@"^([A-Z0-9a-z]{10})$")]
        public string RefreshToken { get; set; }
        [Required]
        [RegularExpression(@"^([A-Za-z]+)$")]
        public string User { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
