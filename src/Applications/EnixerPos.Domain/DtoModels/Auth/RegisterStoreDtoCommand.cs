using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class RegisterStoreDtoCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z0-9a-z])+$")]
        public string StoreName { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$")]
        public string EWalletAccNo { get; set; }
    }
}
