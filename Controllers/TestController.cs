using System.Data;
using Dapper;
using LossPrevention.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace LossPrevention.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<TestController> _logger;

    public TestController(IDbConnectionFactory connectionFactory, ILogger<TestController> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    /// <summary>
    /// Basic API health check
    /// </summary>
    [HttpGet("health")]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            success = true,
            status = "Online",
            service = "Loss Prevention & Goods Security .NET 8 Backend API",
            timestamp = DateTime.UtcNow,
            serverTimeLocal = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
        });
    }

    /// <summary>
    /// Test live database connection to [RFID_ReaderDB] and execute sp_GetStoreConnectionDetails
    /// </summary>
    [HttpGet("db-connection")]
    public async Task<IActionResult> TestDbConnection()
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            
            // Execute sp_GetStoreConnectionDetails
            var storeDetails = await connection.QueryFirstOrDefaultAsync(
                "sp_GetStoreConnectionDetails",
                commandType: CommandType.StoredProcedure
            );

            // Fetch table counts for verification
            var counts = await connection.QueryFirstOrDefaultAsync(@"
                SELECT 
                    (SELECT COUNT(*) FROM dbo.StoreMaster) AS StoreMasterCount,
                    (SELECT COUNT(*) FROM dbo.ScannedTags) AS ScannedTagsCount,
                    (SELECT COUNT(*) FROM dbo.ReaderSessions) AS ReaderSessionsCount,
                    (SELECT COUNT(*) FROM dbo.tbl_Material_Master) AS MaterialMasterCount,
                    (SELECT COUNT(*) FROM dbo.tbl_Ean_Mat_Mst) AS EanMasterCount
            ");

            return Ok(new
            {
                success = true,
                database = "RFID_ReaderDB",
                status = "Connected",
                storeInfo = storeDetails,
                tableMetrics = counts,
                verifiedAt = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to connect to database [RFID_ReaderDB]");
            return StatusCode(500, new
            {
                success = false,
                database = "RFID_ReaderDB",
                status = "Error",
                message = ex.Message
            });
        }
    }

    /// <summary>
    /// Fetch user details using stored procedure [dbo].[sp_GetUserDetails]
    /// </summary>
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? username, 
        [FromQuery] int? userId, 
        [FromQuery] int? isStatus)
    {
        try
        {
            using var connection = _connectionFactory.CreateConnection();
            var parameters = new
            {
                UserId = userId,
                Username = username,
                Password = (string?)null,
                IsStatus = isStatus
            };

            var users = await connection.QueryAsync(
                "sp_GetUserDetails",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return Ok(new
            {
                success = true,
                count = users.Count(),
                data = users
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling sp_GetUserDetails");
            return StatusCode(500, new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}
