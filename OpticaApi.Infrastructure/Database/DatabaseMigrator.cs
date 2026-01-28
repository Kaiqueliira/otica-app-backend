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

            var sqlDataNascimento = @"
        IF EXISTS (
            SELECT 1
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = 'Clientes'
              AND COLUMN_NAME = 'DataNascimento'
              AND IS_NULLABLE = 'NO'
        )
        BEGIN
            ALTER TABLE Clientes
            ALTER COLUMN DataNascimento DATE NULL;
        END
    ";
            await connection.ExecuteAsync(sqlDataNascimento);

            var sqlAdicaoOD = @"
        IF COL_LENGTH('GrausLentes', 'AdicaoOD') IS NULL
        BEGIN
            ALTER TABLE GrausLentes ADD AdicaoOD DECIMAL(5,2) NOT NULL DEFAULT 0;
        END
    ";
            await connection.ExecuteAsync(sqlAdicaoOD);

            var sqlAdicaoOE = @"
        IF COL_LENGTH('GrausLentes', 'AdicaoOE') IS NULL
        BEGIN
            ALTER TABLE GrausLentes ADD AdicaoOE DECIMAL(5,2) NOT NULL DEFAULT 0;
        END
    ";
            await connection.ExecuteAsync(sqlAdicaoOE);

            var sqlFkGraus = @"
        IF EXISTS (
            SELECT 1
            FROM sys.foreign_keys
            WHERE name = 'FK_GrausLentes_Clientes'
        )
        BEGIN
            ALTER TABLE GrausLentes DROP CONSTRAINT FK_GrausLentes_Clientes;
            ALTER TABLE GrausLentes ADD CONSTRAINT FK_GrausLentes_Clientes
                FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE CASCADE;
        END
    ";
            await connection.ExecuteAsync(sqlFkGraus);

            var sqlFkServicos = @"
        IF EXISTS (
            SELECT 1
            FROM sys.foreign_keys
            WHERE name = 'FK_Servicos_Clientes'
        )
        BEGIN
            ALTER TABLE Servicos DROP CONSTRAINT FK_Servicos_Clientes;
            ALTER TABLE Servicos ADD CONSTRAINT FK_Servicos_Clientes
                FOREIGN KEY (ClienteId) REFERENCES Clientes(Id) ON DELETE CASCADE;
        END
    ";
            await connection.ExecuteAsync(sqlFkServicos);

            Console.WriteLine("Migração aplicada com sucesso.");
        }
    }
}