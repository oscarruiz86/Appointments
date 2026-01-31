using Application.UseCases.Users.Commands;
using FluentValidation;

namespace Application.UseCases.Users.Validators
{
    public class ChangePasswordUserValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordUserValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Debe contener mayúscula")
                .Matches("[a-z]").WithMessage("Debe contener minúscula")
                .Matches("[0-9]").WithMessage("Debe contener número");
        }
    }
}
