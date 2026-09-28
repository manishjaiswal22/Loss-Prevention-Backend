using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Reports;
using LossPrevention.Api.Models.Entities;

namespace LossPrevention.Api.Services.Interfaces;

public interface IReportsService
{
    Task<ApiResponse<PagedResponse<IncidentReportDto>>> GetReportsAsync(ReportFilterRequest filter);
}

public interface IStoreService
{
    Task<ApiResponse<StoreMaster?>> GetStoreConnectionAsync();
    Task<ApiResponse<IEnumerable<StoreMaster>>> GetAllStoresAsync();
}
