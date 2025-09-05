// OpticaApi.Application/Validators/CreateServicoValidator.cs
using FluentValidation;
using OpticaApi.Application.DTOs;
using OpticaApi.Domain.Enums;

namespace OpticaApi.Application.Validators;

public class CreateServicoValidator : AbstractValidator<CreateServicoDto>
{
    public CreateServicoValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("Cliente deve ser informado");

        RuleFor(x => x.TipoServico)
            .IsInEnum().WithMessage("Tipo de serviço inválido");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .Length(5, 500).WithMessage("Descrição deve ter entre 5 e 500 caracteres");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("Valor deve ser maior que zero")
            .LessThan(100000).WithMessage("Valor deve ser menor que R$ 100.000");

        RuleFor(x => x.DataServico)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Data do serviço não pode ser futura");
    }
}

public class UpdateServicoValidator : AbstractValidator<UpdateServicoDto>
{
    public UpdateServicoValidator()
    {
        RuleFor(x => x.TipoServico)
            .IsInEnum().WithMessage("Tipo de serviço inválido");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .Length(5, 500).WithMessage("Descrição deve ter entre 5 e 500 caracteres");

        RuleFor(x => x.Valor)
            .GreaterThan(0).WithMessage("Valor deve ser maior que zero")
            .LessThan(100000).WithMessage("Valor deve ser menor que R$ 100.000");

        RuleFor(x => x.DataServico)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Data do serviço não pode ser futura");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status inválido");
    }
}