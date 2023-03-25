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
        private PaDbContext Db { get; set; }
        public event EventHandler<PAIndex> IndexRemoved;
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

        public async Task SaveIndexesAsync(IQueryable<PAIndex> paIndices)
        {
            Db.Set<PAIndex>().AddRange(paIndices);
            await Db.SaveChangesAsync();
        }

        public async Task DeleteIndexAsync(PAIndex index)
        {
            Db.Set<PAIndex>().Remove(index);
            await Db.SaveChangesAsync();
            IndexRemoved(this, index);
        }

        public async Task SaveIndexesAsync(List<PAIndex> paIndices)
        {
            Db.Set<PAIndex>().AttachRange(paIndices);
            paIndices.ForEach(async i =>
            {
                var itemExists = await Db.Set<PAIndex>().AnyAsync(t => t.Id == i.Id);
                Db.Entry(i).State = itemExists ? EntityState.Modified : EntityState.Added;
            });
            await Db.SaveChangesAsync();
        }
    }
}
