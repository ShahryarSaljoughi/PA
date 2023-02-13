using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dto
{
    public class InputDto
    {
        public TimeBox? BaseTimeBox { get; set; }
        public decimal Coefficient { get; set; } // 0.95 or 0.975 or 1
        public PersianDate? PreviousStatementTime { get; set; }
        public PersianDate? CurrentStateMentTime { get; set; }




    }

    public class PersianDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
