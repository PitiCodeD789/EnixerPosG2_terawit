﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.Domain.DtoModels
{
    public class ItemDto : BaseDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Option1 { get; set; }
        public decimal Option1Price { get; set; }
        public string Option2 { get; set; }
        public decimal Option2Price { get; set; }
        public string Option3 { get; set; }
        public decimal Option3Price { get; set; }
        public string Option4 { get; set; }
        public decimal Option4Price { get; set; }
        public string Option5 { get; set; }
        public decimal Option5Price { get; set; }
        public string Option6 { get; set; }
        public decimal Option6Price { get; set; }
        public string Color { get; set; }
        public int StoreId { get; set; }
    }
}
