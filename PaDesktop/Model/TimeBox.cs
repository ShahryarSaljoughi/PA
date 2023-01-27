using System;
using System.Collections.Generic;

namespace PAClient.ObjectModel
{
    public class TimeBox
    {
        public int SolarYear { get; set; }  // چه سالی
        public int ThreeMonthNo { get; set; }  // سه ماهه جندم
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<PAIndex> PAIndexes { get; set; }
    }
}