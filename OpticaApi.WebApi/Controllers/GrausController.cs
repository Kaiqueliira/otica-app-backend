// OpticaApi.WebApi/Controllers/GrausController.cs
using Microsoft.AspNetCore.Mvc;
using OpticaApi.Application.DTOs;
using OpticaApi.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OpticaApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Graus de Lentes")]
[SwaggerTag("Operações relacionadas aos graus das lentes dos clientes")]
public class GrausController : ControllerBase
{
    private readonly IGrauLenteService _grauLenteService;

    public GrausController(IGrauLenteService grauLenteService)
    {
        _grauLenteService = grauLenteService;
    }

    /// <summary>
    /// Lista todos os graus de lentes cadastrados
    /// </summary>
    /// <returns>Lista de graus de lentes</returns>
    /// <response code="200">Retorna a lista de graus</response>
    [HttpGet]
    [SwaggerOperation(Summary = "Listar todos os graus", Description = "Retorna uma lista com todos os graus de lentes cadastrados no sistema")]
    [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<GrauLenteDto>))]
    [ProducesResponseType(typeof(IEnumerable<GrauLenteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GrauLenteDto>>> GetAll()
    {
        var graus = await _grauLenteService.GetAllAsync();
        return Ok(graus);
    }

    /// <summary>
    /// Busca um grau de lente pelo ID
    /// </summary>
    /// <param name="id">ID do grau</param>
    /// <returns>Grau encontrado</returns>
    /// <response code="200">Retorna o grau encontrado</response>
    /// <response code="404">Grau não encontrado</response>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Buscar grau por ID", Description = "Retorna os dados de um grau específico baseado no ID fornecido")]
    [SwaggerResponse(200, "Grau encontrado", typeof(GrauLenteDto))]
    [SwaggerResponse(404, "Grau não encontrado")]
    [ProducesResponseType(typeof(GrauLenteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GrauLenteDto>> GetById([FromRoute] int id)
    {
        var grau = await _grauLenteService.GetByIdAsync(id);

        if (grau == null)
            return NotFound(new { message = "Grau de lente não encontrado" });

        return Ok(grau);
    }

    /// <summary>
    /// Lista os graus de um cliente específico
    /// </summary>
    /// <param name="clienteId">ID do cliente</param>
    /// <returns>Lista de graus do cliente</returns>
    /// <response code="200">Retorna a lista de graus do cliente</response>
    [HttpGet("cliente/{clienteId:int}")]
    [SwaggerOperation(Summary = "Listar graus de um cliente", Description = "Retorna todos os graus de lentes de um cliente específico")]
    [SwaggerResponse(200, "Sucesso", typeof(IEnumerable<GrauLenteDto>))]
    [ProducesResponseType(typeof(IEnumerable<GrauLenteDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GrauLenteDto>>> GetByClienteId([FromRoute] int clienteId)
    {
        var graus = await _grauLenteService.GetByClienteIdAsync(clienteId);
        return Ok(graus);
    }

    /// <summary>
    /// Cria um novo grau de lente
    /// </summary>
    /// <param name="createGrauDto">Dados do grau a ser criado</param>
    /// <returns>Grau criado</returns>
    /// <response code="201">Grau criado com sucesso</response>
    /// <response code="400">Dados inválidos ou cliente não encontrado</response>
    [HttpPost]
    [SwaggerOperation(Summary = "Criar novo grau", Description = "Cria um novo registro de grau de lentes para um cliente")]
    [SwaggerResponse(201, "Grau criado com sucesso", typeof(GrauLenteDto))]
    [SwaggerResponse(400, "Dados inválidos ou cliente não encontrado")]
    [ProducesResponseType(typeof(GrauLenteDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GrauLenteDto>> Create([FromBody] CreateGrauLenteDto createGrauDto)
    {
        try
        {
            var grau = await _grauLenteService.CreateAsync(createGrauDto);
            return CreatedAtAction(nameof(GetById), new { id = grau.Id }, grau);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza um grau de lente
    /// </summary>
    /// <param name="id">ID do grau</param>
    /// <param name="updateGrauDto">Dados atualizados do grau</param>
    /// <returns>Confirmação da atualização</returns>
    /// <response code="204">Grau atualizado com sucesso</response>
    /// <response code="404">Grau não encontrado</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Atualizar grau", Description = "Atualiza os dados de um grau de lente existente")]
    [SwaggerResponse(204, "Grau atualizado com sucesso")]
    [SwaggerResponse(404, "Grau não encontrado")]
    [SwaggerResponse(400, "Dados inválidos")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateGrauLenteDto updateGrauDto)
    {
        try
        {
            await _grauLenteService.UpdateAsync(id, updateGrauDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Grau de lente não encontrado" });
        }
    }

    /// <summary>
    /// Remove um grau de lente do sistema
    /// </summary>
    /// <param name="id">ID do grau</param>
    /// <returns>Confirmação da exclusão</returns>
    /// <response code="204">Grau removido com sucesso</response>
    /// <response code="404">Grau não encontrado</response>
    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Remover grau", Description = "Remove um grau de lente do sistema permanentemente")]
    [SwaggerResponse(204, "Grau removido com sucesso")]
    [SwaggerResponse(404, "Grau não encontrado")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _grauLenteService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Grau de lente não encontrado" });
        }
    }

    /// <summary>
    /// Busca o grau mais recente de um cliente
    /// </summary>
    /// <param name="clienteId">ID do cliente</param>
    /// <returns>Grau mais recente do cliente</returns>
    /// <response code="200">Retorna o grau mais recente</response>
    /// <response code="404">Cliente não possui graus cadastrados</response>
    [HttpGet("cliente/{clienteId:int}/ultimo")]
    [SwaggerOperation(Summary = "Último grau do cliente", Description = "Retorna o grau mais recente de um cliente específico")]
    [SwaggerResponse(200, "Grau mais recente encontrado", typeof(GrauLenteDto))]
    [SwaggerResponse(404, "Cliente não possui graus cadastrados")]
    [ProducesResponseType(typeof(GrauLenteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GrauLenteDto>> GetUltimoGrauCliente([FromRoute] int clienteId)
    {
        var graus = await _grauLenteService.GetByClienteIdAsync(clienteId);
        var ultimoGrau = graus.OrderByDescending(g => g.DataReceita).FirstOrDefault();

        if (ultimoGrau == null)
            return NotFound(new { message = "Cliente não possui graus cadastrados" });

        return Ok(ultimoGrau);
    }
}