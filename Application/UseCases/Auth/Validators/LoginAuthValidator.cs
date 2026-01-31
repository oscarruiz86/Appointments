using Application.UseCases.Auth.Commands;
using FluentValidation;

namespace Application.UseCases.Auth.Validators
{
    public class LoginAuthValidator : AbstractValidator<LoginCommand>
    {
        public LoginAuthValidator()
        {

            RuleFor(x => x.TenantId)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();


            RuleFor(x => x.Password)
                .NotEmpty();


        }
    }
}
