// OpticaApi.Application/Services/ServicoService.cs
using OpticaApi.Application.DTOs;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Enums;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Application.Services;

public class ServicoService : IServicoService
{
    private readonly IServicoRepository _servicoRepository;
    private readonly IClienteRepository _clienteRepository;

    public ServicoService(IServicoRepository servicoRepository, IClienteRepository clienteRepository)
    {
        _servicoRepository = servicoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<ServicoDto> GetByIdAsync(int id)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);
        if (servico == null) return null;

        var cliente = await _clienteRepository.GetByIdAsync(servico.ClienteId);

        return new ServicoDto
        {
            Id = servico.Id,
            ClienteId = servico.ClienteId,
            ClienteNome = cliente?.Nome,
            TipoServico = servico.TipoServico,
            TipoServicoDescricao = GetTipoServicoDescricao(servico.TipoServico),
            Descricao = servico.Descricao,
            Valor = servico.Valor,
            DataServico = servico.DataServico,
            Status = servico.Status,
            StatusDescricao = GetStatusDescricao(servico.Status)
        };
    }

    public async Task<IEnumerable<ServicoDto>> GetAllAsync()
    {
        var servicos = await _servicoRepository.GetAllAsync();
        var result = new List<ServicoDto>();

        foreach (var servico in servicos)
        {
            var cliente = await _clienteRepository.GetByIdAsync(servico.ClienteId);
            result.Add(new ServicoDto
            {
                Id = servico.Id,
                ClienteId = servico.ClienteId,
                ClienteNome = cliente?.Nome,
                TipoServico = servico.TipoServico,
                TipoServicoDescricao = GetTipoServicoDescricao(servico.TipoServico),
                Descricao = servico.Descricao,
                Valor = servico.Valor,
                DataServico = servico.DataServico,
                Status = servico.Status,
                StatusDescricao = GetStatusDescricao(servico.Status)
            });
        }

        return result;
    }

    public async Task<IEnumerable<ServicoDto>> GetByClienteIdAsync(int clienteId)
    {
        var servicos = await _servicoRepository.GetByClienteIdAsync(clienteId);
        var cliente = await _clienteRepository.GetByIdAsync(clienteId);

        return servicos.Select(s => new ServicoDto
        {
            Id = s.Id,
            ClienteId = s.ClienteId,
            ClienteNome = cliente?.Nome,
            TipoServico = s.TipoServico,
            TipoServicoDescricao = GetTipoServicoDescricao(s.TipoServico),
            Descricao = s.Descricao,
            Valor = s.Valor,
            DataServico = s.DataServico,
            Status = s.Status,
            StatusDescricao = GetStatusDescricao(s.Status)
        });
    }

    public async Task<ServicoDto> CreateAsync(CreateServicoDto createServicoDto)
    {
        // Verificar se cliente existe
        var clienteExists = await _clienteRepository.ExistsAsync(createServicoDto.ClienteId);
        if (!clienteExists)
            throw new InvalidOperationException("Cliente não encontrado");

        var servico = new Servico
        {
            ClienteId = createServicoDto.ClienteId,
            TipoServico = createServicoDto.TipoServico,
            Descricao = createServicoDto.Descricao,
            Valor = createServicoDto.Valor,
            DataServico = createServicoDto.DataServico,
            Status = StatusServico.Pendente
        };

        var id = await _servicoRepository.CreateAsync(servico);
        servico.Id = id;

        var cliente = await _clienteRepository.GetByIdAsync(servico.ClienteId);

        return new ServicoDto
        {
            Id = servico.Id,
            ClienteId = servico.ClienteId,
            ClienteNome = cliente?.Nome,
            TipoServico = servico.TipoServico,
            TipoServicoDescricao = GetTipoServicoDescricao(servico.TipoServico),
            Descricao = servico.Descricao,
            Valor = servico.Valor,
            DataServico = servico.DataServico,
            Status = servico.Status,
            StatusDescricao = GetStatusDescricao(servico.Status)
        };
    }

    public async Task UpdateAsync(int id, UpdateServicoDto updateServicoDto)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);
        if (servico == null)
            throw new KeyNotFoundException("Serviço não encontrado");

        servico.TipoServico = updateServicoDto.TipoServico;
        servico.Descricao = updateServicoDto.Descricao;
        servico.Valor = updateServicoDto.Valor;
        servico.DataServico = updateServicoDto.DataServico;
        servico.Status = updateServicoDto.Status;

        await _servicoRepository.UpdateAsync(servico);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _servicoRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException("Serviço não encontrado");

        await _servicoRepository.DeleteAsync(id);
    }

    public async Task MarcarComoConcluido(int id)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);
        if (servico == null)
            throw new KeyNotFoundException("Serviço não encontrado");

        servico.Status = StatusServico.Concluido;
        await _servicoRepository.UpdateAsync(servico);
    }

    public async Task CancelarServico(int id)
    {
        var servico = await _servicoRepository.GetByIdAsync(id);
        if (servico == null)
            throw new KeyNotFoundException("Serviço não encontrado");

        servico.Status = StatusServico.Cancelado;
        await _servicoRepository.UpdateAsync(servico);
    }

    private string GetTipoServicoDescricao(TipoServico tipo)
    {
        return tipo switch
        {
            TipoServico.Venda => "Venda",
            TipoServico.Conserto => "Conserto",
            TipoServico.Ajuste => "Ajuste",
            TipoServico.Troca => "Troca",
            _ => "Desconhecido"
        };
    }

    private string GetStatusDescricao(StatusServico status)
    {
        return status switch
        {
            StatusServico.Pendente => "Pendente",
            StatusServico.Concluido => "Concluído",
            StatusServico.Cancelado => "Cancelado",
            _ => "Desconhecido"
        };
    }
}