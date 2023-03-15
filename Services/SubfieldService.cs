using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SubfieldService : ISubfieldService
    {
        private PaDbContext Db { get; set; }
        private List<string > Fields { get; set; }
        private Dictionary<(string Field, string SubfieldNo), Subfield>? Subfields { get; set; }
        public SubfieldService(PaDbContext db)
        {
            Db = db;
        }
        public async Task<List<Subfield>> GetAllSubfieldsAsync()
        {
            if (Subfields == null || !Subfields.Any()) await PopulateSubfields();
            return Subfields.Values.ToList();
        }
        public async Task<Subfield> GetSubfieldAsync(string field, string subfieldNo)
        {
            Subfields.TryGetValue((field, subfieldNo), out var result);
            if (result is not null) return result;
            await PopulateSubfields();
            return await GetSubfieldAsync(field, subfieldNo);
        }
        public async Task<List<string>> GetAllFieldsAsync()
        {
            if (Fields is null || !Fields.Any()) PopulateFieldsAsync();
            return Fields;
        }

        private async Task PopulateFieldsAsync()
        {
            var subfields = await GetAllSubfieldsAsync();
            Fields = subfields.Select(x => x.Field).Distinct().ToList();
        }

        private async Task PopulateSubfields()
        {
            var subfields = await Db.Set<Subfield>().ToListAsync();
            Subfields = subfields.ToDictionary(subfield => (subfield.Field, subfield.Number));
        }
    }
}
