using System.Data;
using Dapper;
using LossPrevention.Api.Common.Constants;
using LossPrevention.Api.Data;
using LossPrevention.Api.Models.Entities;
using LossPrevention.Api.Repositories.Interfaces;

namespace LossPrevention.Api.Repositories.Implementations;

public class StoreRepository : IStoreRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public StoreRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<StoreMaster?> GetStoreConnectionDetailsAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<StoreMaster>(
            SpNames.GetStoreConnectionDetails,
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IEnumerable<StoreMaster>> GetAllStoresAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT StoreId, StoreName, StoreCode, ReaderIP, ReaderConnType, IsActive FROM dbo.STOREMASTER WITH (NOLOCK) WHERE IsActive = 1 ORDER BY StoreId ASC;";
        return await connection.QueryAsync<StoreMaster>(sql);
    }
}
