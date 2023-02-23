using DataModel.Model;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDesktop.ViewModel
{
    public class TimeBoxesList: ObservableCollection<TimeBox>
    {
        public TimeBoxesList()
        {
            var service = App.Current.Services.GetService<ITimeBoxService>();
            var loadingTask = service?.GetAllTimeBoxesAsync() ?? throw new Exception("IoC not working");
            loadingTask.ContinueWith(async t =>
            {
                var timeboxes = await t;
                var all = new List<TimeBox>(timeboxes);
                foreach (var item in all)
                {
                    Add(item);
                }
            });

        }
    }
}
