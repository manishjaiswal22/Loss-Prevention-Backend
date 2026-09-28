using LossPrevention.Api.Models.DTOs.Auth;

namespace LossPrevention.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetUsersAsync(int? userId = null, string? username = null, string? password = null, int? isStatus = null);
    Task<UserDto?> GetByUsernameAsync(string username);
    Task<UserDto?> ValidateCredentialsAsync(string username, string password);
}
