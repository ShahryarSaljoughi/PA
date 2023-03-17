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
        public double Coefficient { get; set; } // 0.95 or 0.975 or 1
        public DateTime? PreviousStatementTime { get; set; }
        public DateTime? CurrentStateMentTime { get; set; }
        public DateTime? LandSurrenderTime { get; set; }
        public List<PricesInputRowDto> Prices { get; } = new List<PricesInputRowDto> { };
    }
}
