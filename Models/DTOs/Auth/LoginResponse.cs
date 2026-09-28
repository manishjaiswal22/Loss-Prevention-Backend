namespace LossPrevention.Api.Models.DTOs.Auth;

/// <summary>
/// Authenticated user profile data transferred upon successful login
/// </summary>
public class UserDetailsDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int IsStatus { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public string FormattedCreationDate { get; set; } = string.Empty;
}

/// <summary>
/// Standard response payload for authentication endpoints
/// </summary>
public class LoginResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public UserDetailsDto? User { get; set; }
    public string? Token { get; set; }
    public string Timestamp { get; set; } = string.Empty;
}
