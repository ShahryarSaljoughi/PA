using DataModel.Model;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulateDbJob
{
    class PaDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=file:pa.sqlite;Mode=ReadWrite;");
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        public DbSet<TimeBox> TimeBoxes { get; set; }
        public DbSet<Subfield> Subfields { get; set; }
        public DbSet<PAIndex> PAIndexes { get; set; }
        public DbSet<EscalationItemRow> EscalationItemRows { get; set; }
        public DbSet<EscalationItem> EscalationItems { get; set; }
    }
}
