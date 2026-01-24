using Microsoft.AspNetCore.Mvc;
using OpticaApi.Application.Dtos;
using OpticaApi.Application.DTOs;
using OpticaApi.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace OpticaApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Tags("Painel")]
    [SwaggerTag("Operações relacionadas aos dados apresentados no painel inicial")]
    public class PainelController(IPainelService painelService) : ControllerBase
    {
        /// <summary>
        /// Informações do painel incial
        /// </summary>
        /// <returns>Informações do painel incial</returns>
        /// <response code="200">Retorna Informações do painel incial</response>
        [HttpGet]
        [SwaggerOperation(Summary = "Listar Informações do painel incial", Description = "Retorna Informações do painel incial")]
        [SwaggerResponse(200, "Sucesso", typeof(PainelDto))]
        [ProducesResponseType(typeof(PainelDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PainelDto>> GetAllCount()
        {
            var count = await painelService.ObterInformacoesPainel();
            return Ok(count);
        }


    }
}
