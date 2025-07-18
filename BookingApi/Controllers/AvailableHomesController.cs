using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/available-homes")]
public class AvailableHomesController : ControllerBase
{
    private readonly IHomeAvailabilityService _homeService;
    private readonly ILogger<AvailableHomesController> _logger;

    public AvailableHomesController(IHomeAvailabilityService homeService, ILogger<AvailableHomesController> logger)
    {
        _homeService = homeService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(AvailableHomeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] string startDate, [FromQuery] string endDate, CancellationToken ct)
    {
        if (!DateOnly.TryParse(startDate, out var start) || !DateOnly.TryParse(endDate, out var end))
        {
            return BadRequest(new ErrorResponse
            {
                Error = "Invalid date format",
                Details = "Expected format: yyyy-MM-dd"
            });
        }

        if (start > end)
        {
            return BadRequest(new ErrorResponse
            {
                Error = "Invalid range",
                Details = "startDate must be earlier than or equal to endDate"
            });
        }

        if ((end.DayNumber - start.DayNumber) > 90)
        {
            return BadRequest(new ErrorResponse
            {
                Error = "Date range too large",
                Details = "Maximum supported range is 90 days"
            });
        }

        var homes = await _homeService.GetAvailableHomesAsync(start, end, ct);
        return Ok(new AvailableHomeResponse { Homes = homes });
    }
}