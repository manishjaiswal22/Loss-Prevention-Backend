using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Models.DTOs.Common;

namespace LossPrevention.Api.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<LoginResponse>> AuthenticateAsync(LoginRequest request);
    Task<ApiResponse<IEnumerable<UserDto>>> GetUsersAsync(int? userId = null, string? username = null);
}
