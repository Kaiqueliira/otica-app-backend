// OpticaApi.Domain/Repositories/IServicoRepository.cs
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Enums;

namespace OpticaApi.Domain.Repositories;

public interface IServicoRepository
{
    Task<Servico> GetByIdAsync(int id);
    Task<IEnumerable<Servico>> GetAllAsync();
    Task<IEnumerable<Servico>> GetByClienteIdAsync(int clienteId);
    Task<int> CreateAsync(Servico servico);
    Task UpdateAsync(Servico servico);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetAllCountAsync();
    Task<decimal> GetReceitaMensal();
    Task<int> GetServicoConcluidoHoje();
    Task<int> GetServicoByStatus(StatusServico status);
}