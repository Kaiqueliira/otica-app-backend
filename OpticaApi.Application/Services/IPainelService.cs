using OpticaApi.Application.Dtos;

namespace OpticaApi.Application.Services
{
    public interface IPainelService
    {
        Task<PainelDto> ObterInformacoesPainel();
    }
}
