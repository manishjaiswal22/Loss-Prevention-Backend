using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Reports;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    /// <summary>
    /// Fetch paged, sorted, and filtered incident records for the React DataTable
    /// </summary>
    [HttpGet("incidents")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<IncidentReportDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIncidentReports([FromQuery] ReportFilterRequest filter)
    {
        var result = await _reportsService.GetReportsAsync(filter);
        return Ok(result);
    }
}
