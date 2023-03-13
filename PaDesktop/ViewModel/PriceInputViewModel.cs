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
        public EscallationInputDto EscallationInputDto { get; set; }
        public ObservableCollection<Subfield> Subfields { get; set; } = new ObservableCollection<Subfield>();
        public ObservableCollection<PriceInputRow> Rows { get; set; }
        public ICommand AddRowCommand { get; set; }
        public PriceInputViewModel(PaDbContext paDb)
        {
            var calculator = App.Current.Services.GetService<IEscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = calculator.EscallationInputDto;
            PaDb = paDb;
            Rows = new ObservableCollection<PriceInputRow>();
            AddRowCommand = new RelayCommand(obj => AddRow());
        }
        public async Task PopulateDataAsync()
        {
            var subfields = await PaDb.Set<Subfield>().Take(10).ToListAsync();
            foreach (var subfield in subfields)
            {
                Subfields.Add(subfield);
            }
        }
        public void AddRow()
        {
            Rows.Add(new PriceInputRow());
        }
    }
}
