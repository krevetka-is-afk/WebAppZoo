namespace WebApplicationZOO.Domain.Events;

public class FeedingTimeEvent
{
    public Guid AnimalId { get; }
    public string FoodType { get; }
    public DateTime ScheduledTime { get; }
    public DateTime OccurredOn { get; }

    public FeedingTimeEvent(Guid animalId, string foodType, DateTime scheduledTime)
    {
        AnimalId = animalId;
        FoodType = foodType;
        ScheduledTime = scheduledTime;
        OccurredOn = DateTime.UtcNow;
    }
} 