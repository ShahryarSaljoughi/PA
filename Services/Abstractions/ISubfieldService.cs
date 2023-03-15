using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ISubfieldService
    {
        Task<List<string>> GetAllFieldsAsync();
        Task<List<Subfield>> GetAllSubfieldsAsync();
        Task<Subfield> GetSubfieldAsync(string field, string subfieldNo);
    }
}
