using Domain.Model;
using Microsoft.Data.Sqlite;

namespace TileProxyServer.Services;

public class BlacklistDatabaseService(string connectionString) : IBlacklistDatabaseService
{
    private readonly string _connectionString = connectionString;

    private void EnsureTableCreated()
    {
        using var connection = new SqliteConnection(_connectionString);
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


    public void BlockIpAddress(string ipAddress)
    {
        EnsureTableCreated();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var blockExpression = "INSERT INTO Blacklist (IpAddress) VALUES (@ipAddress)";
        var ipAddressParameter = new SqliteParameter("@ipAddress", ipAddress);

        var blockCommand = new SqliteCommand(blockExpression)
        {
            Connection = connection
        };
        blockCommand.Parameters.Add(ipAddressParameter);

        var rowsAffected = blockCommand.ExecuteNonQuery();

        if (rowsAffected == 0)
            throw new SqliteException("Ip addres has not been added", 1);
    }

    public bool IsBlocked(string ipAddress)
    {
        EnsureTableCreated();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var selectExpression = "SELECT * FROM Blacklist WHERE IpAddress = @ipAddress";
        var ipAddressParameter = new SqliteParameter("@ipAddress", ipAddress);

        var checkCommand = new SqliteCommand(selectExpression)
        {
            Connection = connection
        };
        checkCommand.Parameters.Add(ipAddressParameter);

        var reader = checkCommand.ExecuteReader();
        return reader.HasRows;
    }
}
