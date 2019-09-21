using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.DtoModels
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int StoreId { get; set; }
    }
}
