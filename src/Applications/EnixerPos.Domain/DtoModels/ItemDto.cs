using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.Domain.DtoModels
{
    public class ItemDto : BaseDto
    {
        public string ItemName { get; set; }
        public string Color { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
    }
}
