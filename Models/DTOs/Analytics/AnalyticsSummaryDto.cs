namespace LossPrevention.Api.Models.DTOs.Analytics;

public class AnalyticsSummaryDto
{
    public TagDistributionDto TagDistribution { get; set; } = new();
    public IEnumerable<CategoryLossDto> CategoryLoss { get; set; } = Enumerable.Empty<CategoryLossDto>();
    public IEnumerable<TopStolenItemDto> TopStolenItems { get; set; } = Enumerable.Empty<TopStolenItemDto>();
    public IEnumerable<HourlyTheftDto> HourlyTrends { get; set; } = Enumerable.Empty<HourlyTheftDto>();
    public IEnumerable<WeeklyTheftDto> WeeklyTrends { get; set; } = Enumerable.Empty<WeeklyTheftDto>();
}

public class TagDistributionDto
{
    public int TotalTags { get; set; }
    public int Untagged { get; set; }
    public int TheftAlerts { get; set; }
    public decimal PotentialLoss { get; set; }
}

public class CategoryLossDto
{
    public string CategoryName { get; set; } = string.Empty;
    public int LossCount { get; set; }
    public decimal LossAmount { get; set; }
}

public class TopStolenItemDto
{
    public string ArticleNo { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TheftCount { get; set; }
    public decimal TotalLossAmount { get; set; }
    public double SharePercent { get; set; }
}

public class HourlyTheftDto
{
    public string TimeSlot { get; set; } = string.Empty; // e.g. "12PM - 3PM"
    public int IncidentCount { get; set; }
    public bool IsPeak { get; set; }
}

public class WeeklyTheftDto
{
    public string DayName { get; set; } = string.Empty; // e.g. "Wed"
    public int IncidentCount { get; set; }
    public bool IsPeak { get; set; }
}
