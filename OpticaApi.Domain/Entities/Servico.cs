// OpticaApi.Domain/Entities/Servico.cs
using OpticaApi.Domain.Enums;

namespace OpticaApi.Domain.Entities;

public class Servico
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public TipoServico TipoServico { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataServico { get; set; }
    public StatusServico Status { get; set; }
}