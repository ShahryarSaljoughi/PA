using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using PaDesktop.Core;
using Services.Abstractions;
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
