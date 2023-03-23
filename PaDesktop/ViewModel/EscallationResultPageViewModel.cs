using DataModel.Model;
using PaDesktop.Core;
using PaDesktop.Service;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PaDesktop.ViewModel
{
    public class EscallationResultPageViewModel: ViewModelBase
    {
        private IEscallationCalculator Calculator { get; set; }
        private INavigationService NavigationService { get; set; }
        public IExcelExportService ExcelService { get; }
        public Escalation? Escalation { get; private set; }
        public ObservableCollection<EscalationItemRow> Rows { get; set; } = new ObservableCollection<EscalationItemRow>();
        public RelayCommand GoPreviousPage { get; set; }
        //public RelayCommand ExportExcelOutput { get; set; }
        public EscallationResultPageViewModel(IEscallationCalculator calculator, INavigationService nav, IExcelExportService excelService) 
        { 
            Calculator = calculator;
            NavigationService = nav;
            ExcelService = excelService;
            GoPreviousPage = new RelayCommand(obj => NavigationService.Navigate<PriceInputViewModel>());
            //ExportExcelOutput = new RelayCommand(async obj => {
            //    await ExportExcel();
            //});
        }

        public async Task CalculateAsync()
        {
            Escalation = await Calculator.CalculateAsync();
            var rows = Escalation.Items.SelectMany(i => i.Rows).ToList();
            this.Rows.AddRange(rows);
        }
        public async Task ExportExcel(string path)
        {
            await ExcelService.ExportAsync(path, Escalation);
        }
    }
}
