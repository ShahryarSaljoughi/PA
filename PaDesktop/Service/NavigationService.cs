using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.Service
{
    internal class NavigationService : INavigationService
    {
        public ViewModelBase CurrentPage { get; private set; }

        public event EventHandler Navigated;
        
        public void Navigate<T>([CallerMemberName] object? sender = null) where T : ViewModelBase
        {
            var viewModel = App.Current.Services.GetService<T>();
            CurrentPage = viewModel;
            Navigated.Invoke(sender, new EventArgs());
        }
    }
}
