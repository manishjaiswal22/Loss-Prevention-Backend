using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticate user credentials against [RFID_ReaderDB]
    /// </summary>
    /// <param name="request">Username and password credentials</param>
    /// <returns>Authentication result with user profile and session token</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (request == null)
        {
            return BadRequest(new LoginResponse
            {
                Success = false,
                Message = "Request body cannot be null.",
                Timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
            });
        }

        var (_, statusCode, response) = await _authService.LoginAsync(request);
        return StatusCode(statusCode, response);
    }
}
