using DataModel.Model;

namespace Services.Abstractions
{
    public interface ITimeBoxService
    {
        Task<TimeBox[]> GetAllTimeBoxesAsync();
    }
}