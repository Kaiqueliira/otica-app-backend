// OpticaApi.WebApi/Controllers/ServicosController.cs
using Microsoft.AspNetCore.Mvc;
using OpticaApi.Application.DTOs;
using OpticaApi.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OpticaApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Serviços")]
[SwaggerTag("Operações relacionadas aos serviços prestados aos clientes")]
public class ServicosController : ControllerBase
{
    private readonly IServicoService _servicoService;

    public ServicosController(IServicoService servicoService)
    {
        _servicoService = servicoService;
    }

    /// <summary>
    /// Lista todos os serviços cadastrados
    /// </summary>
    /// <returns>Lista de serviços</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Listar todos os serviços", Description = "Retorna uma lista com todos os serviços cadastrados")]
    [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<ServicoDto>))]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> GetAll()
    {
        var servicos = await _servicoService.GetAllAsync();
        return Ok(servicos);
    }

    /// <summary>
    /// Busca um serviço pelo ID
    /// </summary>
    /// <param name="id">ID do serviço</param>
    /// <returns>Serviço encontrado</returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Buscar serviço por ID", Description = "Retorna os dados de um serviço específico")]
    [SwaggerResponse(200, "Serviço encontrado", typeof(ServicoDto))]
    [SwaggerResponse(404, "Serviço não encontrado")]
    public async Task<ActionResult<ServicoDto>> GetById(int id)
    {
        var servico = await _servicoService.GetByIdAsync(id);

        if (servico == null)
            return NotFound();

        return Ok(servico);
    }

    /// <summary>
    /// Lista os serviços de um cliente específico
    /// </summary>
    /// <param name="clienteId">ID do cliente</param>
    /// <returns>Lista de serviços do cliente</returns>
    [HttpGet("cliente/{clienteId:int}")]
    [SwaggerOperation(Summary = "Listar serviços de um cliente", Description = "Retorna todos os serviços de um cliente específico")]
    [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<ServicoDto>))]
    public async Task<ActionResult<IEnumerable<ServicoDto>>> GetByClienteId(int clienteId)
    {
        var servicos = await _servicoService.GetByClienteIdAsync(clienteId);
        return Ok(servicos);
    }

    /// <summary>
    /// Cria um novo serviço
    /// </summary>
    /// <param name="createServicoDto">Dados do serviço a ser criado</param>
    /// <returns>Serviço criado</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar novo serviço", Description = "Cria um novo serviço para um cliente")]
    [SwaggerResponse(201, "Serviço criado com sucesso", typeof(ServicoDto))]
    [SwaggerResponse(400, "Dados inválidos")]
    public async Task<ActionResult<ServicoDto>> Create(CreateServicoDto createServicoDto)
    {
        try
        {
            var servico = await _servicoService.CreateAsync(createServicoDto);
            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza um serviço
    /// </summary>
    /// <param name="id">ID do serviço</param>
    /// <param name="updateServicoDto">Dados atualizados</param>
    /// <returns>Confirmação da atualização</returns>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar serviço", Description = "Atualiza os dados de um serviço existente")]
    [SwaggerResponse(204, "Serviço atualizado com sucesso")]
    [SwaggerResponse(404, "Serviço não encontrado")]
    public async Task<IActionResult> Update(int id, UpdateServicoDto updateServicoDto)
    {
        try
        {
            await _servicoService.UpdateAsync(id, updateServicoDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Remove um serviço
    /// </summary>
    /// <param name="id">ID do serviço</param>
    /// <returns>Confirmação da exclusão</returns>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remover serviço", Description = "Remove um serviço do sistema")]
    [SwaggerResponse(204, "Serviço removido com sucesso")]
    [SwaggerResponse(404, "Serviço não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _servicoService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Marca um serviço como concluído
    /// </summary>
    /// <param name="id">ID do serviço</param>
    /// <returns>Confirmação da conclusão</returns>
    [HttpPatch("{id:int}/concluir")]
    [SwaggerOperation(Summary = "Concluir serviço", Description = "Marca um serviço como concluído")]
    [SwaggerResponse(204, "Serviço marcado como concluído")]
    [SwaggerResponse(404, "Serviço não encontrado")]
    public async Task<IActionResult> MarcarComoConcluido(int id)
    {
        try
        {
            await _servicoService.MarcarComoConcluido(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Cancela um serviço
    /// </summary>
    /// <param name="id">ID do serviço</param>
    /// <returns>Confirmação do cancelamento</returns>
    [HttpPatch("{id:int}/cancelar")]
    [SwaggerOperation(Summary = "Cancelar serviço", Description = "Marca um serviço como cancelado")]
    [SwaggerResponse(204, "Serviço cancelado")]
    [SwaggerResponse(404, "Serviço não encontrado")]
    public async Task<IActionResult> CancelarServico(int id)
    {
        try
        {
            await _servicoService.CancelarServico(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}