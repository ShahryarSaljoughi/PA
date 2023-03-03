using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using Services.Abstractions;
using Services.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class EscallationInputViewModel: ObservableObject
    {
        public Task<TimeBox[]> LoadTimeBoxesTask;
        public ObservableCollection<TimeBox>? AllTimeBoxes;
        public List<double> Coefficients { get; } = new List<double>() { 0.9, 0.95, 0.975, 1};
        public EscallationInputDto escallationInputDto { get; } = new EscallationInputDto();
        public EscallationInputViewModel()
        {
            var service = App.Current.Services.GetService<ITimeBoxService>();
            LoadTimeBoxesTask = service?.GetAllTimeBoxesAsync() ?? throw new Exception("IoC not working");
            LoadTimeBoxesTask.ContinueWith(async t =>
            {
                var timeboxes = await t;
                AllTimeBoxes = new ObservableCollection<TimeBox>(timeboxes);
                OnPropertyChanged();
            });

        }
    }
}
