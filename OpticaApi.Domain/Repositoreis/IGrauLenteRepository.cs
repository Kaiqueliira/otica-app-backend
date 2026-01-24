// OpticaApi.Domain/Repositories/IGrauLenteRepository.cs
using OpticaApi.Domain.Entities;

namespace OpticaApi.Domain.Repositories;

public interface IGrauLenteRepository
{
    Task<GrauLente> GetByIdAsync(int id);
    Task<IEnumerable<GrauLente>> GetAllAsync();
    Task<IEnumerable<GrauLente>> GetByClienteIdAsync(int clienteId);
    Task<int> CreateAsync(GrauLente grauLente);
    Task UpdateAsync(GrauLente grauLente);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetAllCountAsync();
}