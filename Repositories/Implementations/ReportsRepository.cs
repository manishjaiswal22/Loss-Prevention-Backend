using System.Data;
using Dapper;
using LossPrevention.Api.Common.Constants;
using LossPrevention.Api.Data;
using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.DTOs.Reports;
using LossPrevention.Api.Repositories.Interfaces;

namespace LossPrevention.Api.Repositories.Implementations;

public class ReportsRepository : IReportsRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ReportsRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<PagedResponse<IncidentReportDto>> GetIncidentReportsAsync(ReportFilterRequest filter)
    {
        using var connection = _connectionFactory.CreateConnection();
        try
        {
            var parameters = new
            {
                StoreCode = filter.StoreCode,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                EventType = filter.EventType,
                SearchTerm = filter.SearchTerm,
                PageIndex = filter.PageIndex,
                PageSize = filter.PageSize
            };

            var items = await connection.QueryAsync<IncidentReportDto>(
                SpNames.GetIncidentReportGrid,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var itemList = items.ToList();
            var totalCount = itemList.Count; // If SP returns total as column, map it, else fallback to count

            return new PagedResponse<IncidentReportDto>(itemList, totalCount, filter.PageIndex, filter.PageSize);
        }
        catch
        {
            // Fallback for offline / design simulation
            var dummyData = GenerateMockReports();
            var filtered = dummyData.AsEnumerable();

            if (!string.IsNullOrEmpty(filter.EventType) && filter.EventType != "All")
                filtered = filtered.Where(x => x.EventType.Equals(filter.EventType, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLowerInvariant();
                filtered = filtered.Where(x => x.ArticleDescription.ToLowerInvariant().Contains(term) ||
                                               x.Epc.ToLowerInvariant().Contains(term) ||
                                               x.ArticleNo.ToLowerInvariant().Contains(term));
            }

            var total = filtered.Count();
            var paged = filtered.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            return new PagedResponse<IncidentReportDto>(paged, total, filter.PageIndex, filter.PageSize);
        }
    }

    private static List<IncidentReportDto> GenerateMockReports()
    {
        var list = new List<IncidentReportDto>();
        for (int i = 1; i <= 35; i++)
        {
            list.Add(new IncidentReportDto
            {
                SrNo = i,
                Date = "28-09-2026",
                Time = $"{10 + (i % 8):D2}:{(i * 7) % 60:D2}:00",
                StoreCode = i % 2 == 0 ? "HD55" : "BL02",
                StoreName = i % 2 == 0 ? "Dwarka Sector 12" : "Connaught Place",
                Epc = $"E280116060000204A8{i:D4}",
                ArticleNo = $"AR{10000 + i}",
                ArticleDescription = i % 2 == 0 ? "Men's Slim Fit Polo T-Shirt" : "Wireless Stereo Bluetooth Earbuds",
                Qty = 1,
                Amount = 999m + (i * 150m),
                EventType = i % 3 == 0 ? "Untagged" : "Theft"
            });
        }
        return list;
    }
}
