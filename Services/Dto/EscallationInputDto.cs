using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dto
{
    public class EscallationInputDto
    {
        public TimeBox? BaseTimeBox { get; set; }
        public decimal Coefficient { get; set; } // 0.95 or 0.975 or 1
        public PersianDate? PreviousStatementTime { get; set; }
        public PersianDate? CurrentStateMentTime { get; set; }
        public PersianDate? LandSurrenderTime { get; set; }
        public List<PricesInputDto>? PreviousPrices { get; set; }
        public List<PricesInputDto>? CurrentPrices { get; set; }
    }

    public class PricesInputDto
    {
        public Subfield? Subfield { get; set; }
        public decimal Price { get; set; }
    }
}
