// OpticaApi.Application/Services/IServicoService.cs
using OpticaApi.Application.DTOs;

namespace OpticaApi.Application.Services;

public interface IServicoService
{
    Task<ServicoDto> GetByIdAsync(int id);
    Task<IEnumerable<ServicoDto>> GetAllAsync();
    Task<IEnumerable<ServicoDto>> GetByClienteIdAsync(int clienteId);
    Task<ServicoDto> CreateAsync(CreateServicoDto createServicoDto);
    Task UpdateAsync(int id, UpdateServicoDto updateServicoDto);
    Task DeleteAsync(int id);
    Task MarcarComoConcluido(int id);
    Task CancelarServico(int id);
}