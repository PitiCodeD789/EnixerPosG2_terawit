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
        [Required]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string StoreEmail { get; set; }
        [Required]
        public string PosImei { get; set; }
        [Required]
        
        public int PosUserId { get; set; }
    }
}
