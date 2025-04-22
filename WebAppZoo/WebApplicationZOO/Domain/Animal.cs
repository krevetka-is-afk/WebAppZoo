namespace WebApplicationZOO.Domain;

public class Animal
{
    public Guid Id { get; private set; }
    public string Species { get; private set; }
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Gender { get; private set; }
    public string FavoriteFood { get; private set; }
    public bool IsHealthy { get; private set; }
    public Guid? EnclosureId { get; private set; }

    public Animal(string species, string name, DateTime birthDate, string gender, string favoriteFood)
    {
        Id = Guid.NewGuid();
        Species = species;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        IsHealthy = true;
    }

    public void Feed()
    {
        // Implementation for feeding
    }

    public void Treat()
    {
        IsHealthy = true;
    }

    public void MoveToEnclosure(Guid newEnclosureId)
    {
        EnclosureId = newEnclosureId;
        // Raise domain event here
    }
} 