using DataModel.Model;

namespace Services.Dto
{
    public class PriceInputRow
    {
        public Subfield? Subfield { get; set; }
        public decimal PreviousPrice { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
