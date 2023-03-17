using PaDesktop.Core;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class EscallationResultPageViewModel: ViewModelBase
    {
        private IEscallationCalculator Calculator { get; set; }
        public EscallationResultPageViewModel(IEscallationCalculator calculator) 
        { 
            Calculator = calculator;
        }

        void Calculate()
        {
            
        }
    }
}
