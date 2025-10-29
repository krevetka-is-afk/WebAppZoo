using System.Collections.Concurrent;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Infrastructure.Repositories;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly ConcurrentDictionary<Guid, Animal> _animals = new();

    public Task<Animal?> GetByIdAsync(Guid id)
    {
        _animals.TryGetValue(id, out var animal);
        return Task.FromResult(animal);
    }

    public Task<IEnumerable<Animal>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Animal>>(_animals.Values);
    }

    public Task AddAsync(Animal animal)
    {
        if (!_animals.TryAdd(animal.Id, animal))
        {
            throw new InvalidOperationException($"Animal with ID {animal.Id} already exists");
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Animal animal)
    {
        if (!_animals.TryUpdate(animal.Id, animal, _animals[animal.Id]))
        {
            throw new InvalidOperationException($"Animal with ID {animal.Id} not found");
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_animals.TryRemove(id, out _))
        {
            throw new InvalidOperationException($"Animal with ID {id} not found");
        }
        return Task.CompletedTask;
    }
} 