using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using Services;
using Services.Abstractions;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PaDbContext = Services.PaDbContext;

namespace PaDesktop.ViewModel
{
    public class PriceInputViewModel: ViewModelBase
    {
        private PaDbContext PaDb { get; set; }
        private ISubfieldService SubfieldService { get; set; }
        public EscallationInputDto EscallationInputDto { get; set; }
        public ObservableCollection<SubFieldViewModel> Subfields { get; set; } = new ObservableCollection<SubFieldViewModel>();
        public ObservableCollection<string> Fields { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<PriceInputRow> Rows { get; set; }
        public ICommand AddRowCommand { get; set; }
        public PriceInputViewModel(PaDbContext paDb, ISubfieldService subfieldService)
        {
            var calculator = App.Current.Services.GetService<IEscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = calculator.EscallationInputDto;
            PaDb = paDb;
            Rows = new ObservableCollection<PriceInputRow>();
            AddRowCommand = new RelayCommand(obj => AddDataGridRow());
            SubfieldService = subfieldService;
        }
        public async Task PopulateDataAsync()
        {
            var subfields = await SubfieldService.GetAllSubfieldsAsync();
            foreach (var subfield in subfields)
            {
                Subfields.Add(new SubFieldViewModel(subfield));
            }
            var fields = await SubfieldService.GetAllFieldsAsync();
            foreach (var field in fields)
            {
                Fields.Add(field);
            }
            await PriceInputRow.PopulateStaticDataAsync();
        }
        public void AddDataGridRow()
        {
            Rows.Add(new PriceInputRow());
        }
    }
}
