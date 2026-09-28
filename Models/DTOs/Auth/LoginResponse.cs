namespace LossPrevention.Api.Models.DTOs.Auth;

public class LoginResponse
{
    public bool Authenticated { get; set; }
    public string Token { get; set; } = string.Empty;
    public UserDto? User { get; set; }
    public string Message { get; set; } = string.Empty;
}

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int IsStatus { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public DateTime? CreationDateTime { get; set; }
    public string FormattedCreationDate { get; set; } = string.Empty;
}
