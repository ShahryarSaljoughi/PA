using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IIndexService
    {
        Task<double> GetIndexAsync(Guid SubfieldId, Guid TimeboxId);
    }
}
