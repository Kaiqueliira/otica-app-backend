using Dapper;
using Microsoft.Data.SqlClient;

namespace OpticaApi.Infrastructure.Database
{
    public class DatabaseMigrator
    {
        private readonly string _connectionString;

        public DatabaseMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task MigrateAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                        ALTER TABLE Clientes
                        ALTER COLUMN DataNascimento DATE NULL;
                "; 

            await connection.ExecuteAsync(sql);
        }
    }
}