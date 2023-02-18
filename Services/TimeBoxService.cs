using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;

namespace Services
{
    public class TimeBoxService : ITimeBoxService, INotifyPropertyChanged
    {
        private TimeBox[]? timeBoxes = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task<TimeBox[]> GetAllTimeBoxesAsync()
        {
            if (timeBoxes is not null) return timeBoxes;
            using var db = new PaDbContext();
            var timeboxes = await db.TimeBoxes.ToArrayAsync();
            return timeboxes;
        }
    }
}
