using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Dashboard;

namespace LossPrevention.Api.Services.Interfaces;

public interface IDashboardService
{
    Task<ApiResponse<TodayDashboardDto>> GetTodayDashboardAsync(string? storeCode = null);
    Task<ApiResponse<IEnumerable<RealtimeAlertDto>>> GetLiveAlertsAsync(string? storeCode = null, int top = 50);
}
