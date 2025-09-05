// OpticaApi.Application/Services/GrauLenteService.cs
using OpticaApi.Application.DTOs;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Application.Services;

public class GrauLenteService : IGrauLenteService
{
    private readonly IGrauLenteRepository _grauLenteRepository;
    private readonly IClienteRepository _clienteRepository;

    public GrauLenteService(IGrauLenteRepository grauLenteRepository, IClienteRepository clienteRepository)
    {
        _grauLenteRepository = grauLenteRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<GrauLenteDto> GetByIdAsync(int id)
    {
        var grauLente = await _grauLenteRepository.GetByIdAsync(id);
        if (grauLente == null) return null;

        var cliente = await _clienteRepository.GetByIdAsync(grauLente.ClienteId);

        return new GrauLenteDto
        {
            Id = grauLente.Id,
            ClienteId = grauLente.ClienteId,
            ClienteNome = cliente?.Nome,
            EsfericoOD = grauLente.EsfericoOD,
            CilindricoOD = grauLente.CilindricoOD,
            EixoOD = grauLente.EixoOD,
            DPOD = grauLente.DPOD,
            EsfericoOE = grauLente.EsfericoOE,
            CilindricoOE = grauLente.CilindricoOE,
            EixoOE = grauLente.EixoOE,
            DPOE = grauLente.DPOE,
            Observacoes = grauLente.Observacoes,
            DataReceita = grauLente.DataReceita
        };
    }

    public async Task<IEnumerable<GrauLenteDto>> GetAllAsync()
    {
        var grausLentes = await _grauLenteRepository.GetAllAsync();
        var result = new List<GrauLenteDto>();

        foreach (var grau in grausLentes)
        {
            var cliente = await _clienteRepository.GetByIdAsync(grau.ClienteId);
            result.Add(new GrauLenteDto
            {
                Id = grau.Id,
                ClienteId = grau.ClienteId,
                ClienteNome = cliente?.Nome,
                EsfericoOD = grau.EsfericoOD,
                CilindricoOD = grau.CilindricoOD,
                EixoOD = grau.EixoOD,
                DPOD = grau.DPOD,
                EsfericoOE = grau.EsfericoOE,
                CilindricoOE = grau.CilindricoOE,
                EixoOE = grau.EixoOE,
                DPOE = grau.DPOE,
                Observacoes = grau.Observacoes,
                DataReceita = grau.DataReceita
            });
        }

        return result;
    }

    public async Task<IEnumerable<GrauLenteDto>> GetByClienteIdAsync(int clienteId)
    {
        var grausLentes = await _grauLenteRepository.GetByClienteIdAsync(clienteId);
        var cliente = await _clienteRepository.GetByIdAsync(clienteId);

        return grausLentes.Select(g => new GrauLenteDto
        {
            Id = g.Id,
            ClienteId = g.ClienteId,
            ClienteNome = cliente?.Nome,
            EsfericoOD = g.EsfericoOD,
            CilindricoOD = g.CilindricoOD,
            EixoOD = g.EixoOD,
            DPOD = g.DPOD,
            EsfericoOE = g.EsfericoOE,
            CilindricoOE = g.CilindricoOE,
            EixoOE = g.EixoOE,
            DPOE = g.DPOE,
            Observacoes = g.Observacoes,
            DataReceita = g.DataReceita
        });
    }

    public async Task<GrauLenteDto> CreateAsync(CreateGrauLenteDto createGrauLenteDto)
    {
        // Verificar se cliente existe
        var clienteExists = await _clienteRepository.ExistsAsync(createGrauLenteDto.ClienteId);
        if (!clienteExists)
            throw new InvalidOperationException("Cliente não encontrado");

        var grauLente = new GrauLente
        {
            ClienteId = createGrauLenteDto.ClienteId,
            EsfericoOD = createGrauLenteDto.EsfericoOD,
            CilindricoOD = createGrauLenteDto.CilindricoOD,
            EixoOD = createGrauLenteDto.EixoOD,
            DPOD = createGrauLenteDto.DPOD,
            EsfericoOE = createGrauLenteDto.EsfericoOE,
            CilindricoOE = createGrauLenteDto.CilindricoOE,
            EixoOE = createGrauLenteDto.EixoOE,
            DPOE = createGrauLenteDto.DPOE,
            Observacoes = createGrauLenteDto.Observacoes ?? string.Empty,
            DataReceita = createGrauLenteDto.DataReceita
        };

        var id = await _grauLenteRepository.CreateAsync(grauLente);
        grauLente.Id = id;

        var cliente = await _clienteRepository.GetByIdAsync(grauLente.ClienteId);

        return new GrauLenteDto
        {
            Id = grauLente.Id,
            ClienteId = grauLente.ClienteId,
            ClienteNome = cliente?.Nome,
            EsfericoOD = grauLente.EsfericoOD,
            CilindricoOD = grauLente.CilindricoOD,
            EixoOD = grauLente.EixoOD,
            DPOD = grauLente.DPOD,
            EsfericoOE = grauLente.EsfericoOE,
            CilindricoOE = grauLente.CilindricoOE,
            EixoOE = grauLente.EixoOE,
            DPOE = grauLente.DPOE,
            Observacoes = grauLente.Observacoes,
            DataReceita = grauLente.DataReceita
        };
    }

    public async Task UpdateAsync(int id, CreateGrauLenteDto updateGrauLenteDto)
    {
        var grauLente = await _grauLenteRepository.GetByIdAsync(id);
        if (grauLente == null)
            throw new KeyNotFoundException("Grau de lente não encontrado");

        grauLente.EsfericoOD = updateGrauLenteDto.EsfericoOD;
        grauLente.CilindricoOD = updateGrauLenteDto.CilindricoOD;
        grauLente.EixoOD = updateGrauLenteDto.EixoOD;
        grauLente.DPOD = updateGrauLenteDto.DPOD;
        grauLente.EsfericoOE = updateGrauLenteDto.EsfericoOE;
        grauLente.CilindricoOE = updateGrauLenteDto.CilindricoOE;
        grauLente.EixoOE = updateGrauLenteDto.EixoOE;
        grauLente.DPOE = updateGrauLenteDto.DPOE;
        grauLente.Observacoes = updateGrauLenteDto.Observacoes ?? string.Empty;
        grauLente.DataReceita = updateGrauLenteDto.DataReceita;

        await _grauLenteRepository.UpdateAsync(grauLente);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _grauLenteRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException("Grau de lente não encontrado");

        await _grauLenteRepository.DeleteAsync(id);
    }
}