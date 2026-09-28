using System.ComponentModel.DataAnnotations;

namespace LossPrevention.Api.Models.DTOs.Auth;

/// <summary>
/// Credentials payload for user authentication
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Username or operator account identifier
    /// </summary>
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// User account password
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}
