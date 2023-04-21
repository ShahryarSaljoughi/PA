using DataModel.Model;

namespace Services.Abstractions
{
    public interface ITimeBoxService
    {
        Task<List<TimeBox>> GetAllTimeBoxesAsync();
        Task LoadAsync();
        Task<TimeBox> SaveNewTimeboxAsync(TimeBox timeBox);
        Task<TimeBox> GetLastNonInterimTimeboxAsync();
        Task DeleteTimeboxAsync(TimeBox? selectedTimeBox);
        Task<TimeBox> GetLatestNonInterimTimeboxBeforeAsync(TimeBox timebox);
    }
}