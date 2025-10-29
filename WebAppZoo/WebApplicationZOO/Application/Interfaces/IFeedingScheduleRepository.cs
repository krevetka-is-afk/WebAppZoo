using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Application.Interfaces;

public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule?> GetByIdAsync(Guid id);
    Task<IEnumerable<FeedingSchedule>> GetAllAsync();
    Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId);
    Task AddAsync(FeedingSchedule schedule);
    Task UpdateAsync(FeedingSchedule schedule);
    Task DeleteAsync(Guid id);
} 