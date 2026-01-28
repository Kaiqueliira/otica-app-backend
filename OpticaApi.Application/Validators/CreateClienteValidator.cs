// OpticaApi.Application/Validators/CreateClienteValidator.cs
using FluentValidation;
using OpticaApi.Application.DTOs;

namespace OpticaApi.Application.Validators;

public class CreateClienteValidator : AbstractValidator<CreateClienteDto>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres");

   /*     RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF é obrigatório")
            .Length(11).WithMessage("CPF deve ter 11 dígitos")
            .Matches(@"^\d+$").WithMessage("CPF deve conter apenas números");
*/
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email inválido")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Telefone)
            .Length(10, 15).WithMessage("Telefone deve ter entre 10 e 15 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Telefone));

        //RuleFor(x => x.DataNascimento)
        //    .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data atual");
    }
}