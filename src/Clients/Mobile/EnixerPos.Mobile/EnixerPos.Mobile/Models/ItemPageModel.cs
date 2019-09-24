using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.Models
{
    public class ItemPageModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string DataName { get; set; }
        public string CountItem { get; set; }
        public bool CountItemVisible { get; set; }
        public string Number { get; set; }
        public bool NumberVisible { get; set; }

    }
}
