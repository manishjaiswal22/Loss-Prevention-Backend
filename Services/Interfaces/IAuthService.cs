using LossPrevention.Api.Models.DTOs.Auth;

namespace LossPrevention.Api.Services.Interfaces;

/// <summary>
/// Service contract for user authentication and session management
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Authenticates a user against [RFID_ReaderDB] using stored procedure [dbo].[sp_GetUserDetails]
    /// </summary>
    /// <param name="request">Login credentials containing Username and Password</param>
    /// <returns>Tuple containing Success flag, HTTP status code, and LoginResponse</returns>
    Task<(bool Success, int StatusCode, LoginResponse Response)> LoginAsync(LoginRequest request);
}
