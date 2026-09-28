using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.Entities;
using LossPrevention.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    /// <summary>
    /// Fetch active store connection details (Reader IP, Conn Type) using sp_GetStoreConnectionDetails
    /// </summary>
    [HttpGet("connection")]
    [ProducesResponseType(typeof(ApiResponse<StoreMaster?>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConnectionDetails()
    {
        var result = await _storeService.GetStoreConnectionAsync();
        return Ok(result);
    }

    /// <summary>
    /// Fetch list of all active retail stores
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<StoreMaster>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStores()
    {
        var result = await _storeService.GetAllStoresAsync();
        return Ok(result);
    }
}
