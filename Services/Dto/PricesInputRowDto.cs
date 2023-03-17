using DataModel.Model;

namespace Services.Dto
{
    public class PricesInputRowDto
    {
        public Subfield? Subfield { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal CurrentPrice{ get; set; }
    }
}
