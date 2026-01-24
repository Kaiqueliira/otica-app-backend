// OpticaApi.Infrastructure/Repositories/ServicoRepository.cs
using Dapper;
using Microsoft.Data.Sqlite;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Enums;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Infrastructure.Repositories;

public class ServicoRepository : IServicoRepository
{
    private readonly string _connectionString;

    public ServicoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Servico> GetByIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT * FROM Servicos WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Servico>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Servico>> GetAllAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT * FROM Servicos ORDER BY DataServico DESC";
        return await connection.QueryAsync<Servico>(sql);
    }

    public async Task<IEnumerable<Servico>> GetByClienteIdAsync(int clienteId)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT * FROM Servicos WHERE ClienteId = @ClienteId ORDER BY DataServico DESC";
        return await connection.QueryAsync<Servico>(sql, new { ClienteId = clienteId });
    }

    public async Task<int> CreateAsync(Servico servico)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = @"INSERT INTO Servicos 
                    (ClienteId, TipoServico, Descricao, Valor, DataServico, Status)
                    VALUES 
                    (@ClienteId, @TipoServico, @Descricao, @Valor, @DataServico, @Status);
                    SELECT last_insert_rowid();";
        return await connection.QuerySingleAsync<int>(sql, servico);
    }

    public async Task UpdateAsync(Servico servico)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = @"UPDATE Servicos SET 
                        TipoServico = @TipoServico, 
                        Descricao = @Descricao, 
                        Valor = @Valor, 
                        DataServico = @DataServico,
                        Status = @Status
                    WHERE Id = @Id";
        await connection.ExecuteAsync(sql, servico);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "DELETE FROM Servicos WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT COUNT(1) FROM Servicos WHERE Id = @Id";
        var count = await connection.QuerySingleAsync<int>(sql, new { Id = id });
        return count > 0;
    }

    public async Task<int> GetAllCountAsync()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = "SELECT COUNT(*) FROM Servicos";
        return await connection.QueryFirstOrDefaultAsync<int>(sql);
    }

    public async Task<decimal> GetReceitaMensal()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = @"
        SELECT COALESCE(SUM(valor), 0) 
        FROM Servicos
        WHERE strftime('%Y-%m', dataServico) = strftime('%Y-%m', 'now')";

        return await connection.QueryFirstOrDefaultAsync<decimal>(sql);
    }

    public async Task<int> GetServicoConcluidoHoje()
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = $"SELECT COUNT(*)FROM Servicos WHERE date(dataServico) = date('now','localtime') AND Status = {Convert.ToInt64(StatusServico.Concluido)};";
        return await connection.QueryFirstOrDefaultAsync<int>(sql);
    }

    public async Task<int> GetServicoByStatus(StatusServico status)
    {
        using var connection = new SqliteConnection(_connectionString);
        var sql = $"SELECT COUNT(*)FROM Servicos WHERE Status = {Convert.ToInt64(status)};";
        return await connection.QueryFirstOrDefaultAsync<int>(sql);
    }
}