// OpticaApi.Infrastructure/Database/DatabaseInitializer.cs
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

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
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        await CreateTablesAsync(connection);
    }

    private async Task CreateTablesAsync(IDbConnection connection)
    {
        var createTables = @"
            CREATE TABLE IF NOT EXISTS Clientes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                CPF TEXT NOT NULL UNIQUE,
                Email TEXT,
                Telefone TEXT,
                Endereco TEXT,
                DataNascimento TEXT NOT NULL,
                DataCadastro TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS GrausLentes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ClienteId INTEGER NOT NULL,
                EsfericoOD REAL NOT NULL,
                CilindricoOD REAL NOT NULL,
                EixoOD INTEGER NOT NULL,
                DPOD REAL NOT NULL,
                EsfericoOE REAL NOT NULL,
                CilindricoOE REAL NOT NULL,
                EixoOE INTEGER NOT NULL,
                DPOE REAL NOT NULL,
                Observacoes TEXT,
                DataReceita TEXT NOT NULL,
                FOREIGN KEY (ClienteId) REFERENCES Clientes (Id)
            );

            CREATE TABLE IF NOT EXISTS Servicos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ClienteId INTEGER NOT NULL,
                TipoServico INTEGER NOT NULL,
                Descricao TEXT NOT NULL,
                Valor REAL NOT NULL,
                DataServico TEXT NOT NULL,
                Status INTEGER NOT NULL,
                FOREIGN KEY (ClienteId) REFERENCES Clientes (Id)
            );";

        await connection.ExecuteAsync(createTables);
    }
}