using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Dashboard;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    /// <summary>
    /// Get aggregated metrics and current day item streams for the main Overview screen
    /// </summary>
    [HttpGet("today")]
    [ProducesResponseType(typeof(ApiResponse<TodayDashboardDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodayDashboard([FromQuery] string? storeCode)
    {
        var result = await _dashboardService.GetTodayDashboardAsync(storeCode);
        return Ok(result);
    }

    /// <summary>
    /// Get real-time alert feed (Theft and Untagged events)
    /// </summary>
    [HttpGet("realtime-feed")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<RealtimeAlertDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRealtimeFeed([FromQuery] string? storeCode, [FromQuery] int top = 50)
    {
        var result = await _dashboardService.GetLiveAlertsAsync(storeCode, top);
        return Ok(result);
    }
}
