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
        public DateTime? CurrentStatementTime { get; set; }
        public DateTime? LandSurrenderTime { get; set; }
        public bool IsCurrentStatementFinal { get; set; } // صورت وضعیت نهایی
        public string Contractor { get; set; } = string.Empty; // پیمانکار
        public string Employer { get; set; } = string.Empty; // کارفرما
        public DateTime? ContractStartDateTime { get; set; } 
        public List<PricesInputRowDto> Prices { get; } = new List<PricesInputRowDto> { };
    }
}
