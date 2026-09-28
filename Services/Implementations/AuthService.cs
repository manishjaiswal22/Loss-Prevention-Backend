using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ApiResponse<LoginResponse>> AuthenticateAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return ApiResponse<LoginResponse>.Fail("Username and password are required.");
        }

        var user = await _userRepository.ValidateCredentialsAsync(request.Username.Trim(), request.Password.Trim());

        if (user == null)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", request.Username);
            return ApiResponse<LoginResponse>.Fail("Invalid credentials or account is inactive.");
        }

        // Generate session / mock JWT token
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        var response = new LoginResponse
        {
            Authenticated = true,
            Token = token,
            User = user,
            Message = "Authentication successful"
        };

        return ApiResponse<LoginResponse>.Ok(response, "Login successful");
    }

    public async Task<ApiResponse<IEnumerable<UserDto>>> GetUsersAsync(int? userId = null, string? username = null)
    {
        var users = await _userRepository.GetUsersAsync(userId, username);
        return ApiResponse<IEnumerable<UserDto>>.Ok(users, "Users retrieved successfully");
    }
}
