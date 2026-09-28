using System.Data;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using LossPrevention.Api.Data;
using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

/// <summary>
/// Implementation of user authentication using [RFID_ReaderDB] stored procedure [dbo].[sp_GetUserDetails]
/// </summary>
public class AuthService : IAuthService
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IDbConnectionFactory connectionFactory, ILogger<AuthService> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<(bool Success, int StatusCode, LoginResponse Response)> LoginAsync(LoginRequest request)
    {
        var timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return (false, StatusCodes.Status400BadRequest, new LoginResponse
            {
                Success = false,
                Message = "Username and password are required.",
                Timestamp = timestamp
            });
        }

        try
        {
            using var connection = _connectionFactory.CreateConnection();

            // 1. Fetch user by username via sp_GetUserDetails
            var parameters = new
            {
                UserId = (int?)null,
                Username = request.Username.Trim(),
                Password = (string?)null,
                IsStatus = (int?)null
            };

            var userRecord = await connection.QueryFirstOrDefaultAsync<UserDbRecord>(
                "sp_GetUserDetails",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            // 2. Validate user existence
            if (userRecord == null)
            {
                _logger.LogWarning("Failed login attempt: User '{Username}' not found", request.Username);
                return (false, StatusCodes.Status401Unauthorized, new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Timestamp = timestamp
                });
            }

            // 3. Verify user active status (1 = Active)
            if (userRecord.IsStatus != 1)
            {
                _logger.LogWarning("Login rejected: User '{Username}' (ID: {UserId}) is inactive (isStatus = {Status})", 
                    userRecord.Username, userRecord.UserId, userRecord.IsStatus);

                return (false, StatusCodes.Status403Forbidden, new LoginResponse
                {
                    Success = false,
                    Message = "Your account is currently inactive. Please contact your store administrator.",
                    Timestamp = timestamp
                });
            }

            // 4. Verify password
            // In dbo.tbl_User_Master passwords are stored as plaintext nvarchar
            if (!string.Equals(userRecord.Password, request.Password, StringComparison.Ordinal))
            {
                _logger.LogWarning("Failed login attempt: Incorrect password for user '{Username}'", request.Username);
                return (false, StatusCodes.Status401Unauthorized, new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password.",
                    Timestamp = timestamp
                });
            }

            // 5. Generate secure session / bearer token
            var tokenPayload = $"{userRecord.UserId}:{userRecord.Username}:{DateTime.UtcNow.Ticks}";
            var tokenHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(tokenPayload)));
            var sessionToken = $"lp_sec_{tokenHash[..32]}";

            _logger.LogInformation("User '{Username}' (ID: {UserId}) successfully authenticated at {Time}", 
                userRecord.Username, userRecord.UserId, timestamp);

            return (true, StatusCodes.Status200OK, new LoginResponse
            {
                Success = true,
                Message = "Login successful.",
                Token = sessionToken,
                Timestamp = timestamp,
                User = new UserDetailsDto
                {
                    UserId = userRecord.UserId,
                    Username = userRecord.Username,
                    IsStatus = userRecord.IsStatus,
                    StatusDescription = userRecord.StatusDescription ?? "Active",
                    FormattedCreationDate = userRecord.FormattedCreationDate ?? string.Empty
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during login authentication for '{Username}'", request.Username);
            return (false, StatusCodes.Status500InternalServerError, new LoginResponse
            {
                Success = false,
                Message = "An internal server error occurred while processing authentication.",
                Timestamp = timestamp
            });
        }
    }

    /// <summary>
    /// Internal mapping record matching columns returned by [dbo].[sp_GetUserDetails]
    /// </summary>
    private sealed class UserDbRecord
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int IsStatus { get; set; }
        public string? StatusDescription { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string? FormattedCreationDate { get; set; }
    }
}
