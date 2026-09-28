using LossPrevention.Api.Models.DTOs.Analytics;
using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Fetch complete analytics summary for 3D Pie, Category Loss, and Top Stolen Items
    /// </summary>
    [HttpGet("summary")]
    [ProducesResponseType(typeof(ApiResponse<AnalyticsSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAnalyticsSummary(
        [FromQuery] string? storeCode,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        var result = await _analyticsService.GetAnalyticsAsync(storeCode, startDate, endDate);
        return Ok(result);
    }

    /// <summary>
    /// Fetch time-of-day theft incidents (Hourly Bar Chart)
    /// </summary>
    [HttpGet("theft-by-time-of-day")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<HourlyTheftDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHourlyThefts([FromQuery] string? storeCode, [FromQuery] string? date)
    {
        var result = await _analyticsService.GetHourlyTheftsAsync(storeCode, date);
        return Ok(result);
    }

    /// <summary>
    /// Fetch day-of-week theft incidents (Weekly Bar Chart)
    /// </summary>
    [HttpGet("theft-by-day-of-week")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<WeeklyTheftDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWeeklyThefts(
        [FromQuery] string? storeCode,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        var result = await _analyticsService.GetWeeklyTheftsAsync(storeCode, startDate, endDate);
        return Ok(result);
    }
}
