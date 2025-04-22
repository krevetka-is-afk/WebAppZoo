using System.Collections.Concurrent;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Infrastructure.Repositories;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly ConcurrentDictionary<Guid, FeedingSchedule> _schedules = new();

    public Task<FeedingSchedule?> GetByIdAsync(Guid id)
    {
        _schedules.TryGetValue(id, out var schedule);
        return Task.FromResult(schedule);
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<FeedingSchedule>>(_schedules.Values);
    }

    public Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId)
    {
        var schedules = _schedules.Values.Where(s => s.AnimalId == animalId);
        return Task.FromResult(schedules);
    }

    public Task AddAsync(FeedingSchedule schedule)
    {
        if (!_schedules.TryAdd(schedule.Id, schedule))
        {
            throw new InvalidOperationException($"Feeding schedule with ID {schedule.Id} already exists");
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(FeedingSchedule schedule)
    {
        if (!_schedules.TryUpdate(schedule.Id, schedule, _schedules[schedule.Id]))
        {
            throw new InvalidOperationException($"Feeding schedule with ID {schedule.Id} not found");
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_schedules.TryRemove(id, out _))
        {
            throw new InvalidOperationException($"Feeding schedule with ID {id} not found");
        }
        return Task.CompletedTask;
    }
} 