using System;
using System.Collections.Generic;

namespace DataModel.Model
{
    public class TimeBox
    {
        public Guid Id { get; set; }
        public virtual int SolarYear { get; set; }  // چه سالی
        public virtual int ThreeMonthNo { get; set; }  // سه ماهه جندم
        public virtual string? Month { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual IList<PAIndex>? PAIndexes { get; set; }
        public string Text => ToString();

        public override string ToString()
        {
            var numToWord = new Dictionary<int, string>()
            {
                {1, "اول" },
                {2, "دوم" },
                {3, "سوم" },
                {4, "چهارم" },
            };
            return ThreeMonthNo > 0 ? 
                string.Join(" ", "سه‌ماهه ", numToWord[ThreeMonthNo], SolarYear) 
                : string.Join(" ", Month, SolarYear);
        }
    }
}