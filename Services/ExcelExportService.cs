using DataModel.Model;
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
        public Task Export(string path, Escalation escalation)
        {
            throw new NotImplementedException();
        }
    }
}
