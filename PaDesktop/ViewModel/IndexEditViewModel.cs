using DataModel.Model;
using Mohsen.PersianDateControls;
using PaDesktop.Core;
using Services;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utilities;

namespace PaDesktop.ViewModel
{
    public class IndexEditViewModel : ViewModelBase
    {
        public ObservableCollection<TimeBox> TimeBoxes { get; set; } = new ObservableCollection<TimeBox>();
        public TimeBox? SelectedTimeBox { get; set; }
        public ITimeBoxService TimeBoxService { get; }
        public ObservableCollection<PAIndex> Indexes { get; set; } = new ObservableCollection<PAIndex>();
        public string IndexGroupHeader => string.Join(" ", "شاخص‌ها", SelectedTimeBox?.Text);
        public ICommand RemoveTimeboxCommand { get; set; }
        public ICommand NewTimeboxCommand { get; set; }
        public ICommand SaveTimeboxCommand { get; set; }
        public IndexEditViewModel(ITimeBoxService timeBoxService)
        {
            TimeBoxService = timeBoxService;
        }

        public async Task PopulateDataAsync()
        {
            var data = await TimeBoxService.GetAllTimeBoxesAsync();
            TimeBoxes.AddRange(data
                .OrderByDescending(x => x.SolarYear)
                .ThenBy(x => x.ThreeMonthNo)
                .ToList());
        }

        public void PopulateIndexes()
        {
            Indexes.Clear();
            Indexes.AddRange(SelectedTimeBox?.PAIndexes?
                .OrderBy(i => i.Subfield.Field)
                .ThenBy(i => i.Subfield.Number)
                .ToList());
        }

        internal async Task SaveTimeBoxAsync(NewTimeboxDialogViewModel timeboxVM)
        {
            var timebox = new TimeBox()
            {
                IsInterim = timeboxVM.IsInterim,
                SolarYear = timeboxVM.SolarYear,
                ThreeMonthNo = timeboxVM.ThreeMonthNo,
            };
            if (timebox.IsInterim)
            {
                SetStartEndDates(timebox);
                await SetInterimIndexesAsync(timebox);
                
            }
            await TimeBoxService.SaveTimeboxAsync(timebox);
            TimeBoxes.Add(timebox);
            TimeBoxes.SortDescending();
        }

        private void SetStartEndDates(TimeBox timebox)
        {
            var pc = new PersianCalendar();
            var startMonth = 1 + 3 * (timebox.ThreeMonthNo - 1);
            var start = DateUtil.ToGregorian(timebox.SolarYear, startMonth, 1);
            var end = start.AddPersianMonths(3);
            timebox.Start = start;
            timebox.End = end;
        }

        private async Task SetInterimIndexesAsync(TimeBox timebox)
        {
            var lastTimebox = await TimeBoxService.GetLastNonInterimTimeboxAsync();
            var indexes = lastTimebox.PAIndexes?.Select(i => new PAIndex(timebox, i.Subfield)
            {
                Value = i.Value
            }).ToList();
            timebox.PAIndexes = indexes;
        }
    }
}
