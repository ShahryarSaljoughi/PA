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
        private TimeBox[]? TimeBoxes = null;
        private PaDbContext db { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public TimeBoxService(PaDbContext _db)
        {
            db = _db;
        }
        public async Task<TimeBox[]> GetAllTimeBoxesAsync()
        {
            if (TimeBoxes is not null) return TimeBoxes;
            using var db = new PaDbContext();
            var timeboxes = await db.TimeBoxes.ToArrayAsync();
            return timeboxes;
        }
        public async Task LoadAsync()
        {
            TimeBoxes = await db.TimeBoxes.ToArrayAsync();
        }
    }
}
