using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Reports;

namespace LossPrevention.Api.Repositories.Interfaces;

public interface IReportsRepository
{
    Task<PagedResponse<IncidentReportDto>> GetIncidentReportsAsync(ReportFilterRequest filter);
}
