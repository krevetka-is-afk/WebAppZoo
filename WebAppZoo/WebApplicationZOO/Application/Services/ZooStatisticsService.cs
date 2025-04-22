using WebApplicationZOO.Application.Interfaces;

namespace WebApplicationZOO.Application.Services;

public class ZooStatisticsService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public async Task<ZooStatistics> GetStatisticsAsync()
    {
        var animals = await _animalRepository.GetAllAsync();
        var enclosures = await _enclosureRepository.GetAllAsync();

        return new ZooStatistics
        {
            TotalAnimals = animals.Count(),
            TotalEnclosures = enclosures.Count(),
            FreeEnclosures = enclosures.Count(e => e.CurrentAnimalCount == 0),
            OccupiedEnclosures = enclosures.Count(e => e.CurrentAnimalCount > 0),
            FullEnclosures = enclosures.Count(e => e.CurrentAnimalCount == e.MaxCapacity),
            AverageAnimalsPerEnclosure = enclosures.Any() 
                ? enclosures.Average(e => e.CurrentAnimalCount) 
                : 0
        };
    }
}

public class ZooStatistics
{
    public int TotalAnimals { get; set; }
    public int TotalEnclosures { get; set; }
    public int FreeEnclosures { get; set; }
    public int OccupiedEnclosures { get; set; }
    public int FullEnclosures { get; set; }
    public double AverageAnimalsPerEnclosure { get; set; }
} 