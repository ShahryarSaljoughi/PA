using DataModel.Model;

namespace Services.Dto
{
    public class PricesInputDto
    {
        public Subfield? Subfield { get; set; }
        public decimal Price { get; set; }
    }
}
