using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IndexService : IIndexService
    {
        private PaDbContext Db {get; set;}
        public IndexService(PaDbContext db)
        {
            Db = db;
        }
        public async Task<double> GetIndexAsync(Guid subfieldId, Guid timeboxId)
        {
            var result = await Db.Set<PAIndex>()
                .Where(i => i.SubfieldId == subfieldId && i.TimeBoxId == timeboxId)
                .Select(i => i.Value)
                .FirstOrDefaultAsync();
            return result;
            
        }
    }
}
