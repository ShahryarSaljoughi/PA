using Castle.Core.Logging;
using ClosedXML.Excel;
using DataModel.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PopulateDbJob
{
    internal class Populator
    {
        private ILogger<Populator> Logger;
        public PaDbContext Db { get; set; }
        private IList<TimeBox> TimeBoxes = new List<TimeBox>();
        private IDictionary<Guid, Subfield> Subfields = new Dictionary<Guid, Subfield>();
        public async Task PopulateAsync()
        {
            IList<RawRow> rawrows;
            try
            {
                rawrows = ExtractRawData();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "ExtractRawData");
                throw;
            }

            TimeBoxes =
                (from row in rawrows
                 group row by new { row.Year, row.TimeBoxNo, row.TimeBoxStr } into g
                 let monthNo = g.Key.TimeBoxNo.HasValue ? 1 + 3*(g.Key.TimeBoxNo.Value - 1) : 0
                 let start = DateUtil.ToGregorian(g.Key.Year, monthNo, 1, monthStr: g.Key.TimeBoxStr)
                 let TimeBoxDuration = g.Key.TimeBoxNo.HasValue ? 3 : 1 
                 let end = start.AddPersianMonths(TimeBoxDuration)
                 select new TimeBox()
                 {
                     Id = Guid.NewGuid(),
                     ThreeMonthNo = g.Key.TimeBoxNo ?? 0,
                     SolarYear = g.Key.Year,
                     Start = start,
                     End = end,
                     Month = g.Key.TimeBoxStr,
                     PAIndexes = g.ToList().Select(row => new PAIndex() 
                     { 
                         Id = Guid.NewGuid(),
                         Subfield = new Subfield() 
                         { 
                             Id= Guid.NewGuid(), 
                             Field = row.Reshte, 
                             Number = row.FaslNo,
                             Title = row.FaslTitle
                         },
                         Value = row.Index
                     }).ToList()
                 }).ToList();
            Db.TimeBoxes.AddRange(TimeBoxes);
            await Db.SaveChangesAsync();
        }

        private static IList<RawRow> ExtractRawData()
        {
            var file = new XLWorkbook("shahkes 81-1401-end.xlsx");
            var rows = file.Worksheet(1).RangeUsed().RowsUsed().Skip(2).ToList();
            var rowsNo = rows.Count;
            var rawrows = new List<RawRow>();
            foreach (var row in rows)
            {
                var doreText =
                    int.TryParse(row.Cell("F").GetValue<string>(), out var doreNo)
                    ? ""
                    : row.Cell("F").GetValue<string>();
                RawRow r = new RawRow()
                {
                    Year = row.Cell("A").GetValue<int>(),
                    Raste = row.Cell("B").GetValue<string>(),
                    Reshte = row.Cell("C").GetValue<string>(),
                    FaslNo = row.Cell("D").GetValue<string>(),
                    FaslTitle = row.Cell("E").GetValue<string>(),
                    TimeBoxNo = doreNo,
                    TimeBoxStr = doreText,
                    Index = row.Cell("G").GetValue<double>(),

                };
                rawrows.Add(r);
            }

            return rawrows;
        }

        public Populator(PaDbContext dbContext, ILogger<Populator> logger)
        {
            this.Logger = logger;
            Db = dbContext;
        }

        private struct RawRow
        {
            public int Year { get; init; }
            public string Raste { get; init; }
            public string Reshte { get; init; }
            public string FaslNo { get; init; }
            public string FaslTitle { get; init; }
            public int? TimeBoxNo { get; init; }
            public string TimeBoxStr { get; init; }
            public double Index { get; init; }

        }
    }
}
