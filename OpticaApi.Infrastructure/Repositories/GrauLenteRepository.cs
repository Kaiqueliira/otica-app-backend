using Dapper;
using Microsoft.Data.SqlClient;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Infrastructure.Repositories;

public class GrauLenteRepository : IGrauLenteRepository
{
    private readonly string _connectionString;

    public GrauLenteRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<GrauLente> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM GrausLentes WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<GrauLente>(sql, new { Id = id });
    }

    public async Task<IEnumerable<GrauLente>> GetAllAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM GrausLentes ORDER BY DataReceita DESC";
        return await connection.QueryAsync<GrauLente>(sql);
    }

    public async Task<IEnumerable<GrauLente>> GetByClienteIdAsync(int clienteId)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT * FROM GrausLentes WHERE ClienteId = @ClienteId ORDER BY DataReceita DESC";
        return await connection.QueryAsync<GrauLente>(sql, new { ClienteId = clienteId });
    }

    public async Task<int> CreateAsync(GrauLente grauLente)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            INSERT INTO GrausLentes 
                (ClienteId, EsfericoOD, CilindricoOD, EixoOD, DPOD, 
                 EsfericoOE, CilindricoOE, EixoOE, DPOE, Observacoes, DataReceita, AdicaoOe, AdicaoOd)
            VALUES 
                (@ClienteId, @EsfericoOD, @CilindricoOD, @EixoOD, @DPOD, 
                 @EsfericoOE, @CilindricoOE, @EixoOE, @DPOE, @Observacoes, @DataReceita, @AdicaoOe, @AdicaoOd);

            SELECT CAST(SCOPE_IDENTITY() AS INT);"; 
        return await connection.QuerySingleAsync<int>(sql, grauLente);
    }

    public async Task UpdateAsync(GrauLente grauLente)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = @"
            UPDATE GrausLentes SET 
                EsfericoOD = @EsfericoOD, 
                CilindricoOD = @CilindricoOD, 
                EixoOD = @EixoOD, 
                DPOD = @DPOD,
                EsfericoOE = @EsfericoOE, 
                CilindricoOE = @CilindricoOE, 
                EixoOE = @EixoOE, 
                DPOE = @DPOE,
                Observacoes = @Observacoes, 
                DataReceita = @DataReceita,
                AdicaoOe = @AdicaoOe,
                AdicaoOd = @AdicaoOd
            WHERE Id = @Id";
        await connection.ExecuteAsync(sql, grauLente);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM GrausLentes WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT COUNT(1) FROM GrausLentes WHERE Id = @Id";
        var count = await connection.QuerySingleAsync<int>(sql, new { Id = id });
        return count > 0;
    }

    public async Task<int> GetAllCountAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "SELECT COUNT(*) FROM GrausLentes";
        return await connection.QueryFirstOrDefaultAsync<int>(sql);
    }
}
