using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class SetPasswordDtoCommand
    {
        [Required]
        [RegularExpression(@"^([A-Z0-9a-z]){20}$")]
        public string OTP { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z0-9a-z])+$")]
        public string Password { get; set; }
    }
}
