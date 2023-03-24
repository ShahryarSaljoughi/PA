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
        private List<TimeBox>? TimeBoxes = null;
        private PaDbContext db { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public TimeBoxService(PaDbContext _db)
        {
            db = _db;
        }
        public async Task<List<TimeBox>> GetAllTimeBoxesAsync()
        {
            if (TimeBoxes is not null) return TimeBoxes;
            using var db = new PaDbContext();
            var timeboxes = await db.TimeBoxes.ToListAsync();
            return timeboxes;
        }
        public async Task LoadAsync()
        {
            TimeBoxes = await db.TimeBoxes.ToListAsync();
        }

        public async Task SaveTimeboxAsync(TimeBox timeBox)
        {
            db.TimeBoxes.Add(timeBox);
            if (TimeBoxes != null) { TimeBoxes.Add(timeBox); }
            await db.SaveChangesAsync();
        }

        public async Task<TimeBox> GetLastNonInterimTimeboxAsync()
        {
            var result = (await GetAllTimeBoxesAsync())
                .Where(t => !t.IsInterim)
                .OrderByDescending(t => t.End)
                .First();
            return result;
        }
    }
}
