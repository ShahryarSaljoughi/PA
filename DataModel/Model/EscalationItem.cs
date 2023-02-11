using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.Model
{
    public class EscalationItem
    {
        public Guid Id { get; set; }
        public virtual IList<EscalationItemRow>? Rows { get; set; }
        /// <summary>
        /// مبلغ صورت وضعیت قبلی
        /// </summary>
        public virtual decimal PreviousPrice { get; set; }
        /// <summary>
        /// مبلغ صورت وضعیت فعلی
        /// </summary>
        public virtual decimal CurrentPrice { get; set; }
        public virtual Subfield? Subfield { get; set; }
        public virtual decimal PriceDifference => CurrentPrice - PreviousPrice;

    }
}
