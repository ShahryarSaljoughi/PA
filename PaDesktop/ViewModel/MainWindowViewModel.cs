using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
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
    public class MainWindowViewModel : ObservableObject

    {
        public string Hello { get; set; } = "Hello";
        private object _currentPage;
        public EscallationInputViewModel EscallationInputViewModel { get; set; }
        public IndexEditViewModel IndexEditViewModel { get; set; }
        public ICommand GoToEscallationInputPage { get; set; }
        public ICommand GoToIndexEditPage { get; set; }

        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(IndexEditViewModel indexmVm, EscallationInputViewModel escallationInputVm)
        {
            EscallationInputViewModel = escallationInputVm;
            IndexEditViewModel = indexmVm;
            GoToEscallationInputPage = new RelayCommand(obj =>
            {
                CurrentPage = EscallationInputViewModel;
            });
            GoToIndexEditPage = new RelayCommand(obj =>
            {
                CurrentPage = IndexEditViewModel;
            });
            CurrentPage = EscallationInputViewModel;
        }
    }
}
