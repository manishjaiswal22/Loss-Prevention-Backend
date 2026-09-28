using LossPrevention.Api.Models.DTOs.Common;
using LossPrevention.Api.Models.Entities;
using LossPrevention.Api.Repositories.Interfaces;
using LossPrevention.Api.Services.Interfaces;

namespace LossPrevention.Api.Services.Implementations;

public class StoreService : IStoreService
{
    private readonly IStoreRepository _storeRepository;

    public StoreService(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<ApiResponse<StoreMaster?>> GetStoreConnectionAsync()
    {
        var store = await _storeRepository.GetStoreConnectionDetailsAsync();
        return ApiResponse<StoreMaster?>.Ok(store, "Store connection details retrieved");
    }

    public async Task<ApiResponse<IEnumerable<StoreMaster>>> GetAllStoresAsync()
    {
        var stores = await _storeRepository.GetAllStoresAsync();
        return ApiResponse<IEnumerable<StoreMaster>>.Ok(stores, "Stores list retrieved");
    }
}
