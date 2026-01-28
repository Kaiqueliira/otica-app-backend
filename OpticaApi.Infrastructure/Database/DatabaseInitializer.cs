using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace OpticaApi.Infrastructure.Database;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeAsync()
    {
        await CreateDatabaseIfNotExists();

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await CreateTablesAsync(connection);

    }

    private async Task CreateDatabaseIfNotExists()
    {
        var masterConnectionString = _connectionString.Replace("Database=OpticaApi;", "Database=master;");

        using var connection = new SqlConnection(masterConnectionString);
        await connection.OpenAsync();

        var sql = @"
        IF DB_ID('OpticaApi') IS NULL
        BEGIN
            CREATE DATABASE OpticaApi;
        END";

        await connection.ExecuteAsync(sql);
    }

    private async Task CreateTablesAsync(IDbConnection connection)
    {
        var sql = @"
        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Clientes')
        BEGIN
           CREATE TABLE Clientes (
                Id BIGINT IDENTITY(1,1) PRIMARY KEY,
                Nome NVARCHAR(200) NOT NULL,
                CPF NVARCHAR(14) NULL,      
                Email NVARCHAR(200),
                Telefone NVARCHAR(20),
                Endereco NVARCHAR(300),
                DataNascimento DATE NOT NULL,
                DataCadastro DATETIME2 NOT NULL);
        END

        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'GrausLentes')
        BEGIN
            CREATE TABLE GrausLentes (
                Id BIGINT IDENTITY(1,1) PRIMARY KEY,
                ClienteId BIGINT NOT NULL,
                EsfericoOD DECIMAL(6,2) NOT NULL,
                CilindricoOD DECIMAL(6,2) NOT NULL,
                EixoOD INT NOT NULL,
                DPOD DECIMAL(6,2) NOT NULL,
                EsfericoOE DECIMAL(6,2) NOT NULL,
                CilindricoOE DECIMAL(6,2) NOT NULL,
                EixoOE INT NOT NULL,
                DPOE DECIMAL(6,2) NOT NULL,
                Observacoes NVARCHAR(500),
                DataReceita DATE NOT NULL,
                CONSTRAINT FK_GrausLentes_Clientes
                    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
            );
        END

        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Servicos')
        BEGIN
            CREATE TABLE Servicos (
                Id BIGINT IDENTITY(1,1) PRIMARY KEY,
                ClienteId BIGINT NOT NULL,
                TipoServico INT NOT NULL,
                Descricao NVARCHAR(300) NOT NULL,
                Valor DECIMAL(10,2) NOT NULL,
                DataServico DATE NOT NULL,
                Status INT NOT NULL,
                CONSTRAINT FK_Servicos_Clientes
                    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
            );
        END";

        await connection.ExecuteAsync(sql);
    }
}
