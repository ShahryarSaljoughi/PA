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
        private PAIndex? selectedIndex;
        private TimeBox? selectedTimeBox;

        private List<PAIndex> ModifiedIndexes { get; } = new List<PAIndex>();
        public bool IsReadyToAddIndex => SelectedTimeBox != null;
        public bool IsReadyToDeleteIndex => SelectedIndex != null;
        public List<PAIndex> ToBeAddedIndexes { get; } = new List<PAIndex>();
        public ObservableCollection<TimeBox> TimeBoxes { get; set; } = new ObservableCollection<TimeBox>();
        public TimeBox? SelectedTimeBox
        {
            get => selectedTimeBox;
            set
            {
                selectedTimeBox = value;
                OnPropertyChanged(nameof(IsReadyToAddIndex));
            }
        }
        public PAIndex? SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(IsReadyToDeleteIndex));
            }
        }
        public IIndexService IndexService { get; }
        public ObservableCollection<PAIndex> Indexes { get; set; } = new ObservableCollection<PAIndex>();
        public string IndexGroupHeader => string.Join(" ", "شاخص‌ها", SelectedTimeBox?.Text);
        public ICommand SaveIndexChangesCommand { get; }
        public ICommand NewIndexCommand { get; }
        public ITimeBoxService TimeBoxService { get; }
        public IndexEditViewModel(ITimeBoxService timeBoxService, IIndexService indexService)
        {
            TimeBoxService = timeBoxService;
            IndexService = indexService;
            SaveIndexChangesCommand = new RelayCommand(async _ => await SaveIndexChangesAsync());
            NewIndexCommand = new RelayCommand(_ => { });
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
            SetStartEndDates(timebox);
            if (timebox.IsInterim)
            {
                await SetInterimIndexesAsync(timebox);
            }
            var added = await TimeBoxService.SaveNewTimeboxAsync(timebox);
            if (added is not null)
            {
                TimeBoxes.Add(timebox);
                TimeBoxes.SortDescending();
            }
        }

        internal async Task DeleteSelectedTimeboxAsync()
        {
            await TimeBoxService.DeleteTimeboxAsync(SelectedTimeBox);
            TimeBoxes.Remove(SelectedTimeBox);
        }
        internal async Task SaveIndexChangesAsync()
        {
            await IndexService.SaveIndexesAsync(ModifiedIndexes);
        }
        internal async Task DeleteSelectedIndexAsync()
        {
            if (SelectedIndex is null) return;
            await IndexService.DeleteIndexAsync(SelectedIndex);
            if (Indexes is not null && Indexes.Contains(SelectedIndex))
            {
                Indexes.Remove(SelectedIndex);
            }

        }
        internal void OnIndexChanged(PAIndex editedIndex)
        {
            ModifiedIndexes.Add(editedIndex);
        }
        internal async Task AddIndexAsync(PAIndex? newIndex)
        {
            if (newIndex == null) { return; }
            await IndexService.SaveNewIndex(newIndex);
            Indexes.Add(newIndex);

        }
        private void SetStartEndDates(TimeBox timebox)
        {
            var startMonth = 1 + 3 * (timebox.ThreeMonthNo - 1);
            var start = DateUtil.ToGregorian(timebox.SolarYear, startMonth, 1);
            var end = start.AddPersianMonths(3).AddDays(-1);
            timebox.Start = start;
            timebox.End = end;
        }
        private async Task SetInterimIndexesAsync(TimeBox timebox)
        {
            var lastTimebox = await TimeBoxService.GetLatestNonInterimTimeboxBeforeAsync(timebox);
            var indexes = lastTimebox.PAIndexes?.Select(i => new PAIndex(timebox, i.Subfield)
            {
                Value = i.Value
            }).ToList();
            timebox.PAIndexes = indexes;
        }

    }
}
