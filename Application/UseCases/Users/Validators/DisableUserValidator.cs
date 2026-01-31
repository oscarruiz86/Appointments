using Application.UseCases.Users.Commands;
using FluentValidation;

namespace Application.UseCases.Users.Validators
{
    public class DisableUserValidator : AbstractValidator<DisableUserCommand>
    {
        public DisableUserValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
