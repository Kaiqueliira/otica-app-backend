// OpticaApi.WebApi/Controllers/ClientesController.cs
using Microsoft.AspNetCore.Mvc;
using OpticaApi.Application.DTOs;
using OpticaApi.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace OpticaApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Clientes")]
[SwaggerTag("Operações relacionadas ao cadastro de clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    /// <summary>
    /// Lista todos os clientes cadastrados
    /// </summary>
    /// <returns>Lista de clientes</returns>
    /// <response code="200">Retorna a lista de clientes</response>
    [HttpGet]
    [SwaggerOperation(Summary = "Listar todos os clientes", Description = "Retorna uma lista paginada com todos os clientes cadastrados no sistema")]
    [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<ClienteDto>))]
    [ProducesResponseType(typeof(IEnumerable<ClienteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Busca um cliente pelo ID
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>Cliente encontrado</returns>
    /// <response code="200">Retorna o cliente encontrado</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Buscar cliente por ID", Description = "Retorna os dados de um cliente específico baseado no ID fornecido")]
    [SwaggerResponse(200, "Cliente encontrado", typeof(ClienteDto))]
    [SwaggerResponse(404, "Cliente não encontrado")]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> GetById([FromRoute] int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);

        if (cliente == null)
            return NotFound(new { message = "Cliente não encontrado" });

        return Ok(cliente);
    }

    /// <summary>
    /// Cria um novo cliente
    /// </summary>
    /// <param name="createClienteDto">Dados do cliente a ser criado</param>
    /// <returns>Cliente criado</returns>
    /// <response code="201">Cliente criado com sucesso</response>
    /// <response code="400">Dados inválidos ou CPF já existe</response>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar novo cliente", Description = "Cria um novo cliente no sistema com os dados fornecidos")]
    [SwaggerResponse(201, "Cliente criado com sucesso", typeof(ClienteDto))]
    [SwaggerResponse(400, "Dados inválidos ou CPF já cadastrado")]
    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDto>> Create([FromBody] CreateClienteDto createClienteDto)
    {
        try
        {
            var cliente = await _clienteService.CreateAsync(createClienteDto);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza os dados de um cliente
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="updateClienteDto">Dados atualizados do cliente</param>
    /// <returns>Confirmação da atualização</returns>
    /// <response code="204">Cliente atualizado com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar cliente", Description = "Atualiza os dados de um cliente existente")]
    [SwaggerResponse(204, "Cliente atualizado com sucesso")]
    [SwaggerResponse(404, "Cliente não encontrado")]
    [SwaggerResponse(400, "Dados inválidos")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateClienteDto updateClienteDto)
    {
        try
        {
            await _clienteService.UpdateAsync(id, updateClienteDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Cliente não encontrado" });
        }
    }

    /// <summary>
    /// Remove um cliente do sistema
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>Confirmação da exclusão</returns>
    /// <response code="204">Cliente removido com sucesso</response>
    /// <response code="404">Cliente não encontrado</response>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remover cliente", Description = "Remove um cliente do sistema permanentemente")]
    [SwaggerResponse(204, "Cliente removido com sucesso")]
    [SwaggerResponse(404, "Cliente não encontrado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Cliente não encontrado" });
        }
    }
}