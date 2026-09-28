using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Dashboard;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<ApiResponse<TodayDashboardDto>> GetTodayDashboardAsync(string? storeCode = null)
    {
        var data = await _dashboardRepository.GetTodayDashboardAsync(storeCode);
        var alerts = (await _dashboardRepository.GetLatestRealtimeEventsAsync(storeCode)).ToList();

        data.UntaggedItems = alerts.Where(x => x.EventType.Equals("Untagged", StringComparison.OrdinalIgnoreCase));
        data.TheftAlertItems = alerts.Where(x => x.EventType.Equals("Theft", StringComparison.OrdinalIgnoreCase));

        return ApiResponse<TodayDashboardDto>.Ok(data, "Today dashboard metrics loaded");
    }

    public async Task<ApiResponse<IEnumerable<RealtimeAlertDto>>> GetLiveAlertsAsync(string? storeCode = null, int top = 50)
    {
        var alerts = await _dashboardRepository.GetLatestRealtimeEventsAsync(storeCode, top);
        return ApiResponse<IEnumerable<RealtimeAlertDto>>.Ok(alerts, "Realtime alerts fetched");
    }
}
