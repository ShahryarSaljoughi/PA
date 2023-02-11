using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;
namespace DbMigratorJob
{
    internal class MigratorDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=../database/pa.sqlite;");
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        public DbSet<TimeBox> TimeBoxes { get; set; }
        public DbSet<Subfield> Subfields { get; set; }
        public DbSet<PAIndex> PAIndexes { get; set; }
        public DbSet<EscalationItemRow> EscalationItemRows { get; set; }
        public DbSet<EscalationItem> EscalationItems { get; set; }
    }
}
