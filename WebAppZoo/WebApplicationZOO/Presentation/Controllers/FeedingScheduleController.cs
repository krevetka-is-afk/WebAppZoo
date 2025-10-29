using Microsoft.AspNetCore.Mvc;
using WebApplicationZOO.Application.Interfaces;
using WebApplicationZOO.Application.Services;
using WebApplicationZOO.Domain;

namespace WebApplicationZOO.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingScheduleController : ControllerBase
{
    private readonly IFeedingScheduleRepository _feedingScheduleRepository;
    private readonly FeedingOrganizationService _feedingOrganizationService;

    public FeedingScheduleController(
        IFeedingScheduleRepository feedingScheduleRepository,
        FeedingOrganizationService feedingOrganizationService)
    {
        _feedingScheduleRepository = feedingScheduleRepository;
        _feedingOrganizationService = feedingOrganizationService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedingSchedule>>> GetAll()
    {
        var schedules = await _feedingScheduleRepository.GetAllAsync();
        return Ok(schedules);
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<FeedingSchedule>>> GetUpcoming([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var schedules = await _feedingOrganizationService.GetUpcomingFeedingsAsync(from, to);
        return Ok(schedules);
    }

    [HttpPost]
    public async Task<ActionResult<FeedingSchedule>> Create([FromBody] CreateFeedingScheduleRequest request)
    {
        try
        {
            await _feedingOrganizationService.ScheduleFeedingAsync(
                request.AnimalId,
                request.Time,
                request.FoodType
            );
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/mark-completed")]
    public async Task<IActionResult> MarkCompleted(Guid id)
    {
        try
        {
            await _feedingOrganizationService.MarkFeedingAsCompletedAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class CreateFeedingScheduleRequest
{
    public Guid AnimalId { get; set; }
    public DateTime Time { get; set; }
    public string FoodType { get; set; } = null!;
} 