// OpticaApi.Application/Validators/CreateGrauLenteValidator.cs
using FluentValidation;
using OpticaApi.Application.DTOs;

namespace OpticaApi.Application.Validators;

public class CreateGrauLenteValidator : AbstractValidator<CreateGrauLenteDto>
{
    public CreateGrauLenteValidator()
    {
        RuleFor(x => x.ClienteId)
            .GreaterThan(0).WithMessage("Cliente deve ser informado");

        RuleFor(x => x.EsfericoOD)
            .InclusiveBetween(-30, 30).WithMessage("Esférico OD deve estar entre -30 e +30");

        RuleFor(x => x.CilindricoOD)
            .InclusiveBetween(-10, 10).WithMessage("Cilíndrico OD deve estar entre -10 e +10");

        RuleFor(x => x.EixoOD)
            .InclusiveBetween(0, 180).WithMessage("Eixo OD deve estar entre 0 e 180 graus");

        RuleFor(x => x.DPOD)
            .InclusiveBetween(20, 40).WithMessage("DP OD deve estar entre 20 e 40mm");

        RuleFor(x => x.EsfericoOE)
            .InclusiveBetween(-30, 30).WithMessage("Esférico OE deve estar entre -30 e +30");

        RuleFor(x => x.CilindricoOE)
            .InclusiveBetween(-10, 10).WithMessage("Cilíndrico OE deve estar entre -10 e +10");

        RuleFor(x => x.EixoOE)
            .InclusiveBetween(0, 180).WithMessage("Eixo OE deve estar entre 0 e 180 graus");

        RuleFor(x => x.DPOE)
            .InclusiveBetween(20, 40).WithMessage("DP OE deve estar entre 20 e 40mm");

        RuleFor(x => x.DataReceita)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Data da receita não pode ser futura");

        RuleFor(x => x.Observacoes)
            .MaximumLength(500).WithMessage("Observações devem ter no máximo 500 caracteres");
    }
}