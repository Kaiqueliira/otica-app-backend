using OpticaApi.Application.Dtos;
using OpticaApi.Domain.Enums;
using OpticaApi.Domain.Repositories;

namespace OpticaApi.Application.Services
{
    public class PainelService(IGrauLenteRepository grauLenteRepository, IClienteRepository clienteRepository, IServicoRepository servicoRepository) : IPainelService
    {
        public async Task<PainelDto> ObterInformacoesPainel()
        {
            var countGrau = await grauLenteRepository.GetAllCountAsync();
            var countClientes = await clienteRepository.GetAllCountAsync();
            var countServico = await servicoRepository.GetAllCountAsync();
            var receitaMensal = await servicoRepository.GetReceitaMensal();
            var servicoConcluidosHoje = await servicoRepository.GetServicoConcluidoHoje();
            var servicosPendentes = await servicoRepository.GetServicoByStatus(StatusServico.Pendente);

            var result = new PainelDto
            {
                Graus = countGrau,
                Clientes = countClientes,
                Servicos = countServico,
                ReceitaMensal = receitaMensal,
                ConcluidosHoje = servicoConcluidosHoje,
                ServicosPendentes = servicosPendentes
            };

            return result;
        }
    }
}
