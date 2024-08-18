using Gdd.Domain.Services;
using Gdd.Repository.Utils;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace TileProxyServer.Services;

public class BlacklistRepository(IOptionsMonitor<SqliteConfiguration> configuration) : IBlacklistRepository
{
    private readonly SqliteConfiguration _configuration = configuration.CurrentValue;

    private void EnsureTableCreated()
    {
        using var connection = new SqliteConnection(_configuration.ConnectionString);
        connection.Open();

        var checkCommand = new SqliteCommand
        {
            Connection = connection,
            CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Blacklist'"
        };

        var reader = checkCommand.ExecuteReader();
        if (!reader.HasRows)
        {
            var createTableCommand = new SqliteCommand()
            {
                Connection = connection,
                CommandText = "CREATE TABLE Blacklist(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, IpAddress TEXT NOT NULL)"
            };

            createTableCommand.ExecuteNonQuery();
        }
    }


    public async Task AddIp(string ipAddress)
    {
        EnsureTableCreated();

        using var connection = new SqliteConnection(_configuration.ConnectionString);
        connection.Open();

        var blockExpression = "INSERT INTO Blacklist (IpAddress) VALUES (@ipAddress)";
        var ipAddressParameter = new SqliteParameter("@ipAddress", ipAddress);

        var blockCommand = new SqliteCommand(blockExpression)
        {
            Connection = connection
        };
        blockCommand.Parameters.Add(ipAddressParameter);

        var rowsAffected = await blockCommand.ExecuteNonQueryAsync();

        if (rowsAffected == 0)
            throw new SqliteException("Ip addres has not been added", 1);
    }

    public async Task<bool> IsInDatabase(string ipAddress)
    {
        EnsureTableCreated();

        using var connection = new SqliteConnection(_configuration.ConnectionString);
        connection.Open();

        var selectExpression = "SELECT * FROM Blacklist WHERE IpAddress = @ipAddress";
        var ipAddressParameter = new SqliteParameter("@ipAddress", ipAddress);

        var checkCommand = new SqliteCommand(selectExpression)
        {
            Connection = connection
        };
        checkCommand.Parameters.Add(ipAddressParameter);

        var reader = await checkCommand.ExecuteReaderAsync();
        return reader.HasRows;
    }
}
