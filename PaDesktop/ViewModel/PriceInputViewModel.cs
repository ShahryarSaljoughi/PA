using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using PaDesktop.Service;
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
using System.Collections.Specialized;
using PaDbContext = Services.PaDbContext;

namespace PaDesktop.ViewModel
{
    public class PriceInputViewModel : ViewModelBase
    {
        private PaDbContext PaDb { get; set; }
        private ISubfieldService SubfieldService { get; set; }
        private INavigationService NavigationService { get; set; }
        public EscallationInputDto EscallationInputDto { get; set; }
        public ObservableCollection<SubFieldViewModel> Subfields { get; set; } = new ObservableCollection<SubFieldViewModel>();
        public ObservableCollection<string> Fields { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<PriceInputRowViewModel> VMRows { get; set; }

        public ICommand AddRowCommand { get; set; }
        public ICommand GoNextPage { get; set; }
        public ICommand GoPreviousPage { get; set; }
        public PriceInputViewModel(PaDbContext paDb, ISubfieldService subfieldService, INavigationService navigationService)
        {
            var calculator = App.Current.Services.GetService<IEscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = calculator.EscalationInputDto;
            PaDb = paDb;
            VMRows = new ObservableCollection<PriceInputRowViewModel>();
            VMRows.CollectionChanged += VMRows_CollectionChanged;
            SubfieldService = subfieldService;
            this.NavigationService = navigationService;

            AddRowCommand = new RelayCommand(obj => AddDataGridRow());
            GoNextPage = new RelayCommand(obj =>
            {
                RemoveInValidRows(EscallationInputDto);
                NavigationService.Navigate<EscallationResultPageViewModel>();
            });
            GoPreviousPage = new RelayCommand(obj =>
            {
                this.NavigationService.Navigate<EscallationInputViewModel>();
            });

        }

        private void RemoveInValidRows(EscallationInputDto escallationInputDto)
        {
            var invalidRows = EscallationInputDto.Prices.Where(r => r.Subfield is null).ToList();
            if (!invalidRows.Any()) return;
            invalidRows.ForEach(row => EscallationInputDto.Prices.Remove(row));
            var invalidVMRows = VMRows.Where(vmr => invalidRows.Contains(vmr.RowDto)).ToList();
            invalidVMRows.ForEach(ir => VMRows.Remove(ir));
        }

        /// <summary>
        /// to keep in sync the view model rows and and model rows (DTOs) as the view model collection changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VMRows_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            var _event = e.Action;

            switch (_event)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var item in e.NewItems)
                    {
                        var row = ((PriceInputRowViewModel)item).RowDto;
                        EscallationInputDto.Prices.Add(row);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var item in e.OldItems)
                    {
                        var rowVM = (PriceInputRowViewModel)item;
                        if (!VMRows.Contains(rowVM)) EscallationInputDto.Prices.Remove(rowVM.RowDto);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        public async Task PopulateDataAsync()
        {
            if (!Subfields.Any())
            {
                var subfields = await SubfieldService.GetAllSubfieldsAsync();
                foreach (var subfield in subfields)
                {
                    Subfields.Add(new SubFieldViewModel(subfield));
                }
            }
            if (!Fields.Any())
            {
                var fields = await SubfieldService.GetAllFieldsAsync();
                foreach (var field in fields)
                {
                    Fields.Add(field);
                }
            }
            await PriceInputRowViewModel.PopulateStaticDataAsync();
        }
        public void AddDataGridRow()
        {
            VMRows.Add(new PriceInputRowViewModel());
        }

    }
}
