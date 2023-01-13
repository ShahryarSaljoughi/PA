using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAClient.ObjectModel
{
    public class PAIndex
    {
        public int Value { get; set; }
        public string Description { get; set; }
        public TimeBox TimeBox { get; set; }
        public Subfield Subfield { get; set; }

    }
}
