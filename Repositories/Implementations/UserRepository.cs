using System.Data;
using Dapper;
using LossPrevention.Api.Common.Constants;
using LossPrevention.Api.Data;
using LossPrevention.Api.Models.DTOs.Auth;
using LossPrevention.Api.Repositories.Interfaces;

namespace LossPrevention.Api.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync(int? userId = null, string? username = null, string? password = null, int? isStatus = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new
        {
            UserId = userId,
            Username = username,
            Password = password,
            IsStatus = isStatus
        };

        return await connection.QueryAsync<UserDto>(
            SpNames.GetUserDetails,
            parameters,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<UserDto?> GetByUsernameAsync(string username)
    {
        var users = await GetUsersAsync(username: username);
        return users.FirstOrDefault();
    }

    public async Task<UserDto?> ValidateCredentialsAsync(string username, string password)
    {
        var users = await GetUsersAsync(username: username, password: password, isStatus: 1);
        return users.FirstOrDefault();
    }
}
