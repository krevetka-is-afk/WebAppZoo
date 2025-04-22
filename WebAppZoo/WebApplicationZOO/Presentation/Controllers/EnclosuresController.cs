using Microsoft.AspNetCore.Mvc;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosureRepository;

    public EnclosuresController(IEnclosureRepository enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enclosure>>> GetAll()
    {
        var enclosures = await _enclosureRepository.GetAllAsync();
        return Ok(enclosures);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Enclosure>> GetById(Guid id)
    {
        var enclosure = await _enclosureRepository.GetByIdAsync(id);
        if (enclosure == null)
        {
            return NotFound();
        }
        return Ok(enclosure);
    }

    [HttpPost]
    public async Task<ActionResult<Enclosure>> Create([FromBody] CreateEnclosureRequest request)
    {
        var enclosure = new Enclosure(
            request.Type,
            request.Size,
            request.MaxCapacity
        );

        await _enclosureRepository.AddAsync(enclosure);
        return CreatedAtAction(nameof(GetById), new { id = enclosure.Id }, enclosure);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _enclosureRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}

public class CreateEnclosureRequest
{
    public string Type { get; set; } = null!;
    public int Size { get; set; }
    public int MaxCapacity { get; set; }
} 