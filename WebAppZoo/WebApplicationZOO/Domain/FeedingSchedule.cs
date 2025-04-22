namespace WebApplicationZOO.Domain;

public class FeedingSchedule
{
    public Guid Id { get; private set; }
    public Guid AnimalId { get; private set; }
    public DateTime Time { get; private set; }
    public string FoodType { get; private set; }
    public bool IsCompleted { get; private set; }

    public FeedingSchedule(Guid animalId, DateTime time, string foodType)
    {
        Id = Guid.NewGuid();
        AnimalId = animalId;
        Time = time;
        FoodType = foodType;
        IsCompleted = false;
    }

    public void UpdateTime(DateTime newTime)
    {
        Time = newTime;
    }

    public void MarkCompleted()
    {
        IsCompleted = true;
    }
} 