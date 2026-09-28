using LossPrevention.Api.Models.DTOs.Analytics;
using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

public class AnalyticsService : IAnalyticsService
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public AnalyticsService(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    public async Task<ApiResponse<AnalyticsSummaryDto>> GetAnalyticsAsync(string? storeCode = null, string? startDate = null, string? endDate = null)
    {
        var data = await _analyticsRepository.GetAnalyticsDashboardAsync(storeCode, startDate, endDate);
        return ApiResponse<AnalyticsSummaryDto>.Ok(data, "Analytics data retrieved");
    }

    public async Task<ApiResponse<IEnumerable<HourlyTheftDto>>> GetHourlyTheftsAsync(string? storeCode = null, string? date = null)
    {
        var data = await _analyticsRepository.GetHourlyTheftStatsAsync(storeCode, date);
        return ApiResponse<IEnumerable<HourlyTheftDto>>.Ok(data, "Hourly theft distribution retrieved");
    }

    public async Task<ApiResponse<IEnumerable<WeeklyTheftDto>>> GetWeeklyTheftsAsync(string? storeCode = null, string? startDate = null, string? endDate = null)
    {
        var data = await _analyticsRepository.GetWeeklyTheftStatsAsync(storeCode, startDate, endDate);
        return ApiResponse<IEnumerable<WeeklyTheftDto>>.Ok(data, "Weekly theft distribution retrieved");
    }
}
