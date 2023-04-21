﻿using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Excel
{
    public interface IExcelExportService
    {
        Task ExportAsync(string path, Escalation escalation);
    }
}