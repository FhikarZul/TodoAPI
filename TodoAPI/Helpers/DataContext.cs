namespace TodoAPI.Helpers;

using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

public class DataContext
{
    private readonly DbSettings _dbSettings;

    public DataContext(IOptions<DbSettings> dbSettings)
    {
        _dbSettings = dbSettings.Value;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = $"Server={_dbSettings.Server}; Database={_dbSettings.Database}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        return new MySqlConnection(connectionString);
    }

    public async Task Init()
    {
        await InitDatabase();
        await InitTables();
    }

    private async Task InitDatabase()
    {
        // create database if it doesn't exist
        var connectionString = $"Server={_dbSettings.Server}; Uid={_dbSettings.UserId}; Pwd={_dbSettings.Password};";
        using var connection = new MySqlConnection(connectionString);
        var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
        await connection.ExecuteAsync(sql);
    }

    private async Task InitTables()
    {
        // create tables if they don't exist
        using var connection = CreateConnection();
        await _initUsers();

        async Task _initUsers()
        {
            var sql = """
                CREATE TABLE IF NOT EXISTS tb_todo (
                    id INT NOT NULL AUTO_INCREMENT,
                    name VARCHAR(255),
                    description VARCHAR(255),
                    PRIMARY KEY (Id)
                );
            """;
            await connection.ExecuteAsync(sql);
        }
    }
}