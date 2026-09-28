using LossPrevention.Api.Models.DTOs.Analytics;

namespace LossPrevention.Api.Repositories.Interfaces;

public interface IAnalyticsRepository
{
    Task<AnalyticsSummaryDto> GetAnalyticsDashboardAsync(string? storeCode = null, string? startDate = null, string? endDate = null);
    Task<IEnumerable<HourlyTheftDto>> GetHourlyTheftStatsAsync(string? storeCode = null, string? date = null);
    Task<IEnumerable<WeeklyTheftDto>> GetWeeklyTheftStatsAsync(string? storeCode = null, string? startDate = null, string? endDate = null);
}
