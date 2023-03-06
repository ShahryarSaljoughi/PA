using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class PriceInputViewModel
    {
        public EscallationInputDto EscallationInputDto { get; set; }
        public PriceInputViewModel()
        {
            var calculator = App.Current.Services.GetService<EscallationCalculator>() ?? throw new Exception("IoC not working");
            EscallationInputDto = calculator.EscallationInputDto;
        }
    }
}
