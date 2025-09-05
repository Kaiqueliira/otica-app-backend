using OpticaApi.Domain.Entities;

namespace OpticaApi.Domain.Repositories;

public interface IClienteRepository
{
    Task<Cliente> GetByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByCPFAsync(string cpf);
    Task<int> CreateAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}