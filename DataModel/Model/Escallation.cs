using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Model
{
    public class Escallation
    {
        public decimal Coefficient { get; set; }
        public TimeBox? BaseTimeBox { get; set; }
        public DateTime? PreviousStatementTime { get; set; }
        public DateTime? CurrentStateMentTime { get; set; }
    }
}
