using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Domain.DtoModels.Auth
{
    public class CheckPinDtoCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(\d{4})$")]
        public string Pin { get; set; }
    }
}
