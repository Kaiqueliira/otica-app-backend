// OpticaApi.Application/Services/ClienteService.cs
using OpticaApi.Application.DTOs;
using OpticaApi.Domain.Entities;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteDto> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null) return null;

        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            CPF = cliente.CPF,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Endereco = cliente.Endereco,
            DataNascimento = cliente.DataNascimento,
            DataCadastro = cliente.DataCadastro
        };
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return clientes.Select(c => new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            CPF = c.CPF,
            Email = c.Email,
            Telefone = c.Telefone,
            Endereco = c.Endereco,
            DataNascimento = c.DataNascimento,
            DataCadastro = c.DataCadastro
        });
    }

    public async Task<ClienteDto> CreateAsync(CreateClienteDto createClienteDto)
    {
     /*   var clienteExistente = await _clienteRepository.GetByCPFAsync(createClienteDto.CPF);
        if (clienteExistente != null)
            throw new InvalidOperationException("Cliente com este CPF já existe");*/

        var cliente = new Cliente
        {
            Nome = createClienteDto.Nome,
            CPF = createClienteDto.CPF,
            Email = createClienteDto.Email,
            Telefone = createClienteDto.Telefone,
            Endereco = createClienteDto.Endereco,
            DataNascimento = createClienteDto.DataNascimento,
            DataCadastro = DateTime.Now
        };

        var id = await _clienteRepository.CreateAsync(cliente);
        cliente.Id = id;

        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            CPF = cliente.CPF,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Endereco = cliente.Endereco,
            DataNascimento = cliente.DataNascimento,
            DataCadastro = cliente.DataCadastro
        };
    }

    public async Task UpdateAsync(int id, UpdateClienteDto updateClienteDto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente == null)
            throw new KeyNotFoundException("Cliente não encontrado");

        cliente.Nome = updateClienteDto.Nome;
        cliente.Email = updateClienteDto.Email;
        cliente.Telefone = updateClienteDto.Telefone;
        cliente.Endereco = updateClienteDto.Endereco;

        await _clienteRepository.UpdateAsync(cliente);
    }

    public async Task DeleteAsync(int id)
    {
        var exists = await _clienteRepository.ExistsAsync(id);
        if (!exists)
            throw new KeyNotFoundException("Cliente não encontrado");

        await _clienteRepository.DeleteAsync(id);
    }
}