using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using PaDesktop.Service;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaDesktop.ViewModel
{
    public class MainWindowViewModel : Core.ViewModelBase

    {
        private INavigationService NavigationService { get; set; }
        public string Hello { get; set; } = "Hello";
        public EscallationInputViewModel EscallationInputViewModel { get; set; }
        public IndexEditViewModel IndexEditViewModel { get; set; }
        public ICommand GoToEscallationInputPage { get; set; }
        public ICommand GoToIndexEditPage { get; set; }
        public ViewModelBase CurrentPage => NavigationService.CurrentPage;

        public MainWindowViewModel(IndexEditViewModel indexmVm, EscallationInputViewModel escallationInputVm, INavigationService navigationService)
        {
            NavigationService = navigationService;
            NavigationService.Navigated += OnNavigated;
            EscallationInputViewModel = escallationInputVm;
            IndexEditViewModel = indexmVm;
            GoToEscallationInputPage = new RelayCommand(obj =>
            {
                NavigationService.Navigate<EscallationInputViewModel>();
            });
            GoToIndexEditPage = new RelayCommand(obj =>
            {
                NavigationService.Navigate<IndexEditViewModel>();
            });
            NavigationService.Navigate<EscallationInputViewModel>();
        }

        private void OnNavigated(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(CurrentPage));
        }
    }
}
