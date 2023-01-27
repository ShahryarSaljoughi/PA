using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAClient.ObjectModel
{
    public class EscalationItem
    {
        public IList<EscalationItemRow> Rows { get; set; }
        /// <summary>
        /// مبلغ صورت وضعیت قبلی
        /// </summary>
        public decimal PreviousPrice { get; set; }
        /// <summary>
        /// مبلغ صورت وضعیت فعلی
        /// </summary>
        public decimal CurrentPrice { get; set; }
        public Subfield Subfield { get; set; }
        public decimal PriceDifference => CurrentPrice - PreviousPrice;

    }
}
