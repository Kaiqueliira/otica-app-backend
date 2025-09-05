// OpticaApi.Application/DTOs/GrauLenteDto.cs
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace OpticaApi.Application.DTOs;

[SwaggerSchema("Dados do grau de lente")]
public class GrauLenteDto
{
    [SwaggerSchema("ID único do grau")]
    public int Id { get; set; }

    [SwaggerSchema("ID do cliente")]
    public int ClienteId { get; set; }

    [SwaggerSchema("Nome do cliente")]
    public string ClienteNome { get; set; }

    [SwaggerSchema("Grau esférico olho direito")]
    public decimal EsfericoOD { get; set; }

    [SwaggerSchema("Grau cilíndrico olho direito")]
    public decimal CilindricoOD { get; set; }

    [SwaggerSchema("Eixo olho direito (0-180 graus)")]
    public int EixoOD { get; set; }

    [SwaggerSchema("Distância pupilar olho direito")]
    public decimal DPOD { get; set; }

    [SwaggerSchema("Grau esférico olho esquerdo")]
    public decimal EsfericoOE { get; set; }

    [SwaggerSchema("Grau cilíndrico olho esquerdo")]
    public decimal CilindricoOE { get; set; }

    [SwaggerSchema("Eixo olho esquerdo (0-180 graus)")]
    public int EixoOE { get; set; }

    [SwaggerSchema("Distância pupilar olho esquerdo")]
    public decimal DPOE { get; set; }

    [SwaggerSchema("Observações sobre o grau")]
    public string Observacoes { get; set; }

    [SwaggerSchema("Data da receita médica")]
    public DateTime DataReceita { get; set; }
}

[SwaggerSchema("Dados para criação de um novo grau de lente")]
public class CreateGrauLenteDto
{
    /// <summary>
    /// ID do cliente
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "Cliente é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Cliente deve ser informado")]
    [SwaggerSchema("ID do cliente")]
    public int ClienteId { get; set; }

    /// <summary>
    /// Grau esférico olho direito
    /// </summary>
    /// <example>-2.5</example>
    [Range(-30, 30, ErrorMessage = "Esférico OD deve estar entre -30 e +30")]
    [SwaggerSchema("Grau esférico do olho direito (-30 a +30)")]
    public decimal EsfericoOD { get; set; }

    /// <summary>
    /// Grau cilíndrico olho direito
    /// </summary>
    /// <example>-1.0</example>
    [Range(-10, 10, ErrorMessage = "Cilíndrico OD deve estar entre -10 e +10")]
    [SwaggerSchema("Grau cilíndrico do olho direito (-10 a +10)")]
    public decimal CilindricoOD { get; set; }

    /// <summary>
    /// Eixo olho direito
    /// </summary>
    /// <example>90</example>
    [Range(0, 180, ErrorMessage = "Eixo OD deve estar entre 0 e 180 graus")]
    [SwaggerSchema("Eixo do olho direito (0 a 180 graus)")]
    public int EixoOD { get; set; }

    /// <summary>
    /// Distância pupilar olho direito
    /// </summary>
    /// <example>32.0</example>
    [Range(20, 40, ErrorMessage = "DP OD deve estar entre 20 e 40mm")]
    [SwaggerSchema("Distância pupilar do olho direito (20 a 40mm)")]
    public decimal DPOD { get; set; }

    /// <summary>
    /// Grau esférico olho esquerdo
    /// </summary>
    /// <example>-2.25</example>
    [Range(-30, 30, ErrorMessage = "Esférico OE deve estar entre -30 e +30")]
    [SwaggerSchema("Grau esférico do olho esquerdo (-30 a +30)")]
    public decimal EsfericoOE { get; set; }

    /// <summary>
    /// Grau cilíndrico olho esquerdo
    /// </summary>
    /// <example>-0.75</example>
    [Range(-10, 10, ErrorMessage = "Cilíndrico OE deve estar entre -10 e +10")]
    [SwaggerSchema("Grau cilíndrico do olho esquerdo (-10 a +10)")]
    public decimal CilindricoOE { get; set; }

    /// <summary>
    /// Eixo olho esquerdo
    /// </summary>
    /// <example>85</example>
    [Range(0, 180, ErrorMessage = "Eixo OE deve estar entre 0 e 180 graus")]
    [SwaggerSchema("Eixo do olho esquerdo (0 a 180 graus)")]
    public int EixoOE { get; set; }

    /// <summary>
    /// Distância pupilar olho esquerdo
    /// </summary>
    /// <example>32.0</example>
    [Range(20, 40, ErrorMessage = "DP OE deve estar entre 20 e 40mm")]
    [SwaggerSchema("Distância pupilar do olho esquerdo (20 a 40mm)")]
    public decimal DPOE { get; set; }

    /// <summary>
    /// Observações sobre o grau
    /// </summary>
    /// <example>Receita para uso contínuo</example>
    [StringLength(500, ErrorMessage = "Observações devem ter no máximo 500 caracteres")]
    [SwaggerSchema("Observações sobre o grau (máximo 500 caracteres)")]
    public string Observacoes { get; set; }

    /// <summary>
    /// Data da receita médica
    /// </summary>
    /// <example>2023-12-01</example>
    [Required(ErrorMessage = "Data da receita é obrigatória")]
    [SwaggerSchema("Data da receita médica")]
    public DateTime DataReceita { get; set; }
}