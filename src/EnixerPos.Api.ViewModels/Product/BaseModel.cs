using System;

namespace EnixerPos.Api.ViewModels.Product
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}