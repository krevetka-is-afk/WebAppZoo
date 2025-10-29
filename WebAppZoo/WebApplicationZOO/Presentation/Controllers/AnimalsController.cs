using Microsoft.AspNetCore.Mvc;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Application.Services;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepository;
    private readonly AnimalTransferService _animalTransferService;

    public AnimalsController(IAnimalRepository animalRepository, AnimalTransferService animalTransferService)
    {
        _animalRepository = animalRepository;
        _animalTransferService = animalTransferService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> GetAll()
    {
        var animals = await _animalRepository.GetAllAsync();
        return Ok(animals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetById(Guid id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal == null)
        {
            return NotFound();
        }
        return Ok(animal);
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> Create([FromBody] CreateAnimalRequest request)
    {
        var animal = new Animal(
            request.Species,
            request.Name,
            request.BirthDate,
            request.Gender,
            request.FavoriteFood
        );

        await _animalRepository.AddAsync(animal);
        return CreatedAtAction(nameof(GetById), new { id = animal.Id }, animal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _animalRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/transfer")]
    public async Task<IActionResult> Transfer(Guid id, [FromBody] TransferAnimalRequest request)
    {
        try
        {
            await _animalTransferService.TransferAnimalAsync(id, request.NewEnclosureId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateAnimalRequest
{
    public string Species { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; } = null!;
    public string FavoriteFood { get; set; } = null!;
}

public class TransferAnimalRequest
{
    public Guid NewEnclosureId { get; set; }
} 