using DataModel.Model;

namespace Services.Abstractions
{
    public interface ITimeBoxService
    {
        Task<List<TimeBox>> GetAllTimeBoxesAsync();
        Task LoadAsync();
        Task SaveTimeboxAsync(TimeBox timeBox);
        Task<TimeBox> GetLastNonInterimTimeboxAsync();
        Task DeleteTimeboxAsync(TimeBox? selectedTimeBox);
    }
}