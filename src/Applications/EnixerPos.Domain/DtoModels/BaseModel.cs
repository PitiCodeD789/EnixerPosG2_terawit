using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
