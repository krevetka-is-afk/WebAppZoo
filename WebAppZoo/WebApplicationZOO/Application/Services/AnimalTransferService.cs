using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain.Events;

namespace WebApplicationZOO.Application.Services;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IEnclosureRepository _enclosureRepository;

    public AnimalTransferService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
    {
        _animalRepository = animalRepository;
        _enclosureRepository = enclosureRepository;
    }

    public async Task TransferAnimalAsync(Guid animalId, Guid newEnclosureId)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal == null)
        {
            throw new ArgumentException($"Animal with ID {animalId} not found");
        }

        var newEnclosure = await _enclosureRepository.GetByIdAsync(newEnclosureId);
        if (newEnclosure == null)
        {
            throw new ArgumentException($"Enclosure with ID {newEnclosureId} not found");
        }

        if (!newEnclosure.CanAddAnimal())
        {
            throw new InvalidOperationException("New enclosure is at maximum capacity");
        }

        var oldEnclosureId = animal.EnclosureId;
        
        // Remove from old enclosure if exists
        if (oldEnclosureId.HasValue)
        {
            var oldEnclosure = await _enclosureRepository.GetByIdAsync(oldEnclosureId.Value);
            if (oldEnclosure != null)
            {
                oldEnclosure.RemoveAnimal(animalId);
                await _enclosureRepository.UpdateAsync(oldEnclosure);
            }
        }

        // Add to new enclosure
        newEnclosure.AddAnimal(animalId);
        await _enclosureRepository.UpdateAsync(newEnclosure);

        // Update animal's enclosure
        animal.MoveToEnclosure(newEnclosureId);
        await _animalRepository.UpdateAsync(animal);

        // Raise domain event
        var @event = new AnimalMovedEvent(animalId, oldEnclosureId, newEnclosureId);
        // TODO: Implement event handling
    }
} 