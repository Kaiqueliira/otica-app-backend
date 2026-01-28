namespace OpticaApi.Domain.Entities;
public class GrauLente
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal EsfericoOD { get; set; }
    public decimal CilindricoOD { get; set; }
    public int EixoOD { get; set; }
    public decimal DPOD { get; set; }
    public decimal EsfericoOE { get; set; }
    public decimal CilindricoOE { get; set; }
    public int EixoOE { get; set; }
    public decimal DPOE { get; set; }
    public string Observacoes { get; set; }
    public DateTime DataReceita { get; set; }
    public decimal AdicaoOD { get; set; }
    public decimal AdicaoOE { get; set; }
}