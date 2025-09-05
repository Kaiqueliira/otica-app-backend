// OpticaApi.Application/Services/IClienteService.cs
using OpticaApi.Application.DTOs;

namespace OpticaApi.Application.Services;

public interface IClienteService
{
    Task<ClienteDto> GetByIdAsync(int id);
    Task<IEnumerable<ClienteDto>> GetAllAsync();
    Task<ClienteDto> CreateAsync(CreateClienteDto createClienteDto);
    Task UpdateAsync(int id, UpdateClienteDto updateClienteDto);
    Task DeleteAsync(int id);
}