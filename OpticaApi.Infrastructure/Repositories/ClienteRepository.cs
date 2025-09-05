// OpticaApi.Infrastructure/Repositories/ClienteRepository.cs
using Dapper;
using Microsoft.Data.Sqlite;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly string _connectionString;

    public ClienteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = @"SELECT Id, Nome, CPF, Email, Telefone, Endereco, 
                           DataNascimento, DataCadastro 
                    FROM Clientes WHERE Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = @"SELECT Id, Nome, CPF, Email, Telefone, Endereco, 
                           DataNascimento, DataCadastro 
                    FROM Clientes ORDER BY Nome";

        return await connection.QueryAsync<Cliente>(sql);
    }

    public async Task<Cliente> GetByCPFAsync(string cpf)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = @"SELECT Id, Nome, CPF, Email, Telefone, Endereco, 
                           DataNascimento, DataCadastro 
                    FROM Clientes WHERE CPF = @CPF";

        return await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { CPF = cpf });
    }

    public async Task<int> CreateAsync(Cliente cliente)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = @"INSERT INTO Clientes (Nome, CPF, Email, Telefone, Endereco, DataNascimento, DataCadastro)
                    VALUES (@Nome, @CPF, @Email, @Telefone, @Endereco, @DataNascimento, @DataCadastro);
                    SELECT last_insert_rowid();";

        return await connection.QuerySingleAsync<int>(sql, cliente);
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = @"UPDATE Clientes SET 
                        Nome = @Nome, 
                        Email = @Email, 
                        Telefone = @Telefone, 
                        Endereco = @Endereco
                    WHERE Id = @Id";

        await connection.ExecuteAsync(sql, cliente);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = "DELETE FROM Clientes WHERE Id = @Id";

        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);

        var sql = "SELECT COUNT(1) FROM Clientes WHERE Id = @Id";

        var count = await connection.QuerySingleAsync<int>(sql, new { Id = id });
        return count > 0;
    }
}