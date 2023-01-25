using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly string _default;
    private readonly string _mfConnx;
    private readonly string _velociti;

    public SqlDataAccess(IConfiguration Config)
    {
        IConfiguration config = Config;
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(1),
                MaxDelay = TimeSpan.FromSeconds(5),
                MaxRetries = 5,
                Mode = RetryMode.Exponential,
            },
        };

    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(SQLStatment, parameters, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<T>> CallSP<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("C3PLDB"));

        return await connection.QueryAsync<T>(SQLStatment, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string SQLStatment, T parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        await connection.ExecuteAsync(SQLStatment, parameters, commandType: CommandType.Text);
    }

    public async Task<long> ExecuteScalar<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("C3PLDB"));

        return await connection.ExecuteScalarAsync<long>(SQLStatment, parameters, commandType: CommandType.Text);
    }

    private string GetConnectionString(string connectionId)
    {
        string connectionString = Environment.GetEnvironmentVariable("C3PLDB");
        switch (connectionId)
        {
            case "MFConnx":
                connectionString = _mfConnx;
                break;

            case "Velociti":
                connectionString = _velociti;
                break;
        }

        return connectionString;
    }
}