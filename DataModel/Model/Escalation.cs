using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// علی الحساب است؟ یا قطعی است؟
        /// </summary>
        public virtual bool? IsInterim { get; set; } = false;
        public bool IsCurrentStatementFinal { get; set; } = false; // صورت وضعیت نهایی
        public string Contractor { get; set; } = string.Empty; // پیمانکار
        public string Employer { get; set; } = string.Empty; // کارفرما
        public string ProjectTitle { get; set; } = string.Empty;
        public DateTime? ContractStartDateTime { get; set; }
        public decimal GetSumOfField(string? Field)
        {
            if (string.IsNullOrWhiteSpace(Field)) return 0;
            var result = Items.Where(i => i.Subfield?.Field == Field).SelectMany(i => i.Rows).Sum(r => r.EscalationPrice);
            return result;
        }
    }
}
