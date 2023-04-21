using ClosedXML.Excel;
using DataModel.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Excel
{
    public interface IExcelCalculationsSheetCreator
    {
        void WriteCalculationsSheet(Escalation escalation, XLWorkbook workbook);
    }
}
