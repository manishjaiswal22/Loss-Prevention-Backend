namespace LossPrevention.Api.Common.Constants;

/// <summary>
/// Centralized Stored Procedure names in [RFID_ReaderDB]
/// </summary>
public static class SpNames
{
    public const string GetUserDetails = "sp_GetUserDetails";
    public const string GetTodayDashboard = "sp_GetTodayDashboard";
    public const string GetAnalyticsDashboard = "sp_GetAnalyticsDashboard";
    public const string GetIncidentReportGrid = "sp_GetIncidentReportGrid";
    public const string GetTimeWiseLossStats = "sp_GetTimeWiseLossStats";
    public const string GetDayWiseLossStats = "sp_GetDayWiseLossStats";
    public const string GetStoreConnectionDetails = "sp_GetStoreConnectionDetails";
    public const string SaveSingleTag = "sp_SaveSingleTag";
}
