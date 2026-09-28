namespace LossPrevention.Api.Models.DTOs.Dashboard;

public class TodayDashboardDto
{
    public int TotalTags { get; set; }
    public int UntaggedCount { get; set; }
    public int TheftAlertsCount { get; set; }
    public decimal PotentialLossAmount { get; set; }
    public IEnumerable<RealtimeAlertDto> UntaggedItems { get; set; } = Enumerable.Empty<RealtimeAlertDto>();
    public IEnumerable<RealtimeAlertDto> TheftAlertItems { get; set; } = Enumerable.Empty<RealtimeAlertDto>();
}

public class RealtimeAlertDto
{
    public long Id { get; set; }
    public string Epc { get; set; } = string.Empty;
    public string ArticleNo { get; set; } = string.Empty;
    public string ArticleDescription { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string EventType { get; set; } = string.Empty; // "Theft" or "Untagged"
    public string EventDate { get; set; } = string.Empty;
    public string EventTime { get; set; } = string.Empty;
    public string StoreCode { get; set; } = string.Empty;
    public string StoreName { get; set; } = string.Empty;
}
