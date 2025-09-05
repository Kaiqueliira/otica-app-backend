// OpticaApi.Application/DTOs/ServicoDto.cs
using System.ComponentModel.DataAnnotations;
using OpticaApi.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace OpticaApi.Application.DTOs;

[SwaggerSchema("Dados do serviço")]
public class ServicoDto
{
    [SwaggerSchema("ID único do serviço")]
    public int Id { get; set; }

    [SwaggerSchema("ID do cliente")]
    public int ClienteId { get; set; }

    [SwaggerSchema("Nome do cliente")]
    public string ClienteNome { get; set; }

    [SwaggerSchema("Tipo do serviço")]
    public TipoServico TipoServico { get; set; }

    [SwaggerSchema("Descrição do tipo de serviço")]
    public string TipoServicoDescricao { get; set; }

    [SwaggerSchema("Descrição detalhada do serviço")]
    public string Descricao { get; set; }

    [SwaggerSchema("Valor do serviço")]
    public decimal Valor { get; set; }

    [SwaggerSchema("Data do serviço")]
    public DateTime DataServico { get; set; }

    [SwaggerSchema("Status do serviço")]
    public StatusServico Status { get; set; }

    [SwaggerSchema("Descrição do status")]
    public string StatusDescricao { get; set; }
}

[SwaggerSchema("Dados para criação de um novo serviço")]
public class CreateServicoDto
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
    /// Tipo do serviço
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "Tipo de serviço é obrigatório")]
    [SwaggerSchema("Tipo do serviço (1=Venda, 2=Conserto, 3=Ajuste, 4=Troca)")]
    public TipoServico TipoServico { get; set; }

    /// <summary>
    /// Descrição do serviço
    /// </summary>
    /// <example>Óculos de grau com lente antirreflexo</example>
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Descrição deve ter entre 5 e 500 caracteres")]
    [SwaggerSchema("Descrição detalhada do serviço")]
    public string Descricao { get; set; }

    /// <summary>
    /// Valor do serviço
    /// </summary>
    /// <example>299.90</example>
    [Required(ErrorMessage = "Valor é obrigatório")]
    [Range(0.01, 99999.99, ErrorMessage = "Valor deve ser maior que zero")]
    [SwaggerSchema("Valor do serviço em reais")]
    public decimal Valor { get; set; }

    /// <summary>
    /// Data do serviço
    /// </summary>
    /// <example>2023-12-15</example>
    [Required(ErrorMessage = "Data do serviço é obrigatória")]
    [SwaggerSchema("Data de realização do serviço")]
    public DateTime DataServico { get; set; }
}

[SwaggerSchema("Dados para atualização de um serviço")]
public class UpdateServicoDto
{
    /// <summary>
    /// Tipo do serviço
    /// </summary>
    /// <example>2</example>
    [Required(ErrorMessage = "Tipo de serviço é obrigatório")]
    [SwaggerSchema("Tipo do serviço")]
    public TipoServico TipoServico { get; set; }

    /// <summary>
    /// Descrição do serviço
    /// </summary>
    /// <example>Conserto da armação quebrada</example>
    [Required(ErrorMessage = "Descrição é obrigatória")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Descrição deve ter entre 5 e 500 caracteres")]
    [SwaggerSchema("Descrição detalhada do serviço")]
    public string Descricao { get; set; }

    /// <summary>
    /// Valor do serviço
    /// </summary>
    /// <example>89.90</example>
    [Required(ErrorMessage = "Valor é obrigatório")]
    [Range(0.01, 99999.99, ErrorMessage = "Valor deve ser maior que zero")]
    [SwaggerSchema("Valor do serviço em reais")]
    public decimal Valor { get; set; }

    /// <summary>
    /// Data do serviço
    /// </summary>
    /// <example>2023-12-20</example>
    [Required(ErrorMessage = "Data do serviço é obrigatória")]
    [SwaggerSchema("Data de realização do serviço")]
    public DateTime DataServico { get; set; }

    /// <summary>
    /// Status do serviço
    /// </summary>
    /// <example>2</example>
    [Required(ErrorMessage = "Status é obrigatório")]
    [SwaggerSchema("Status do serviço (1=Pendente, 2=Concluído, 3=Cancelado)")]
    public StatusServico Status { get; set; }
}