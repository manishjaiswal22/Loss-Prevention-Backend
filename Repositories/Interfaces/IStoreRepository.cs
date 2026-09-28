using LossPrevention.Api.Models.Entities;

namespace LossPrevention.Api.Repositories.Interfaces;

public interface IStoreRepository
{
    Task<StoreMaster?> GetStoreConnectionDetailsAsync();
    Task<IEnumerable<StoreMaster>> GetAllStoresAsync();
}
