namespace LossPrevention.Api.Common.Constants;

/// <summary>
/// Application-wide constants
/// </summary>
public static class AppConstants
{
    public const string DateFormat = "dd-MM-yyyy";
    public const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";

    public static class EventTypes
    {
        public const string Theft = "Theft";
        public const string Untagged = "Untagged";
    }

    public static class Pagination
    {
        public const int DefaultPageIndex = 1;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
    }
}
