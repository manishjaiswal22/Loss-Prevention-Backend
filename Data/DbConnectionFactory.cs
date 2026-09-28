using System.Data;
using Microsoft.Data.SqlClient;

namespace LossPrevention.Api.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("RFID_ReaderDB")
            ?? throw new InvalidOperationException("Connection string 'RFID_ReaderDB' was not found in appsettings.json.");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
