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
        public IIndexService IndexService { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public TimeBoxService(PaDbContext _db, IIndexService indexService)
        {
            db = _db;
            IndexService = indexService;
            IndexService.IndexRemoved += OnIndexRemoved;
        }

        private void OnIndexRemoved(object? sender, PAIndex e)
        {
            var timebox = TimeBoxes?.FirstOrDefault(t => t.Id == e.TimeBoxId);
            if (timebox != null)
            {
                var indexes = timebox.PAIndexes;
                TimeBoxes?.First(t => t.Id == e.TimeBoxId).PAIndexes?.Remove(e);
            }
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

        public async Task DeleteTimeboxAsync(TimeBox? selectedTimeBox)
        {
            db.TimeBoxes.Remove(selectedTimeBox);
            await db.SaveChangesAsync();
            TimeBoxes?.Remove(selectedTimeBox);
        }
    }
}
