using LossPrevention.Api.Models.DTOs.Analytics;
using LossPrevention.Api.Models.DTOs.Common;

namespace LossPrevention.Api.Services.Interfaces;

public interface IAnalyticsService
{
    Task<ApiResponse<AnalyticsSummaryDto>> GetAnalyticsAsync(string? storeCode = null, string? startDate = null, string? endDate = null);
    Task<ApiResponse<IEnumerable<HourlyTheftDto>>> GetHourlyTheftsAsync(string? storeCode = null, string? date = null);
    Task<ApiResponse<IEnumerable<WeeklyTheftDto>>> GetWeeklyTheftsAsync(string? storeCode = null, string? startDate = null, string? endDate = null);
}
