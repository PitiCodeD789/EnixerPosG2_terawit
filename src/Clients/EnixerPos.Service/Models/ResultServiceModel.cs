using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EnixerPos.Service.Models
{
    public class ResultServiceModel<T> where T : class
    {
        public HttpStatusCode IsError { get; set; }
        public T Model { get; set; }
    }
}
