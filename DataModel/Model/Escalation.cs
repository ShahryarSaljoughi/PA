using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.Model
{
    public class Escalation
    {
        public virtual Guid Id { get; set; }
        public virtual double Coefficient { get; set; }
        public virtual TimeBox? BaseTimeBox { get; set; }
        public virtual DateTime? PreviousStatementTime { get; set; }
        public virtual DateTime? CurrentStatementTime { get; set; }
        public virtual List<EscalationItem> Items { get; set; } = new List<EscalationItem>();
        public virtual bool IsCalculated { get; set; } = false;
    }
}
