using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class EscallationCalculator : IEscallationCalculator
    {
        public PaDbContext Db { get; set; }
        public EscallationCalculator(PaDbContext db)
        {
            Db = db;
        }
        public async Task CalculateAsync(EscallationInputDto dto)
        {

        }

        private async Task<IEnumerable<TimeBox>> GetWorkingTimeBoxes(DateTime first, DateTime second)
        {
            var result = await Db.TimeBoxes
                .Where(t => t.End > first && t.Start < second)
                .ToListAsync();

            return result;
        }
    }
}
