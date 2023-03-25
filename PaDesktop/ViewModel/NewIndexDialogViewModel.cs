using DataModel.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
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
    public class NewIndexDialogViewModel : ViewModelBase
    {
        private string? field;
        public string? Field
        {
            get => field;
            set
            {
                field = value;
                OnPropertyChanged();
                RefillSubFields();
            }
        }

        public List<string> AllFields { get; set; }
        public ObservableCollection<Subfield> Subfields { get; set; } = new ObservableCollection<Subfield>();
        public List<Subfield> AllSubfields { get; set; } = new List<Subfield>();
        public Subfield? Subfield { get; set; }
        public double Value { get; set; }
        public TimeBox TimeBox { get; set; }
        public ISubfieldService SubfieldService { get; }

        public string Description => string.Join(" ", "افزودن شاخص به دوره", TimeBox?.Text);

        public NewIndexDialogViewModel(TimeBox timebox)
        {
            TimeBox = timebox;
            SubfieldService = App.Current.Services.GetService<ISubfieldService>() ?? throw new Exception("IoC not working");
        }

        public async Task InitializeAsync()
        {
            AllSubfields = await SubfieldService.GetAllSubfieldsAsync();
            AllFields = (await SubfieldService.GetAllFieldsAsync()).OrderBy(x => x).ToList();
            OnPropertyChanged(nameof(AllSubfields));
            OnPropertyChanged(nameof(AllFields));
        }

        public PAIndex? CreateIndex()
        {
            if (TimeBox is null || Subfield is null) return null;
            var result = new PAIndex(TimeBox, Subfield)
            {
                Value = Value,
                SubfieldId = Subfield.Id,
                TimeBoxId = TimeBox.Id
            };
            return result;
        }
        private void RefillSubFields()
        {
            if (string.IsNullOrWhiteSpace(Field)) return;
            Subfields.Clear();
            Subfields.AddRange(
            AllSubfields
                .Where(s => s.Field == Field)
                .OrderBy(s => s.Number)
                .ToList());
        }

    }
}
