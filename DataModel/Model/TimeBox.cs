using System;
using System.Collections.Generic;

namespace DataModel.Model
{
    public class TimeBox : IComparable<TimeBox>, IComparable
    {
        public Guid Id { get; set; }
        public virtual int SolarYear { get; set; }  // چه سالی
        public virtual int ThreeMonthNo { get; set; }  // سه ماهه جندم
        public virtual string? Month { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public int Duration => (End - Start).Days;
        public virtual IList<PAIndex>? PAIndexes { get; set; }
        public virtual bool IsInterim { get; set; } = false;
        public string Text => ToString();
        public TimeBox()
        {
            Id = Guid.NewGuid();
        }
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

        public int CompareTo(TimeBox other)
        {
            return End.CompareTo(other.End);
        }

        public int CompareTo(object obj)
        {
            var other = (TimeBox)obj;
            return CompareTo(other);
        }
    }
}