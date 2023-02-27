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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaDesktop.ViewModel
{
    public class MainWindowViewModel : ObservableObject

    {
        private object _currentPage;
        public EscallationInputViewModel EscallationInputViewModel { get; set; } = new EscallationInputViewModel();
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

        public MainWindowViewModel()
        {
            //_currentPage = EscallationInputViewModel;
            GoToEscallationInputPage = new RelayCommand(obj =>
            {
                CurrentPage = EscallationInputViewModel;
            });
            GoToIndexEditPage = new RelayCommand(obj =>
            {
                CurrentPage = EscallationInputViewModel;
            });
        }
    }
}
