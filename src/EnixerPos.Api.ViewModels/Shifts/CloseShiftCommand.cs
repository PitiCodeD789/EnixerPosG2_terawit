using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnixerPos.Api.ViewModels.Shifts
{
    public class CloseShiftCommand
    {
        [Required]
        public int UserId { get; set; }
        public int ShiftId { get; set; }
    }
}
