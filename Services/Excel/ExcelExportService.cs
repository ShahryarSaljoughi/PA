using ClosedXML.Excel;
using DataModel.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

using Services.Abstractions.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Excel
{
    public class ExcelExportService : IExcelExportService
    {
        public IExcelCalculationsSheetCreator CalculationsWriter { get; }
        public IExcelCoverSheetCreator CoverWriter { get; }
        public IExcelSubfieldsSumSheetCreator SumSheetCreator { get; }

        public ExcelExportService(
            IExcelCalculationsSheetCreator calculattionsWriter, 
            IExcelCoverSheetCreator coverWriter,
            IExcelSubfieldsSumSheetCreator sumSheetCreator)
        {
            CalculationsWriter = calculattionsWriter;
            CoverWriter = coverWriter;
            SumSheetCreator = sumSheetCreator;
        }
        //https://wpf-tutorial.com/dialogs/the-savefiledialog/
        public async Task ExportAsync(string path, Escalation escalation)
        {
            using var workbook = new XLWorkbook();
            CalculationsWriter.WriteCalculationsSheet(escalation, workbook);
            CoverWriter.WriteCoverSheet(escalation, workbook);
            SumSheetCreator.WriteSumsSheet(escalation, workbook);
            SaveFileAsync(workbook, path);
        }

        private void SaveFileAsync(XLWorkbook book, string path)
        {
            if (!path.EndsWith("xlsx")) { path += ".xlsx"; }
            book.SaveAs(file: path);
        }
    }
}
