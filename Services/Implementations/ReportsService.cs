using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Reports;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

public class ReportsService : IReportsService
{
    private readonly IReportsRepository _reportsRepository;

    public ReportsService(IReportsRepository reportsRepository)
    {
        _reportsRepository = reportsRepository;
    }

    public async Task<ApiResponse<PagedResponse<IncidentReportDto>>> GetReportsAsync(ReportFilterRequest filter)
    {
        var result = await _reportsRepository.GetIncidentReportsAsync(filter);
        return ApiResponse<PagedResponse<IncidentReportDto>>.Ok(result, "Incident reports retrieved");
    }
}
