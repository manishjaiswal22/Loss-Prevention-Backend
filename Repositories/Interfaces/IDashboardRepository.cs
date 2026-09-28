using LossPrevention.Api.Models.DTOs.Dashboard;

namespace LossPrevention.Api.Repositories.Interfaces;

public interface IDashboardRepository
{
    Task<TodayDashboardDto> GetTodayDashboardAsync(string? storeCode = null);
    Task<IEnumerable<RealtimeAlertDto>> GetLatestRealtimeEventsAsync(string? storeCode = null, int top = 50);
}
