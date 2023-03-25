using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IIndexService
    {
        event EventHandler<PAIndex> IndexRemoved;

        Task DeleteIndexAsync(PAIndex selectedIndex);
        Task<double> GetIndexAsync(Guid SubfieldId, Guid TimeboxId);
        Task SaveIndexesAsync(List<PAIndex> paIndices);
        Task SaveNewIndex(PAIndex index);
    }
}
