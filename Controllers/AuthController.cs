using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticate user credentials against [dbo].[sp_GetUserDetails]
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.AuthenticateAsync(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Fetch user details / directory using [dbo].[sp_GetUserDetails]
    /// </summary>
    [HttpGet("users")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] int? userId, [FromQuery] string? username)
    {
        var result = await _authService.GetUsersAsync(userId, username);
        return Ok(result);
    }
}
