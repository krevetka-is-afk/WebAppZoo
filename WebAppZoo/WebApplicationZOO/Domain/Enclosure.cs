namespace WebApplicationZOO.Domain;

public class Enclosure
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public int Size { get; private set; }
    public int MaxCapacity { get; private set; }
    public int CurrentAnimalCount { get; private set; }
    private readonly List<Guid> _animalIds = new();

    public Enclosure(string type, int size, int maxCapacity)
    {
        Id = Guid.NewGuid();
        Type = type;
        Size = size;
        MaxCapacity = maxCapacity;
        CurrentAnimalCount = 0;
    }

    public bool CanAddAnimal()
    {
        return CurrentAnimalCount < MaxCapacity;
    }

    public void AddAnimal(Guid animalId)
    {
        if (!CanAddAnimal())
        {
            throw new InvalidOperationException("Enclosure is at maximum capacity");
        }

        _animalIds.Add(animalId);
        CurrentAnimalCount++;
    }

    public void RemoveAnimal(Guid animalId)
    {
        if (_animalIds.Remove(animalId))
        {
            CurrentAnimalCount--;
        }
    }

    public void Clean()
    {
        // Implementation for cleaning
    }
} 