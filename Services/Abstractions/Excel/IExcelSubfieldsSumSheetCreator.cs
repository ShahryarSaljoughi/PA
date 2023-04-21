using ClosedXML.Excel;
using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Excel
{
    public interface IExcelSubfieldsSumSheetCreator
    {
        void WriteSumsSheet(Escalation escalation, XLWorkbook workbook);
    }
}
