using ClosedXML.Excel;
using DataModel.Model;
using DocumentFormat.OpenXml.Wordprocessing;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExcelExportService : IExcelExportService
    {
        //https://wpf-tutorial.com/dialogs/the-savefiledialog/
        public async Task ExportAsync(string path, Escalation escalation)
        {
            var usedRowsUpTo = 1;
            using var workbook = new XLWorkbook();
            var calculationsSheet = workbook.Worksheets.Add("محاسبات تعدیل");
            calculationsSheet.SetRightToLeft(true);
            WriteHeaders(calculationsSheet);
            //var fields = escalation.Items.GroupBy(i => i.Subfield?.Field).ToArray();
            //var startingRow = 1;
            //for (int i = 0; i < fields.Count(); i++)
            //{
            //    var rows = fields[0].ToArray();
            //    usedRowsUpTo = await WriteRowsOfField(calculationsSheet, startingRow, rows);
            //    startingRow = usedRowsUpTo + 1;
            //}
            SaveFileAsync(workbook, path);
        }
        private void WriteHeaders(IXLWorksheet sheet)
        {
            sheet.Cell("A1").Value = "دوره کارکرد";
            sheet.Range("A1:B2").Row(1).Merge();
        }
        /// <summary>
        /// returns row number up to where has been used
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="startingRow"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        private async Task<int> WriteRowsOfField(IXLWorksheet sheet, int startingRow, EscalationItem[] rows)
        {
            throw new NotImplementedException();
        }

        private void SaveFileAsync(XLWorkbook book, string path)
        {
            //TODO: 
            book.SaveAs(file: path);
        }
    }
}
