using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain;
using WebApplicationZOO.Domain.Events;

namespace WebApplicationZOO.Application.Services;

public class FeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly IAnimalRepository _animalRepository;

    public FeedingOrganizationService(IFeedingScheduleRepository feedingScheduleRepository, IAnimalRepository animalRepository)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _animalRepository = animalRepository;
    }

    public async Task ScheduleFeedingAsync(Guid animalId, DateTime time, string foodType)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal == null)
        {
            throw new ArgumentException($"Animal with ID {animalId} not found");
        }

        var schedule = new FeedingSchedule(animalId, time, foodType);
        await _feedingScheduleRepository.AddAsync(schedule);

        // Raise domain event
        var @event = new FeedingTimeEvent(animalId, foodType, time);
        // TODO: Implement event handling
    }

    public async Task MarkFeedingAsCompletedAsync(Guid scheduleId)
    {
        var schedule = await _feedingScheduleRepository.GetByIdAsync(scheduleId);
        if (schedule == null)
        {
            throw new ArgumentException($"Feeding schedule with ID {scheduleId} not found");
        }

        schedule.MarkCompleted();
        await _feedingScheduleRepository.UpdateAsync(schedule);
    }

    public async Task<IEnumerable<FeedingSchedule>> GetUpcomingFeedingsAsync(DateTime from, DateTime to)
    {
        var allSchedules = await _feedingScheduleRepository.GetAllAsync();
        return allSchedules.Where(s => s.Time >= from && s.Time <= to && !s.IsCompleted);
    }
} 