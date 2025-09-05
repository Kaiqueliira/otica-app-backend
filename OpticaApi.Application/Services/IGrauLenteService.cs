// OpticaApi.Application/Services/IGrauLenteService.cs
using OpticaApi.Application.DTOs;

namespace OpticaApi.Application.Services;

public interface IGrauLenteService
{
    Task<GrauLenteDto> GetByIdAsync(int id);
    Task<IEnumerable<GrauLenteDto>> GetAllAsync();
    Task<IEnumerable<GrauLenteDto>> GetByClienteIdAsync(int clienteId);
    Task<GrauLenteDto> CreateAsync(CreateGrauLenteDto createGrauLenteDto);
    Task UpdateAsync(int id, CreateGrauLenteDto updateGrauLenteDto);
    Task DeleteAsync(int id);
}