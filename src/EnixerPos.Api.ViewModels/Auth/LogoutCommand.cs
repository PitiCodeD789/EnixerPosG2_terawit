using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Api.ViewModels.Auth
{
    public class LogoutCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
