namespace LossPrevention.Api.Models.DTOs.Reports;

public class IncidentReportDto
{
    public long SrNo { get; set; }
    public string Date { get; set; } = string.Empty; // "dd-MM-yyyy"
    public string Time { get; set; } = string.Empty;
    public string StoreCode { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
    public string Epc { get; set; } = string.Empty;
    public string ArticleNo { get; set; } = string.Empty;
    public string ArticleDescription { get; set; } = string.Empty;
    public int Qty { get; set; } = 1;
    public decimal Amount { get; set; }
    public string EventType { get; set; } = string.Empty; // "Theft" or "Untagged"
}

public class ReportFilterRequest
{
    public string? StoreCode { get; set; }
    public string? StartDate { get; set; } // "dd-MM-yyyy"
    public string? EndDate { get; set; }   // "dd-MM-yyyy"
    public string? EventType { get; set; } // "All", "Theft", "Untagged"
    public string? SearchTerm { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortField { get; set; } = "date";
    public string? SortDirection { get; set; } = "desc";
}
