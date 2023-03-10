using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using PaDesktop.Service;
using Services;
using Services.Abstractions;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaDesktop.ViewModel
{
    public class EscallationInputViewModel: Core.ViewModelBase
    {
        private IEscallationCalculator escallationCalculator;
        private INavigationService NavigationService;
        public ICommand GoToNextPageCommand { get; set; }
        public Task<TimeBox[]> LoadTimeBoxesTask;
        public ObservableCollection<TimeBox>? AllTimeBoxes { get; set; } = new();
        public List<double> Coefficients { get; } = new List<double>() { 0.9, 0.95, 0.975, 1};
        public EscallationInputDto EscallationInputDto { get; }
        
        public EscallationInputViewModel(INavigationService navigationService)
        {
            escallationCalculator = App.Current.Services.GetService<IEscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = escallationCalculator.EscallationInputDto;
            NavigationService = navigationService;
            GoToNextPageCommand = new RelayCommand(obj => GoToNextPage());
        }

        public async Task PopulateDataAsync()
        {
            var service = App.Current.Services.GetService<ITimeBoxService>() ?? throw new Exception("IoC not working");
            var t =  (await service.GetAllTimeBoxesAsync()).OrderByDescending(x => x.SolarYear).ThenBy(x => x.ThreeMonthNo);
            foreach (var item in t)
            {
                AllTimeBoxes?.Add(item);
            }
            OnPropertyChanged();
        }

        public void GoToNextPage()
        {
            this.NavigationService.Navigate<PriceInputViewModel>();
        }

    }
}
