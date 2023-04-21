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
    public class EscallationInputViewModel : ValidatableViewModelBase
    {
        private IEscallationCalculator escallationCalculator;
        private INavigationService NavigationService;
        public ICommand GoToNextPageCommand { get; set; }
        public Task<TimeBox[]> LoadTimeBoxesTask;
        public ObservableCollection<TimeBox>? AllTimeBoxes { get; set; } = new();
        public List<double> Coefficients { get; } = new List<double>() { 0.9, 0.95, 0.975, 1 };
        public EscallationInputDto EscallationInputDto { get; }

        public EscallationInputViewModel(INavigationService navigationService)
        {
            escallationCalculator = App.Current.Services.GetService<IEscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = escallationCalculator.EscalationInputDto;
            NavigationService = navigationService;
            GoToNextPageCommand = new RelayCommand(obj => GoToNextPage());
            EscallationInputDto.PropertyChanged += EscallationInputDto_PropertyChanged;
        }

        private void EscallationInputDto_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CheckModelValidity();
        }

        public async Task PopulateDataAsync()
        {
            var service = App.Current.Services.GetService<ITimeBoxService>() ?? throw new Exception("IoC not working");
            var t = (await service.GetAllTimeBoxesAsync()).OrderByDescending(x => x.SolarYear).ThenBy(x => x.ThreeMonthNo);
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

        protected override bool CheckModelValidity()
        {
            var result = true;
            var rules = new List<(Predicate<EscallationInputDto> rule, string propertyName, string error)>()
            {
                (dto => dto.CurrentStatementTime is null
                , nameof(EscallationInputDto.CurrentStatementTime)
                , "تاریخ صورت وضعیتف فعلی را وارد کنید."),

                ((EscallationInputDto dto) => dto.PreviousStatementTime is null
                , nameof(EscallationInputDto.PreviousStatementTime)
                , "مقدار صورت وضعیت قبلی را وارد کنید."),

                ((EscallationInputDto dto)=> dto.CurrentStatementTime < dto.PreviousStatementTime,
                nameof(EscallationInputDto.CurrentStatementTime),
                "تاریخ صورت وضعیت فعلی، بعد از قبلی باید باشد."),

                ((EscallationInputDto dto)=> dto.Coefficient == default ,
                nameof(EscallationInputDto.Coefficient),
                "ضریب به کار رفته در ضریب تعدیل را وارد کنید.")
            };
            foreach (var rule in rules)
            {
                if (rule.rule(EscallationInputDto))
                {
                    AddError(rule.error, rule.propertyName);
                    result = false;
                }
                else ClearErrors(rule.propertyName);
            }
            IsFormValid = result;
            OnPropertyChanged(nameof(IsFormValid));
            return result;
        }
    }
}
