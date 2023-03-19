using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
//using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.Model
{
    public class EscalationItem
    {
        public Guid Id { get; private set; }
        public virtual IList<EscalationItemRow>? Rows { get; set; }
        /// <summary>
        /// مبلغ صورت وضعیت قبلی
        /// </summary>
        public virtual decimal PreviousPrice { get; set; } = default;
        /// <summary>
        /// مبلغ صورت وضعیت فعلی
        /// </summary>
        public virtual decimal CurrentPrice { get; set; } = default;
        public virtual Subfield? Subfield { get; set; }
        public virtual decimal PriceDifference => CurrentPrice - PreviousPrice;
        public virtual Guid EscalationId { get; set; }
        public virtual Escalation Escalation {get; private set;}
        public virtual double BaseIndex { get; set; }

        public EscalationItem(Escalation escallation)
        {
            Id = Guid.NewGuid();
            Rows = new List<EscalationItemRow>();
            Escalation = escallation;
        }
        /// <summary>
        /// don't use this
        /// </summary>
        public EscalationItem()
        {

        }
    }
}
