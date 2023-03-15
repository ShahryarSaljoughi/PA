using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;

using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace PaDesktop.ViewModel
{
    public class PriceInputRow : ViewModelBase
    {
        public static List<Subfield> AllSubFields { get; set; } = new List<Subfield>();
        public static ObservableCollection<string> AllFields { get; set; } = new ObservableCollection<string>();
        private string? selectedField;
        private Subfield? selectedSubfield;

        public string? SelectedField
        {
            get => selectedField;
            set
            {
                selectedField = value;
                RefillSubfields();
                OnPropertyChanged();
            }
        }
        public Subfield? SelectedSubfield
        {
            get => selectedSubfield; set
            {
                selectedSubfield = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Subfield> Subfields { get; set; } = new ObservableCollection<Subfield>();
        public decimal? PreviousPrice { get; set; }
        public decimal? CurrentPrice { get; set; }

        public static async Task PopulateStaticDataAsync()
        {
            var service = App.Current.Services.GetService<ISubfieldService>();
            var allSubFields = await service.GetAllSubfieldsAsync();
            foreach (var item in allSubFields)
            {
                AllSubFields.Add(item);
            }
            var allFields = await service.GetAllFieldsAsync();
            foreach (var item in allFields)
            {
                AllFields.Add(item);
            }
        }

        public void RefillSubfields()
        {
            if (string.IsNullOrWhiteSpace(SelectedField)) return;
            Subfields.Clear();
            Subfields.AddRange(
                AllSubFields
                .Where(s => s.Field == SelectedField)
                .OrderBy(s => s.Number)
                .ToList());
        }
    }
}
