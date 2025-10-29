using System.Collections.Concurrent;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Infrastructure.Repositories;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly ConcurrentDictionary<Guid, Enclosure> _enclosures = new();

    public Task<Enclosure?> GetByIdAsync(Guid id)
    {
        _enclosures.TryGetValue(id, out var enclosure);
        return Task.FromResult(enclosure);
    }

    public Task<IEnumerable<Enclosure>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Enclosure>>(_enclosures.Values);
    }

    public Task AddAsync(Enclosure enclosure)
    {
        if (!_enclosures.TryAdd(enclosure.Id, enclosure))
        {
            throw new InvalidOperationException($"Enclosure with ID {enclosure.Id} already exists");
        }
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Enclosure enclosure)
    {
        if (!_enclosures.TryUpdate(enclosure.Id, enclosure, _enclosures[enclosure.Id]))
        {
            throw new InvalidOperationException($"Enclosure with ID {enclosure.Id} not found");
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        if (!_enclosures.TryRemove(id, out _))
        {
            throw new InvalidOperationException($"Enclosure with ID {id} not found");
        }
        return Task.CompletedTask;
    }
} 