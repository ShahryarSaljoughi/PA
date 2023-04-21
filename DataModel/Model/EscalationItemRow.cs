using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Model
{
    public class EscalationItemRow
    {
        private decimal escalationPrice;

        public virtual Guid Id { get; set; }
        /// <summary>
        /// دوره کارکرد
        /// </summary>
        public virtual TimeBox? WorkingTimeBox { get; set; }

        /// <summary>
        /// نسبت دوره کارکرد
        /// </summary>
        public double WorkingProportion => GetWorkingProportion();
        public double WorkingProportionRounded => Math.Round(WorkingProportion, 4);
        /// <summary>
        /// شاخص دوره کارکرد
        /// </summary>
        public double WorkingTimeBoxIndex { get; set; }
        /// <summary>
        /// ضریب تعدیل
        /// </summary>
        public double EscalationCoefficient { get; set; }
        public double EscalationCoefficientRounded => Math.Round(EscalationCoefficient, 3);
        public decimal EscalationPrice { get => escalationPrice; set => escalationPrice = value; }
        public decimal EscalationPriceRounded => decimal.Round(escalationPrice, 4);
        public decimal WorkingPriceInCurrentTimebox => decimal.Round(
            EscalationItem.PriceDifference * (decimal)WorkingProportion, 4);
        public virtual EscalationItem EscalationItem { get; set; }

        public EscalationItemRow(EscalationItem escalationItem)
        {
            Id = Guid.NewGuid();
            EscalationItem = escalationItem;
        }
        /// <summary>
        /// Don't use this
        /// </summary>
        public EscalationItemRow()
        {

        }

        private double GetWorkingProportion()
        {
            var isLastDayIncludedInWhleDuration = this.EscalationItem.Escalation.IsCurrentStatementFinal;
            var isWorkingTimeBoxTheLastOne = WorkingTimeBox?.End.Date >= EscalationItem.Escalation.CurrentStatementTime?.Date;
            var isLastDayIncludedInWorkingTimeBox = this.EscalationItem.Escalation.IsCurrentStatementFinal && isWorkingTimeBoxTheLastOne || !isWorkingTimeBoxTheLastOne;
            var start = WorkingTimeBox?.Start > EscalationItem.Escalation.PreviousStatementTime
                ? WorkingTimeBox.Start
                : EscalationItem.Escalation.PreviousStatementTime;
            var end = WorkingTimeBox?.End < EscalationItem.Escalation.CurrentStatementTime
                ? WorkingTimeBox?.End
                : EscalationItem.Escalation.CurrentStatementTime;
            var workingDurationInTimebox = (end?.Date - start?.Date).Value.Days + (isLastDayIncludedInWorkingTimeBox ? 1 : 0);
            var overallWorkingDuration =
                (EscalationItem?.Escalation?.CurrentStatementTime - EscalationItem?.Escalation?.PreviousStatementTime)?.Days
                + (isLastDayIncludedInWhleDuration ? 1 : 0);
            var result =
                 (double)workingDurationInTimebox /
                overallWorkingDuration;
            var rounded = (double)Math.Round((double)result.Value, 4);
            return rounded;
        }

    }
}
