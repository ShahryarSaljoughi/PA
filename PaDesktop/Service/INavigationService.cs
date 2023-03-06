using PaDesktop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.Service
{
    public interface INavigationService
    {
        void Navigate<T>([CallerMemberName] object? sender = null) where T : ViewModelBase;
        ViewModelBase CurrentPage { get; }
        event EventHandler Navigated;
    }
}
