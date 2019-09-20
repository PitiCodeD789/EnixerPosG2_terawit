using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Api.ViewModels.Shifts
{
    public class OpenShiftCommand
    {
        [Required]
        public decimal StartingCash { get; set; }
        public int UserId { get; set; }
    }
}
